using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateOepn : MonoBehaviour
{   
    // If enemy died (gameObject deactivate) open the gate
    [SerializeField] GameObject enemy;

    public float speed = 2.0f;
    public float distance = 5.0f;
    private bool isOpen = false;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update() {

        // If using enemy for opening door
        if (enemy != null) {
            // If enemy object deactivate, open door
            if (enemy.activeSelf == false) {
                Open();
            }
        }

        // If using enter area to activate foor
        
    }

    void FixedUpdate()
    {
        if (isOpen) {
            // Calculate the target position based on the move distance and the initial position
            Vector3 targetPosition = startPosition - new Vector3(0, distance, 0);

            // Move the door towards the target position using Lerp
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the door has reached the target position
            if (transform.position.y <= targetPosition.y)
            {
                isOpen = false;
            }
        }
    }

    public void Open()
    {
        isOpen = true;
    }
}
