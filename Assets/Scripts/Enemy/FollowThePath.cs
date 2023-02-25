using UnityEngine;

public class FollowThePath : MonoBehaviour {


    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed = 2f;

    // Index of current waypoint from which Enemy walks to the next one
    private int waypointIndex = 0;
    private bool reverseMove = false;

	
	private void Update () {
        Move();
	}

    private void Move()
    {
        // Check if reverse move is required
        if (waypointIndex == waypoints.Length && !reverseMove) {
            reverseMove = true;
            waypointIndex--;
        } else if (waypointIndex == 0 && reverseMove) {
            reverseMove = false;
            waypointIndex++;
        }

        // Move the object towards the next waypoint
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position,
            moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            // Update the waypoint index based on the direction of movement
            if (reverseMove) {
                waypointIndex--;
            } else {
                waypointIndex++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Health playerHealth = collision.GetComponent<Health>();
            playerHealth.TakeDamage(playerHealth.currentHealth);
        }
    }
}