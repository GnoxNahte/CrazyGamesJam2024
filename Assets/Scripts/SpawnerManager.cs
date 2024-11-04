using UnityEngine;
using VInspector;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] PoissonDistribution treeDistribution;
    [SerializeField] ObjectPool projectilePool;

    public ObjectPool ProjectilePool => projectilePool;

    private void Start()
    {
        treeDistribution.SpawnAll();
        projectilePool.Init();
    }
}
