using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fence : MonoBehaviour
{
    [Header("Jump Animation")]
    [SerializeField] Vector2 jumpForce;

    [Header("Colliders")]
    [SerializeField] Collider2D leftCol;
    [SerializeField] Collider2D rightCol;

    bool canJump;

    InputAction jumpAction;
    PlayerController playerController;
    Collider2D currentCol;
    Vector2 ogJumpForce;

    private void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        ogJumpForce = jumpForce;
    }

    private void Update()
    {
        if (jumpAction.WasPressedThisFrame() && canJump && playerController != null)
        {
            StartCoroutine(Jump());
        }

        if (playerController != null && playerController.jumping)
        {
            leftCol.enabled = false;
            rightCol.enabled = false;
        }
        else
        {
            leftCol.enabled = true;
            rightCol.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canJump = true;
        playerController = other.GetComponent<PlayerController>();
        AssignColliders(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!playerController.jumping)
        {
            playerController = null;
        }

        canJump = false;
        currentCol = null;
    }

    IEnumerator Jump()
    {
        Debug.Log("Jump");
        playerController.jumping = true;

        playerController.transform.localPosition = (Vector2)transform.localPosition + currentCol.offset;
        playerController.rb.gravityScale = 2;
        playerController.rb.linearVelocity = jumpForce;

        yield return new WaitUntil(() => playerController.transform.localPosition.y != transform.localPosition.y);

        while (playerController.transform.localPosition.x - transform.localPosition.x > 0.2)
        {
            playerController.rb.linearVelocity = jumpForce;
            yield return new WaitForEndOfFrame();
        }

        jumpForce.y = -ogJumpForce.y;

        while (playerController.transform.localPosition.y - transform.localPosition.y > 0.2)
        {
            playerController.rb.linearVelocity = jumpForce;
            yield return new WaitForEndOfFrame();
        }

        jumpForce = ogJumpForce;
        playerController.rb.gravityScale = 0;
        playerController.jumping = false;
        playerController = null;
    }

    void AssignColliders(Collider2D other)
    {
        if (other.IsTouching(rightCol))
        {
            currentCol = rightCol;
            jumpForce.x = -ogJumpForce.x;
        }
        else if (other.IsTouching(leftCol))
        {
            currentCol = leftCol;
            jumpForce.x = ogJumpForce.x;
        }
    }
}
