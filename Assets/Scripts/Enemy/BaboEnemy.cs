using UnityEngine;

public class BaboEnemy : BasicEnemy
{
    [SerializeField] Transform projectileSpawnLocation;

    private ObjectPool projectilePool;

    protected override void Start()
    {
        base.Start();

        projectilePool = GameManager.MainGameManager.SpawnerManager.ProjectilePool;
    }

    protected override void Attack()
    {
        GameObject projectile = projectilePool.Get(projectileSpawnLocation.position);
        projectile.GetComponent<ProjectileEye>().SetVelocity(target.transform.position - transform.position);
    }
}
