using System;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    int currAnim;
    int nextAnim;

    Animator animator;
    SpriteRenderer sr;

    PlayerMovement movement;
    PlayerAttack attack;

    readonly int animId_DirX = Animator.StringToHash("DirX");
    readonly int animId_DirY = Animator.StringToHash("DirY");
    readonly int animId_IsMelee = Animator.StringToHash("IsMelee");
    readonly int animId_UseAbility = Animator.StringToHash("UseAbility");
    readonly int animId_IsDead = Animator.StringToHash("IsDead");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        animator.SetFloat(animId_DirX, movement.MoveDir.x);
        animator.SetFloat(animId_DirY, movement.MoveDir.y);

        if (movement.MoveDir.x != 0)
            sr.flipX = movement.MoveDir.x < 0;
    }

    public void OnChangeWeapon(bool isUsingMelee)
    {
        animator.SetBool(animId_IsMelee, isUsingMelee);
    }

    public void OnUseAbility()
    {
        animator.SetTrigger(animId_UseAbility);
    }

    public void OnUseAbilityAnimDone()
    {
        bool success = attack.OnTransportEnemies();
        
        if (success) 
            GameManager.MinigameManager.OnTransitionMinigame(MinigameManager.GameType.ShootEmUp);
    }
}
