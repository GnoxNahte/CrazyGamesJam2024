using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

    InputManager input;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        input = GameManager.InputManager;
    }

    void FixedUpdate()
    {
        if (input)
            rb.MovePosition(rb.position + input.MoveDir * speed * Time.deltaTime);
    }
}
