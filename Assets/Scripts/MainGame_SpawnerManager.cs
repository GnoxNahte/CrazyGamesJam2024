using UnityEngine;
using VInspector;

public class MainGame_SpawnerManager : MonoBehaviour
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
