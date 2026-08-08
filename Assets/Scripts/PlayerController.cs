using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpAngleClamp;

    [HideInInspector] public bool jumping;

    [HideInInspector] public Rigidbody2D rb;
    InputAction moveAction;
    Animator animator;
    SpriteRenderer spriteRenderer;
    InputAction jumpAction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (moveAction.IsPressed() && !jumping)
        {
            MovePlayer(moveAction.ReadValue<Vector2>());
            StartCoroutine(AudioManager.instance.WalkLoop());
        }
        else if (!moveAction.IsPressed() && !jumping)
        {
            MovePlayer(Vector2.zero);
        }

        Graphics();

        if (jumpAction.WasPressedThisFrame())
        {
            AudioManager.instance.PlaySFXPitched(AudioManager.instance.baahClip);
        }
    }

    void MovePlayer(Vector2 moveVector)
    {
        rb.linearVelocity = moveVector * moveSpeed;
    }

    void Graphics()
    {
        if (rb.linearVelocityX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.linearVelocityX > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (rb.linearVelocityX != 0 && !jumping)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));
            rb.rotation = 0;
        }
        else if (jumping)
        {
            // I don't know how this works, it just calculates something and converts it to degrees (except I added the velocity part)
            float rotationAngle = Mathf.Atan2(rb.linearVelocityY, rb.linearVelocityX) * Mathf.Rad2Deg 
                * Mathf.RoundToInt(Mathf.Clamp(rb.linearVelocityX, -1, 1));

            rb.rotation = Mathf.Clamp(rotationAngle, -jumpAngleClamp, jumpAngleClamp);
        }
        else
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocityY));
            rb.rotation = 0;
        }

        animator.SetBool("Jump", jumping);
    }
}
