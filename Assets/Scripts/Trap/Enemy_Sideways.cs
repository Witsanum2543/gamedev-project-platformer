using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private bool upDown;

    // moving left right
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    // moving up down
    private bool movingUp;
    private float upperEdge;
    private float lowerEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;

        upperEdge = transform.position.y - movementDistance;
        lowerEdge = transform.position.y + movementDistance;
    }

    private void Update()
    {
        if (upDown)
        {
            if (movingUp)
            {
                if (transform.position.y > upperEdge)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
                }
                else movingUp = false;
            }
            else 
            {
                if (transform.position.y < lowerEdge)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
                }
                else movingUp = true;
            }
        }
        else
        {
            if (movingLeft)
            {
                if (transform.position.x > leftEdge)
                {
                    transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else movingLeft = false;
            }
            else 
            {
                if (transform.position.x < rightEdge)
                {
                    transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
