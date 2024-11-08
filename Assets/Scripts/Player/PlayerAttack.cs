using UnityEngine;
using VInspector;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform bowTransform;
    [SerializeField] float bowDist;
    [SerializeField] float abilityRadius;
    [SerializeField] LayerMask abilityLayerMask;
    [SerializeField] SpriteRenderer abilityCircle;
    [SerializeField] Transform meleeAttackTransform;
    [SerializeField] Vector2 meleeSize;
    [SerializeField] float meleeDist;

    InputManager controls;
    PlayerAnimations animations;
    PlayerMovement movement;

    Camera mainCam;
    public bool isMelee;

    private void Awake()
    {
        animations = GetComponent<PlayerAnimations>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        mainCam = Camera.main;
        controls = GameManager.InputManager;

        bowTransform.gameObject.SetActive(!isMelee);
        meleeAttackTransform.gameObject.SetActive(isMelee);
    }

    private void Update()
    {
        if (controls.SwitchWeapon)
        {
            isMelee = !isMelee;
            animations.OnChangeWeapon(isMelee);

            bowTransform.gameObject.SetActive(!isMelee);
            meleeAttackTransform.gameObject.SetActive(isMelee);
        }

        if (controls.IsInteracting)
        {
            animations.OnUseAbility();
        }

        Vector3 dir = (mainCam.ScreenToWorldPoint(controls.MousePos) - transform.position);
        dir.z = 0;
        bowTransform.position = transform.position + dir.normalized * bowDist;
        bowTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

        Vector2 moveDir = movement.MoveDir;
        meleeAttackTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg);
    }

    public bool OnTransportEnemies()
    {
        abilityCircle.color = new Color(1, 1, 1, 0);
        var results = Physics2D.OverlapCircleAll(transform.position, abilityRadius, abilityLayerMask);
        if (results.Length > 0)
        {
            ShootEmUp_EnemySpawner.EnemyCount = results.Length;

            foreach (var enemy in results)
            {
                enemy.GetComponent<BaboEnemy>().OnDeadAnimDone();
            }

            return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        var originalColor = Gizmos.color;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, abilityRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up * meleeDist, new Vector3(meleeSize.x, meleeSize.y, 1));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.right * meleeDist, new Vector3(meleeSize.y, meleeSize.x, 1));
        Gizmos.color = originalColor;
    }
}
