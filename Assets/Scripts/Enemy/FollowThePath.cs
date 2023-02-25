using UnityEngine;

public class FollowThePath : MonoBehaviour {


    [SerializeField] private Transform[] waypoints;

    [SerializeField] private float moveSpeed = 2f;

    // Index of current waypoint from which Enemy walks to the next one
    private int waypointIndex = 0;

	private void Start () {
        // Set position of Enemy as position of the first waypoint
        // transform.position = waypoints[waypointIndex].transform.position;
	}
	
	private void Update () {
        Move();
	}

    private void Move()
    {

        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
}