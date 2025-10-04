using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerControler : MonoBehaviour
{
    [SerializeField] private InputAction playerControls;

    private bool isMoving;

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

    void HandleInput()
    {
        Vector2 moveDirection = playerControls.ReadValue<Vector2>();
        moveDirection = new Vector2(Mathf.Round(moveDirection.x), Mathf.Round(moveDirection.y));
        if (moveDirection == Vector2.zero) return;
        if (moveDirection.y != 0 && moveDirection.x != 0) return;
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Idle") return;

        if(moveDirection.y > 0) animator.Play("MoveUp");
        else if(moveDirection.y < 0) animator.Play("MoveDown");
        else if(moveDirection.x > 0) animator.Play("MoveRight");
        else if(moveDirection.x < 0) animator.Play("MoveLeft");
    }

    public void CorrectPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x * 10f) / 10f;
        pos.y = Mathf.Round(pos.y * 10f) / 10f;
        transform.position = pos;
    }

    void FixedUpdate()
    {
        HandleInput();
    }
}
