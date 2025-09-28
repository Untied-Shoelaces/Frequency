using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GridMovment))]
public class PlayerControler : MonoBehaviour
{
    [SerializeField] private InputAction playerControls;

    private GridMovment gridMovment;

    void Start()
    {
        gridMovment = GetComponent<GridMovment>();
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
        if (gridMovment.pointsToMove.Count != 0) return;
        if (moveDirection == Vector2.zero) return;

        Vector3 lastPoint;
        if(gridMovment.pointsToMove.Count != 0) lastPoint = gridMovment.pointsToMove.Last();
        else lastPoint = transform.position;

        if (moveDirection.x != 0)
        {
            Vector3 newPoint = new(lastPoint.x + moveDirection.x, lastPoint.y, lastPoint.z);
            if(gridMovment.MoveTo(newPoint)) lastPoint = newPoint;
        }
        if(moveDirection.y != 0)
        {
            Vector3 newPoint = new(lastPoint.x, lastPoint.y + moveDirection.y, lastPoint.z);
            gridMovment.pointsToMove.Enqueue(newPoint);
        }
    }

    void FixedUpdate()
    {
        HandleInput();
    }
}
