using System.Collections;
using UnityEngine;
using VInspector;

public class ShootEmUp_Babo : EntityBase
{
    [SerializeField] GameObject parent;

    [SerializeField] ShootEmUp_EnemyGun gun1;
    [SerializeField] ShootEmUp_EnemyGun gun2;
    [SerializeField] float moveWidth; // 0 is the center
    [SerializeField] float speed; 
    [SerializeField] [MinMaxSlider(0.5f, 5f)]
    Vector2 waitRange;

    Animator animator;

    float timeLeft = 1f;
    bool isMovingRight = true;

    int animId_OnDead = Animator.StringToHash("IsDead");

    public ShootEmUp_EnemySpawner spawner;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        timeLeft = Random.Range(waitRange.x, waitRange.y);
        isMovingRight = Random.value < 0.5f;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0 || 
            transform.position.x < -moveWidth * 0.5f || transform.position.x > moveWidth * 0.5f)
        {
            timeLeft = Random.Range(waitRange.x, waitRange.y);
            isMovingRight = !isMovingRight;
        }
        
        transform.position = transform.position + speed * Time.deltaTime * (isMovingRight ? 1 : -1) * Vector3.right;
    }

    

    public override void OnDead()
    {
        base.OnDead();

        gun1.gameObject.SetActive(false);
        gun2.gameObject.SetActive(false);
        animator.SetBool(animId_OnDead, true);
    }

    public override void OnDeadAnimDone()
    {
        spawner.ReleaseEnemy(gameObject);
    }
}
