using UnityEngine;

public class ProjectileEye : MonoBehaviour
{
    [SerializeField] float speed;

    SpriteRenderer sr;
    Animator animator;
    Rigidbody2D rb;

    readonly int animId_OnHit = Animator.StringToHash("OnHit");
    readonly int animId_OnReset = Animator.StringToHash("OnReset");

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        animator.SetTrigger(animId_OnReset);
    }

    private void ResetProjectile()
    {
        // TODO change to use object pool
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            animator.SetTrigger(animId_OnHit);
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void SetVelocity(Vector2 dir)
    {
        rb.linearVelocity = dir.normalized * speed;
        sr.flipX = dir.x > 0;
    }
}
