using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootEmUp_Gun : MonoBehaviour
{
    [SerializeField] int ammoCount;
    [SerializeField] ObjectPool ammoPool;
    [SerializeField] Transform spawnPoint;

    [Range(5f, 500f)]
    public float bulletSpeed;
    [Tooltip("Shots per second")]
    [Range(0.5f, 1000f)]
    public float attackSpeed = 1f;

    private float timeBetweenEachShot; // 1 / attackSpeed
    private float lastShotTime;

    private Vector2 prevPos;
    private Quaternion prevRotation;

    // Caching
    private InputManager controls;
    private ObjectPool projectilePool;

    private void Awake()
    {
        projectilePool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        controls = GameManager.InputManager;

        lastShotTime = Time.time;
        prevRotation = transform.rotation;

        ammoPool.Init(ammoCount);
    }

    // Update is called once per frame
    private void Update()
    {
        timeBetweenEachShot = 1f / attackSpeed;

        TryShoot();
    }

    private void TryShoot()
    {
        if (lastShotTime + timeBetweenEachShot > Time.time)
            return;

        float timeSinceLastShot = Time.time - lastShotTime;
        int numBullets = Mathf.FloorToInt(timeSinceLastShot / timeBetweenEachShot);

        for (int i = 0; i < numBullets; i++)
        {
            float timeOffset = (i) * timeBetweenEachShot;
            float lerpTime = timeOffset / timeSinceLastShot;
            Vector2 spawnPos = spawnPoint.position;
            GameObject projectile = projectilePool.Get(spawnPos);

            projectile.GetComponent<ShootEmUp_Bullet>().Init(bulletSpeed, Color.white, i * timeBetweenEachShot);
        }

        lastShotTime = lastShotTime + numBullets * timeBetweenEachShot;

        prevPos = spawnPoint.position;
        prevRotation = transform.rotation;
    }

    public void ReleaseBullet(GameObject bullet)
    {
        projectilePool.Release(bullet);
    }
}
