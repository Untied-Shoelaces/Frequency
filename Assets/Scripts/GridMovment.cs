using System.Collections.Generic;
using UnityEngine;

public class GridMovment : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.15f;
    [SerializeField] private LayerMask obstacleLayer;

    public Queue<Vector3> pointsToMove = new();

    public bool MoveTo(Vector3 point)
    {
        if(Physics2D.OverlapCircle(point, 0.4f, obstacleLayer)) return false;
        pointsToMove.Enqueue(point);
        return true;
    }

    void HandleMove()
    {
        if (pointsToMove.Count == 0) return;
        if (transform.position == pointsToMove.Peek())
        {
            pointsToMove.Dequeue();
            return;
        }
        Vector3 pointToMove = pointsToMove.Peek();
        transform.position = Vector3.MoveTowards(transform.position, pointToMove, moveSpeed);
    }

    void FixedUpdate()
    {
        HandleMove();
    }
}
