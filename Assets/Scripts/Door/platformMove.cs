using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMove : MonoBehaviour
{
    public enum MovementDirection { Horizontal, Vertical };

    public MovementDirection direction = MovementDirection.Vertical;
    public float speed = 2.0f;
    public float distance = 3.0f;
    public float cooldownDuration = 1.0f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMovingForward = true;
    private bool isCooldown = false;
    private float cooldownTimer = 0.0f;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = GetTargetPosition();
    }

    private void FixedUpdate()
    {
        if (isCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownDuration)
            {
                isCooldown = false;
                cooldownTimer = 0.0f;
            }
        }
        else
        {
            float distanceMoved = speed * Time.deltaTime;
            Vector3 directionVector = (targetPosition - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceMoved >= distanceToTarget)
            {
                transform.position = targetPosition;
                isCooldown = true;
            }
            else
            {
                transform.position += directionVector * distanceMoved;
            }

            if (!isCooldown && distanceToTarget <= 0.1f)
            {
                isMovingForward = !isMovingForward;
                targetPosition = GetTargetPosition();
                isCooldown = true;
            }
        }
    }

    private Vector3 GetTargetPosition()
    {
        switch (direction)
        {
            case MovementDirection.Horizontal:
                return isMovingForward ? startPosition + new Vector3(distance, 0, 0) : startPosition;

            case MovementDirection.Vertical:
            default:
                return isMovingForward ? startPosition - new Vector3(0, distance, 0) : startPosition;
        }
    }


}
