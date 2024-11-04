using UnityEngine;

public class BaboEnemy : BasicEnemy
{
    [SerializeField] Transform projectileSpawnLocation;

    protected override void Attack()
    {
        GameObject projectile = GameManager.SpawnerManager.ProjectilePool.Get(projectileSpawnLocation.position);
        projectile.GetComponent<ProjectileEye>().SetVelocity(target.transform.position - transform.position);
    }
}
