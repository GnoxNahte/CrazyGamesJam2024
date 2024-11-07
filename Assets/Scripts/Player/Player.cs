using UnityEngine;

public class Player : EntityBase
{
    [SerializeField] RectTransform healthBarUI;

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);

        healthBarUI.anchorMax = new Vector2(health / MaxHealth, healthBarUI.anchorMax.y);
    }
}
