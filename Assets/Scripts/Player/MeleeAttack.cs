using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] int meleeDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BasicEnemy enemy = collision.GetComponent<BasicEnemy>();
        if (enemy)
            enemy.GetComponent<BasicEnemy>().TakeDamage(meleeDamage);
    }
}
