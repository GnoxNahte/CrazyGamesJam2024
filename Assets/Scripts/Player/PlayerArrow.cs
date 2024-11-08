using System.Collections;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;

    SpriteRenderer sr;
    Animator animator;
    Rigidbody2D rb;

    static WaitForSeconds waitForSeconds = new WaitForSeconds(2);

    readonly int animId_OnHit = Animator.StringToHash("OnHit");
    readonly int animId_OnReset = Animator.StringToHash("OnReset");

    Coroutine currCoroutine;
    Vector2 lastVelocity;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        currCoroutine = StartCoroutine(ReleaseAfterTime());
        rb.linearVelocity = lastVelocity;
    }

    IEnumerator ReleaseAfterTime()
    {
        yield return waitForSeconds;

        OnHit();
    }

    private void OnHit()
    {
        animator.SetTrigger(animId_OnHit);
        rb.linearVelocity = Vector2.zero;
    }

    private void ResetProjectile()
    {
        animator.SetTrigger(animId_OnReset);
        GameManager.MainGameManager.Player.Bow.ReleaseArrow(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BasicEnemy enemy = collision.GetComponent<BasicEnemy>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
            OnHit();
        }
    }

    public void SetVelocity(Vector2 dir)
    {
        rb.linearVelocity = dir.normalized * speed;
        rb.SetRotation(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        lastVelocity = dir;
        //sr.flipX = dir.x > 0;
    }
}
