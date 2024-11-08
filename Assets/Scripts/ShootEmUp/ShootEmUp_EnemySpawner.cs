using System.Collections.Generic;
using UnityEngine;
using VInspector;

[RequireComponent(typeof(ObjectPool))]
public class ShootEmUp_EnemySpawner : MonoBehaviour
{
    ObjectPool pool;

    [SerializeField] int maxCount = 10;
    [SerializeField] float heightSpacing;

    [SerializeField] float spawnWidth; // 0 is the center
    [SerializeField] float spawnHeight; // 0 is the bottom

    [SerializeField] int debugTestCount;

    [SerializeField] int enemiesLeft;
    public static int EnemyCount;

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
        pool.Init(maxCount);
    }

    public void SpawnEnemies()
    {
        enemiesLeft = EnemyCount;
        EnemyCount = Mathf.Min(EnemyCount, maxCount);
        Vector2 xRange = new Vector2(transform.position.x - spawnWidth * 0.5f, transform.position.x + spawnWidth * 0.5f);

        for (int i = 0; i < EnemyCount; i++)
        {
            var enemy = pool.Get(new Vector2(Random.Range(xRange.x, xRange.y), transform.position.y - i * heightSpacing));
            enemy.GetComponentInChildren<ShootEmUp_Babo>().spawner = this;
        }
    }

    [Button]
    public void TestSpawnDebug()
    {
        if (!pool)
            pool = GetComponent<ObjectPool>();

        pool.DestroyAll();
        Awake();
        SpawnEnemies();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnWidth, spawnHeight, 1));
    }

    public void ReleaseEnemy(GameObject gameObject)
    {
        pool.Release(gameObject);

        --enemiesLeft;
        if (enemiesLeft <= 0)
        {
            GameManager.MinigameManager.OnTransitionMinigame(MinigameManager.GameType.MainGame);
        }
    }
}