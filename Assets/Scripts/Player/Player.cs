using UnityEngine;

public class Player : EntityBase
{
    [SerializeField] RectTransform healthBarUI;
    [SerializeField] PlayerBow bow;

    public PlayerBow Bow => bow;
     
    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);

        healthBarUI.anchorMax = new Vector2((float)health / MaxHealth, healthBarUI.anchorMax.y);
    }
}
