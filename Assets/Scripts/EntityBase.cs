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
        health = maxHealth;
    }

    virtual public void TakeDamage(int value)
    {
        health -= value;
    }

    virtual public void OnDead()
    {
        Destroy(gameObject);
    }
}
