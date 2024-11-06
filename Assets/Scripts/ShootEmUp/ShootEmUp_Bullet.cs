using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootEmUp_Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    // Caching
    private Rigidbody2D rb;

    ShootEmUp_Gun gun;

    private void OnEnable()
    {
        gun = ShootEmUp_Mananger.Player.Gun;
    }

    public void Init(float speed, Color color)
    {
        //GetComponent<SpriteRenderer>().color = color;
        this.speed = speed;
        
        //rb.velocity = rb.transform.up * speed;
    }

    public void Init(float speed, Color color, float simulateSeconds)
    {
        //GetComponent<SpriteRenderer>().color = color;
        this.speed = speed;

        //rb.velocity = rb.transform.up * speed;
        transform.position = transform.position + transform.up * speed * simulateSeconds;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.position = transform.position + transform.up * speed * Time.deltaTime;
    }

    private void SimulateTime(float seconds)
    {
        transform.position = transform.position + transform.up * speed * seconds;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gun.ReleaseBullet(gameObject);
    }
}