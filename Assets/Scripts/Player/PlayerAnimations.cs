using System;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    int currAnim;
    int nextAnim;

    Animator animator;

    PlayerMovement movement;
    PlayerAttack attack;

    readonly int animId_DirX = Animator.StringToHash("DirX");
    readonly int animId_DirY = Animator.StringToHash("DirY");
    readonly int animId_Attack = Animator.StringToHash("Attack");
    readonly int animId_AttackType = Animator.StringToHash("AttackType");
    readonly int animId_IsDead = Animator.StringToHash("IsDead");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        animator.SetFloat(animId_DirX, movement.MoveDir.x);
        animator.SetFloat(animId_DirY, movement.MoveDir.y);
    }

    public void OnAttack(PlayerAttack.Weapon weapon)
    {
        animator.SetInteger(animId_AttackType, (int)weapon);
        animator.SetTrigger(animId_Attack);
    }
}
