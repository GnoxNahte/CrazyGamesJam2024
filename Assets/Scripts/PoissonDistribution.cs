using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;
 
[RequireComponent(typeof(ObjectPoolVariation))]
public class PoissonDistribution : MonoBehaviour
{
    [SerializeField]
    [Range(1, 5000)] 
    int startCount;
    [SerializeField] float spawnWidth; // 0 is the center
    [SerializeField] float spawnHeight; // 0 is the bottom
    [SerializeField] float spacing;
    [SerializeField] float spacingVariation;
    [SerializeField] Vector2Int gridCount = new(5, 5);
    [SerializeField]
    [Range(1, 30)]
    int tryCount;
    [Tooltip("Use -1 to not use the seed")]
    [SerializeField] int randSeed;
    [SerializeField] LayerMask validLayers; // Valid layers that can be spawned on
    // Only needed when in editor, when removing all objs to regenerate.
    [SerializeField] List<GameObject> currObjs;

    private ObjectPoolVariation pool;

    [SerializeField] bool respawnOnValidate;

    private void Awake()
    {

        if (!enabled)
            return;

        ClearChildren();

        pool = GetComponent<ObjectPoolVariation>();
        if (!pool.initDone)
            pool.Init(startCount);

        SpawnAll();

#if UNITY_EDITOR
        if (randSeed != -1)
            Random.InitState(randSeed);
#endif
    }

    public void SpawnAll()
    {
        Vector2 gridSize = new(spawnWidth / gridCount.x, spawnHeight / gridCount.y);

        List<Transform>[,] objGrid = new List<Transform>[gridCount.x, gridCount.y];
        Queue<Transform> unusedObjs = new();
        Bounds bounds = new Bounds(transform.position, new Vector2(spawnWidth, spawnHeight));

        Vector2Int GetGridPos(Vector2 pos)
        {
            pos -= (Vector2)transform.position;
              
            return new Vector2Int((int)((pos.x + spawnWidth / 2) / gridSize.x), (int)((pos.y + spawnHeight / 2) / gridSize.y));
        }

        void SpawnObj(Vector2 pos)
        {
            GameObject obj = Spawn(pos);
            Vector2Int gridPos = GetGridPos(pos);
            if (objGrid[gridPos.x, gridPos.y] == null)
            {
                objGrid[gridPos.x, gridPos.y] = new List<Transform>(1);
            }
            objGrid[gridPos.x, gridPos.y].Add(obj.transform);
            unusedObjs.Enqueue(obj.transform);
            currObjs.Add(obj);
            obj.SetActive(true);
        }

        SpawnObj(transform.position);

        int spawnCount = 1;
        while (spawnCount < startCount && unusedObjs.Count > 0)
        {
            Transform currObj = unusedObjs.Dequeue();
            Vector2 pos = currObj.position;
            for (int currTryCount = 0; currTryCount < tryCount; currTryCount++)
            {
                float angle = Random.value * Mathf.PI * 2;

                // TODO: Check if can replace the 2 lines with Random.insideUnitCircle
                Vector2 newPos = pos + new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * spacing;
                newPos += spacingVariation * Random.insideUnitCircle;

                if (!bounds.Contains(newPos))
                    continue;

                //if (newPos.x < -spawnWidth * 0.5f || newPos.x > spawnWidth * 0.5f ||
                //    newPos.y < 0 || newPos.y > spawnHeight)
                //    continue;

                if (!CheckIfCanSpawn(newPos))
                    continue;

                Vector2Int gridPos = GetGridPos(newPos);
                bool isValid = true;
                for (int i = Mathf.Max(gridPos.x - 1, 0); i <= gridPos.x + 1 && i < gridCount.x; i++)
                {
                    for (int j = Mathf.Max(gridPos.y - 1, 0); j <= gridPos.y + 1 && j < gridCount.y; j++)
                    {
                        if (objGrid[i, j] == null)
                            continue;

                        // Check if it'll overlap with any other objs
                        foreach (Transform t in objGrid[i, j])
                        {
                            if (((Vector2)t.position - newPos).sqrMagnitude < spacing * spacing)
                            {
                                isValid = false;
                                break;
                            }

                        }

                        if (!isValid)
                            break;
                    }

                    if (!isValid)
                        break;
                }

                if (isValid)
                {
                    SpawnObj(newPos);
                    spawnCount++;

                    if (spawnCount >= startCount)
                    {
                        break;
                    }
                }
            }
        }
    }

    public void ReleaseAllObjs()
    {
        foreach (var obj in currObjs)
        {
            if (obj.activeSelf)
            {
                pool.Release(obj.gameObject);
            }
        }

        currObjs.Clear();
    }

    public GameObject Spawn(Vector3 pos)
    {
        return pool.GetRandom(pos);
    }

    public void ReleaseObj(GameObject obj)
    {
        pool.Release(obj);
    }

    public bool CheckIfCanSpawn(Vector2 pos)
    {
        return Physics2D.OverlapCircle(pos, 0.1f, validLayers) != null;
    }

    [Button]
    private void SpawnEditor()
    {
        DestroyAll();

        Awake();
        SpawnAll();
    }

    private void OnValidate()
    {
        if (!respawnOnValidate)
            return;

        Awake();
        ReleaseAllObjs();
        SpawnAll();
    }

    [Button]
    public void DestroyAll()
    {
        if (!pool)
            pool = GetComponent<ObjectPoolVariation>();

        ReleaseAllObjs();
        pool.DestroyAll();


        // Not sure why it loses track of some children. 
        // A temp solution. TODO
        ClearChildren();
    }

    private void ClearChildren()
    {
        while (transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnWidth, spawnHeight, 1));
    }
}