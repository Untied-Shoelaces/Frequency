using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerControler : MonoBehaviour
{
    [SerializeField] private InputAction playerControls;
    [SerializeField] private LayerMask obstacleLayer;

    private bool isSnappingPosition = false;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    void OnAnimatorMove()
    {
        if (isSnappingPosition) return; 

        // Standard Root Motion Application:
        if (animator.applyRootMotion)
        {
            Vector3 deltaPosition = animator.deltaPosition;
            transform.position += deltaPosition;
        }
    }

    public void SnapPosition()
    {
        isSnappingPosition = true;

        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Floor(newPosition.x) + 0.5f;
        newPosition.y = Mathf.Floor(newPosition.y) + 0.5f;
        transform.position = newPosition;

        StartCoroutine(ResetSnappingFlagNextFrame());
    }

    private IEnumerator ResetSnappingFlagNextFrame()
    {
        yield return null; 
        isSnappingPosition = false;
    }

    void HandleInput()
    {
        Vector2 moveDirection = playerControls.ReadValue<Vector2>();
        moveDirection = new Vector2(Mathf.Round(moveDirection.x), Mathf.Round(moveDirection.y));
        if (moveDirection == Vector2.zero) return;
        if (moveDirection.y != 0 && moveDirection.x != 0) return;
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Idle") return;

        if(Physics2D.OverlapCircle(transform.position + (Vector3)moveDirection, 0.4f, obstacleLayer))
        {
            return;
        }

        if (moveDirection.y > 0) animator.Play("MoveUp");
        else if (moveDirection.y < 0) animator.Play("MoveDown");
        else if (moveDirection.x > 0) animator.Play("MoveRight");
        else if (moveDirection.x < 0) animator.Play("MoveLeft");
    }

    void FixedUpdate()
    {
        HandleInput();
    }
}
