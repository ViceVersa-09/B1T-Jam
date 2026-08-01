using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fence : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] float jumpHeight = 1;
    [SerializeField] float forwardSpeed = 1;

    bool canJump;
    int playerDir;

    Vector2 colOffset;
    InputAction jumpAction;
    PlayerController playerController;
    Collider2D[] colliders;

    private void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        colliders = GetComponents<Collider2D>();
    }

    private void Update()
    {
        if (jumpAction.WasPressedThisFrame() && canJump && playerController != null)
        {
            StartCoroutine(Jump());
        }

        if (playerController != null && playerController.jumping)
        {
            foreach (var col in colliders)
            {
                col.enabled = false;
            }
        }
        else
        {
            foreach (var col in colliders)
            {
                col.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canJump = true;
        playerController = other.GetComponent<PlayerController>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!playerController.jumping)
        {
            playerController = null;
        }

        canJump = false;
    }

    IEnumerator Jump()
    {
        Debug.Log("Jump");
        playerController.jumping = true;
        CheckPlayerDirection();
        playerController.transform.position = (Vector2)transform.position + (colOffset * playerDir);
        playerController.rb.gravityScale = 2;

        float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * jumpHeight);
        Vector2 jumpVector = new Vector2(-playerDir * forwardSpeed, jumpVelocity);
        playerController.rb.AddForce(jumpVector, ForceMode2D.Impulse);

        yield return new WaitUntil(() => playerController.transform.position.y != transform.position.y);
        while (Mathf.Abs(playerController.transform.position.x - transform.position.x) > 0.2)
        {
            playerController.rb.linearVelocity = Vector2.zero;
            playerController.rb.AddForce(jumpVector, ForceMode2D.Impulse);
            yield return new WaitForEndOfFrame();
        }
        playerController.rb.linearVelocityY = 0;
        yield return new WaitUntil(() => Mathf.Abs(playerController.transform.position.y - transform.position.y) < 0.2);

        playerController.rb.gravityScale = 0;
        playerController.jumping = false;
        playerController = null;
    }

    void CheckPlayerDirection()
    {
        if (playerController.transform.position.x > transform.position.x)
        {
            playerDir = 1;
        }
        else if (playerController.transform.position.x < transform.position.x)
        {
            playerDir = -1;
        }
        else
        {
            playerDir = 0;
        }

        colOffset = Vector2.zero;
        colOffset.x = Mathf.Abs(colliders[0].offset.x);
    }
}
