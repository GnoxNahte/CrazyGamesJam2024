using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class EnemySpawner : MonoBehaviour
{
    private ObjectPool pool;
    [SerializeField] bool useFastSpawn;
    [SerializeField] private AnimationCurve spawnCurve;
    [SerializeField] private AnimationCurve fastSpawnCurve;

    [SerializeField] int spawnLimit;

    [SerializeField] [MinMaxSlider(1, 50)]
    Vector2 spawnRange;

    Transform camTransform;

    public void Release(GameObject obj)
    {
        pool.Release(obj);
    }

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        camTransform = Camera.main.transform;

        pool.Init(spawnLimit / 2);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        var waitOverLimit = new WaitForSeconds(1);
        while (true)
        {
            if (pool.ActiveCount > spawnLimit)
            {
                yield return waitOverLimit;
                continue;
            }
            
            yield return new WaitForSeconds((useFastSpawn ? fastSpawnCurve : spawnCurve).Evaluate(Time.timeSinceLevelLoad));
            pool.Get((Vector2)camTransform.position + Utility.GetRandomPointOnUnitCircle() * Random.Range(spawnRange.x, spawnRange.y));
        }
    }
}
