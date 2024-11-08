using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    [SerializeField] int ammoCount;
    [SerializeField] Transform spawnPoint;
    [SerializeField] ObjectPool projectilePool;

    [Range(5f, 500f)]
    public float bulletSpeed;

    private void Start()
    {
        projectilePool.Init(ammoCount);
    }

    public void ReleaseArrow(GameObject gameObject)
    {
        projectilePool.Release(gameObject);
    }

    // Update is called once per frame
    //private void Update()
    //{
    //    timeBetweenEachShot = 1f / attackSpeed;

    //    TryShoot();
    //}

    private void TryShoot()
    {
        Vector2 spawnPos = spawnPoint.position;
        GameObject projectile = projectilePool.Get(spawnPos);
        print("Fwd: " + transform.rotation.eulerAngles);

        projectile.GetComponent<PlayerArrow>().SetVelocity(transform.right);
    }
}