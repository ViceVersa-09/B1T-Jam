using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;

    [HideInInspector] public bool jumping;

    [HideInInspector] public Rigidbody2D rb;
    InputAction moveAction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        if (moveAction.IsPressed() && !jumping)
        {
            MovePlayer(moveAction.ReadValue<Vector2>());
        }
        else if (!moveAction.IsPressed() && !jumping)
        {
            MovePlayer(Vector2.zero);
        }
    }

    void MovePlayer(Vector2 moveVector)
    {
        rb.linearVelocity = moveVector * moveSpeed;
    }
}
