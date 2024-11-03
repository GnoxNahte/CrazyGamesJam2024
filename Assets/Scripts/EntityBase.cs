using UnityEngine;

public abstract class EntityBase 
{
    public int health;

    public void TakeDamage(int value)
    {
        health -= value;
    }

    abstract public void Dead();
}
