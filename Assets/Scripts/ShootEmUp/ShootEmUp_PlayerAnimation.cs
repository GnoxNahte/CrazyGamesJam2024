using UnityEngine;

public class ShootEmUp_PlayerAnimation : MonoBehaviour
{

    Animator animator;

    PlayerMovement movement;
    ShootEmUp_Gun gun;

    readonly int animId_IsAttacking = Animator.StringToHash("IsAttacking");
    readonly int animId_IsDead = Animator.StringToHash("IsDead");

    private void Awake()
    {
        animator = GetComponent<Animator>();

        movement = GetComponent<PlayerMovement>();
        gun = GetComponent<ShootEmUp_Gun>();
    }

    private void Update()
    {
        //animator.SetBool(animId_IsAttacking, gun.)
    }
}
