using UnityEngine;

public class Enemy_Sideways : EnemyDamage
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speedXAxis;
    [SerializeField] private float speedYAxis;
    [SerializeField] private bool upDown;

    // moving left right
    private bool movingLeft;
    private float leftBound;
    private float rightBound;

    // moving up down
    private bool movingUp;
    private float upperBound;
    private float lowerBound;

    private void Awake()
    {
        leftBound = transform.position.x - movementDistance;
        rightBound = transform.position.x + movementDistance;

        upperBound = transform.position.y - movementDistance;
        lowerBound = transform.position.y + movementDistance;
    }

    private void Update()
    {
        // Move along Y Axis
        if (movingUp) {
            if (transform.position.y > upperBound) {
                transform.position = new Vector3(transform.position.x, transform.position.y - speedYAxis * Time.deltaTime, transform.position.z);
            }
            else movingUp = false;
        }
        else {
            if (transform.position.y < lowerBound) {
                transform.position = new Vector3(transform.position.x, transform.position.y + speedYAxis * Time.deltaTime, transform.position.z);
            }
            else movingUp = true;
        }
        
        // Move along X Axis
        if (movingLeft) {
            if (transform.position.x > leftBound) {
                transform.position = new Vector3(transform.position.x - speedXAxis * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else movingLeft = false;
        }
        else {
            if (transform.position.x < rightBound) {
                transform.position = new Vector3(transform.position.x + speedXAxis * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else movingLeft = true;
        }
        
    }

}
