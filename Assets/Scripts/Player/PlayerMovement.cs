using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 moveDir;

    public Vector2 MoveDir => moveDir;

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
        {
            moveDir = input.MoveDir;
            rb.MovePosition(rb.position + moveDir * speed * Time.deltaTime);
        }
    }
}
