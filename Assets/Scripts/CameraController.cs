using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] public GameObject target;
    [SerializeField] float smoothing;
    [SerializeField] public Vector3 offset;

    [Header("Tracking")]
    [SerializeField] bool trackX;
    [SerializeField] bool trackY;
    [SerializeField] Vector2 maxBounds;
    [SerializeField] Vector2 minBounds;

    Vector3 targetPosition;
    Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target != null)
        {
            MoveCamera();
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x), Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y), -10);
    }

    void MoveCamera()
    {
        transform.position = Vector3.SmoothDamp(transform.position, GetTargetPosition(), ref velocity, smoothing);
    }

    Vector3 GetTargetPosition()
    {
        if (trackX && trackY) // Track both axis
        {
            if (target != null)
            {
                targetPosition = new Vector3
                (
                Mathf.Clamp(target.transform.position.x + transform.localScale.x * offset.x, minBounds.x, maxBounds.x),
                target.transform.position.y + offset.y,
                transform.position.z + offset.z
                );
            }
        }
        else if (trackX && !trackY) // Track X But not Y Axis
        {
            if (target != null)
            {
                targetPosition = new Vector3
                (
                Mathf.Clamp(target.transform.position.x + transform.localScale.x * offset.x, minBounds.x, maxBounds.x),
                0,
                transform.position.z + offset.z
                );
            }
        }
        else if (!trackX && trackY) // Track Y But not X Axis
        {
            if (target != null)
            {
                targetPosition = new Vector3
                (
                0,
                target.transform.position.y,
                transform.position.z
                ) + new Vector3(offset.x * target.transform.localScale.x, offset.y, offset.z);
            }
        }
        return targetPosition;
    }

    /*
    public IEnumerator Transition(int level)
    {
        Animator animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("Out");
        AudioManager.instance.PlaySFX(AudioManager.instance.swooshClip);

        yield return new WaitForSeconds(1);

        if (SceneManager.sceneCountInBuildSettings >= level)
        {
            SceneManager.LoadScene(level);
        }
    }
    */
}
