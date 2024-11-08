using System.Collections;
using UnityEngine;
using VInspector;

public class BasicEnemy : EnemyBase
{
    [SerializeField] float loseVisionRadius;
    [SerializeField] float detectRadius;
    [SerializeField] float attackRadius;
    [SerializeField] int damage;
    [SerializeField] float speed;

    [SerializeField] [ReadOnly]
    protected EntityBase target;
    [SerializeField] [ReadOnly]
    Vector2 newDir;

    [SerializeField] Collider2D[] overlapCircleResults;
    protected int overlapCircleCount;
    [SerializeField] protected ContactFilter2D overlapCircleFilter;

    [SerializeField]
    [ReadOnly]
    float timeLeftInState;

    [SerializeField] float findUpdateFrequency = 0.5f;
    [SerializeField] float chaseUpdateFrequency = 0.2f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    // Coroutine
    WaitForSeconds findWait;
    WaitForSeconds chaseWait;

    readonly int animId_State = Animator.StringToHash("State");

    protected virtual void Awake()
    {
        overlapCircleResults = new Collider2D[1];
        newDir = Vector2.zero;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        
        findWait = new WaitForSeconds(findUpdateFrequency);
        chaseWait = new WaitForSeconds(chaseUpdateFrequency);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        ChangeState(State.Find);
        StartCoroutine(UpdateCoroutine());
        rb.simulated = true;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + newDir * speed * Time.deltaTime);
        if (newDir.x != 0)
            sr.flipX = newDir.x > 0;
    }

    public override void OnDead()
    {
        base.OnDead();

        rb.simulated = false;
        ChangeState(State.Dead);
    }

    public override void OnDeadAnimDone()
    {
        GameManager.MainGameManager.SpawnerManager.EnemySpawner.Release(gameObject);
    }

    protected override void ChangeState(State newState)
    {
        newDir = Vector2.zero;

        // Previous state
        //switch (currState)
        //{
        //}

        base.ChangeState(newState);

        //switch (newState)
        //{
        //}

        animator.SetInteger(animId_State, (int)newState);
    }

    private Transform FindTarget()
    {
        int count = Physics2D.OverlapCircle(transform.position, detectRadius, overlapCircleFilter, overlapCircleResults);
        if (count > 0)
        {
            Collider2D closest = null;
            float minSqrDistance = float.MaxValue;

            // Find closest target
            foreach (Collider2D col in overlapCircleResults)
            {
                float dist = (col.transform.position - transform.position).sqrMagnitude;
                if (dist < minSqrDistance)
                {
                    closest = col;
                    dist = minSqrDistance;
                }
            }

            return closest.transform;
        }

        return null;
    }

    virtual protected void Attack()
    {
        target.TakeDamage(damage);
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            switch (currState)
            {
                // Don't do anything in these states
                case State.Attack:
                case State.Dead:
                    yield return new WaitUntil(() => currState != State.Attack && currState != State.Dead);
                    break;
                case State.Find:
                {
                    target = FindTarget()?.GetComponent<EntityBase>();
                    if (target)
                        ChangeState(State.Chase);

                    yield return findWait;
                    break;
                }
                case State.Chase:
                {
                    float sqrDistToTarget = (target.transform.position - transform.position).sqrMagnitude;
                    if (sqrDistToTarget < attackRadius * attackRadius)
                    {
                        ChangeState(State.Attack);
                        break;
                    }
                    else if (sqrDistToTarget > loseVisionRadius * loseVisionRadius)
                    {
                        ChangeState(State.Find);
                        break;
                    }

                    newDir = (target.transform.position - transform.position).normalized;

                    yield return chaseWait;
                    break;
                }
                //case State.Attack:
                //    {
                //        timeLeftInState -= Time.deltaTime;
                //        if (timeLeftInState > 0)
                //            Attack();
                //        else
                //        {
                //            ChangeState(State.Chase);
                //            break;
                //        }

                //        yield return null;
                //        break;
                //    }
                default:
                    yield return null;
                    break;
            }
        }
    }

    virtual protected void OnDrawGizmosSelected()
    {
        Color originalColor = Gizmos.color;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, loseVisionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = originalColor;
    }
}
