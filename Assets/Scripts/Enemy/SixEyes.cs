using UnityEngine;

public class SixEyes : BasicEnemy
{
    [SerializeField] Vector2 attackSize;
    [SerializeField] Transform attackPos;
    [SerializeField] LayerMask attackLayerMask;
    [SerializeField] int dmgAmt;

    protected override void Attack()
    {
        Player player = Physics2D.OverlapBox(attackPos.position, attackSize, 0, attackLayerMask)?.GetComponent<Player>();

        if (player == null)
            return;

        player.TakeDamage(dmgAmt);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Color originalColor = Gizmos.color;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPos.position, attackSize);

        Gizmos.color = originalColor;
    }
}