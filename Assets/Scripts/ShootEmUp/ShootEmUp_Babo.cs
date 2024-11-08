using UnityEngine;

public class ShootEmUp_Babo : EntityBase
{
    [SerializeField] GameObject parent;

    [SerializeField] ShootEmUp_EnemyGun gun1;
    [SerializeField] ShootEmUp_EnemyGun gun2;
    
    Animator animator;

    int animId_OnDead = Animator.StringToHash("IsDead");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnDead()
    {
        base.OnDead();

        gun1.gameObject.SetActive(false);
        gun2.gameObject.SetActive(false);
        animator.SetBool(animId_OnDead, true);
    }

    //public override void OnDeadAnimDone()
    //{
    //    Destroy(parent);
    //}
}
