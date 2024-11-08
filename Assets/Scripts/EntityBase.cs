using UnityEngine;
using VInspector;

public abstract class EntityBase : MonoBehaviour
{
    [SerializeField] int maxHealth;

    [ShowInInspector] [ReadOnly]
    public int health { get; private set; }
    public int MaxHealth => maxHealth;

    virtual protected void Start()
    {
        //health = maxHealth;
    }

    virtual protected void OnEnable()
    {
        health = maxHealth;
    }

    virtual public void TakeDamage(int value)
    {
        health -= value;

        if (health < 0)
            OnDead();
    }

    virtual public void OnDead()
    {
        //Destroy(gameObject);
    }

    virtual public void OnDeadAnimDone()
    {
        Destroy(gameObject);
    }
}
