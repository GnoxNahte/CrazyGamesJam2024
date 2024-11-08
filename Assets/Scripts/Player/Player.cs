using UnityEngine;

public class Player : EntityBase
{
    [SerializeField] RectTransform healthBarUI;
    [SerializeField] PlayerBow bow;
    bool isDead = false;
    public PlayerBow Bow => bow;

    protected override void OnEnable()
    {
        base.OnEnable();

        healthBarUI.anchorMax = new Vector2((float)health / MaxHealth, healthBarUI.anchorMax.y);
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);

        healthBarUI.anchorMax = new Vector2((float)health / MaxHealth, healthBarUI.anchorMax.y);
    }

    public override void OnDead()
    {
        if (isDead) return;

        isDead = true;
        GameManager.EndScreen.ShowEndScreen();
    }
}
