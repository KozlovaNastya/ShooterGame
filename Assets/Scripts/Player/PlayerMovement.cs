using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private Vector2 movement;

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");

    private void Awake()
    {
        if(rb == null) rb = GetComponent<Rigidbody2D>();
        if(animator == null) animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) movement.y += 1;
        if (Input.GetKey(KeyCode.S)) movement.y -= 1;
        if (Input.GetKey(KeyCode.D)) movement.x += 1;
        if (Input.GetKey(KeyCode.A)) movement.x -= 1;

        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void UpdateAnimations()
    {
        if (animator == null) return;

        bool isMoving = movement.magnitude > 0.1f;
        animator.SetBool(IsMoving, isMoving);

        if (isMoving)
        {
            animator.SetFloat(MoveX, movement.x);
            animator.SetFloat(MoveY, movement.y);
        }
    }

    public Vector2 GetMovementDirection() => movement;
}
