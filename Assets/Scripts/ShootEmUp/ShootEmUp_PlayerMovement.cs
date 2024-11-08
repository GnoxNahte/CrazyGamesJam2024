using UnityEngine;
using VInspector;

public class ShootEmUp_PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 moveDir;
    [SerializeField] float maxAngle;
    [SerializeField] float angleSpeed = 0.5f;

    public Vector2 MoveDir => moveDir;
    public float Angle => currAngle;
    float currAngle;
    float currAngleVelocity;

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
        moveDir = input.MoveDir;

        float targetAngle = -input.MoveDir.x * maxAngle;
        currAngle = Mathf.SmoothDampAngle(currAngle, targetAngle, ref currAngleVelocity, angleSpeed);

        rb.MovePositionAndRotation(rb.position + moveDir * speed * Time.deltaTime, currAngle);
        
    }
}
