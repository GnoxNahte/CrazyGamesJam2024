using UnityEngine;
using VInspector;

public class PlayerAttack : MonoBehaviour
{
    InputManager controls;
    PlayerAnimations animations;

    public enum Weapon
    {
        Bow,
        Knife,
        Bomb,
        WEAPON_COUNT
    }

    [SerializeField] [ReadOnly]
    Weapon currWeapon;

    public Weapon CurrWeapon => currWeapon;

    private void Awake()
    {
        animations = GetComponent<PlayerAnimations>();
    }

    private void Start()
    {
        controls = GameManager.InputManager;
    }

    private void Update()
    {
        if (controls.SwitchWeapon)
        {
            currWeapon = currWeapon + 1;
            if (currWeapon == Weapon.WEAPON_COUNT)
                currWeapon = 0;
        }

        if (controls.IsHoldingPrimaryAction)
        {
            animations.OnAttack(currWeapon);
        }
    }
}
