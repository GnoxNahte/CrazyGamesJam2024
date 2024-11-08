using UnityEngine;
using VInspector;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform bowTransform;

    InputManager controls;
    PlayerAnimations animations;

    public enum Weapon
    {
        Knife,
        Bow,
        Bomb,
        WEAPON_COUNT
    }

    [SerializeField] [ReadOnly]
    Weapon currWeapon;

    Camera mainCam;

    public Weapon CurrWeapon => currWeapon;

    private void Awake()
    {
        animations = GetComponent<PlayerAnimations>();
    }

    private void Start()
    {
        mainCam = Camera.main;
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

        Vector3 dir = (mainCam.ScreenToWorldPoint(controls.MousePos) - transform.position).normalized;
        bowTransform.position = transform.position + dir;
        bowTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }
}
