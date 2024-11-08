using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootEmUp_Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    [SerializeField] LayerMask layerMask;
    [SerializeField] bool isPlayer;

    // Caching
    private Rigidbody2D rb;
    private Animator animator;

    ObjectPool pool;

    readonly int animId_OnHit = Animator.StringToHash("OnHit");

    private void OnEnable()
    {
        animator?.SetBool(animId_OnHit, false);
    }

    public void Init(ObjectPool pool, float speed, float angle, Color color, float simulateSeconds)
    {
        this.pool = pool;
        //GetComponent<SpriteRenderer>().color = color;
        this.speed = speed;

        //rb.velocity = rb.transform.up * speed;
        transform.position = transform.position + transform.up * speed * simulateSeconds;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.back);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = transform.position + (isPlayer ? 1 : -1) * transform.up * speed * Time.deltaTime;
    }

    private void SimulateTime(float seconds)
    {
        transform.position = transform.position + transform.up * speed * seconds;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((layerMask & (1 << collision.gameObject.layer)) == 0)
            return;

        animator?.SetBool(animId_OnHit, true);
        pool.Release(gameObject);

        ShootEmUp_Babo babo = collision.GetComponent<ShootEmUp_Babo>();
        if (babo != null)
        {
            babo.TakeDamage(damage);
        }
    }
}