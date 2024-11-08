using UnityEngine;
using VInspector;

public class MainGame_SpawnerManager : MonoBehaviour
{
    [SerializeField] PoissonDistribution treeDistribution;
    [SerializeField] ObjectPool projectilePool;
    [SerializeField] EnemySpawner enemySpawner;

    public ObjectPool ProjectilePool => projectilePool;
    public EnemySpawner EnemySpawner => enemySpawner;

    private void Start()
    {
        treeDistribution.SpawnAll();
        projectilePool.Init();
    }
}
