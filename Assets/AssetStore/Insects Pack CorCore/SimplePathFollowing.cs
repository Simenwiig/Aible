using UnityEngine;
namespace CorCoreInsects
{

    public class SimplePathFollowing : MonoBehaviour
{
    public Transform[] waypoints;   // Array of waypoints defining the path
    public float speed = 5f;        // Speed at which the object moves along the path
    private int currentWaypoint = 0; // Index of the current waypoint


    void Update()
    {
        // Check if there are waypoints in the array
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned to the path.");
            return;
        }

        // Move towards the current waypoint and rotate towards it
        MoveAndRotateTowardsWaypoint();
    }

    void MoveAndRotateTowardsWaypoint()
    {
        // Get the current waypoint position
        Vector3 targetPosition = waypoints[currentWaypoint].position;

        // Calculate the direction to the waypoint
        Vector3 directionToWaypoint = targetPosition - transform.position;

        // Calculate the angle between the current forward direction and the direction to the waypoint
        float angle = Mathf.Atan2(directionToWaypoint.y, directionToWaypoint.x) * Mathf.Rad2Deg;

        // Rotate only along the Z-axis, adding 90 degrees to the calculated angle
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the object has reached the waypoint
        if (transform.position == targetPosition)
        {
            // Increment to the next waypoint
            currentWaypoint++;

            // Reset to the first waypoint if at the end of the path
            if (currentWaypoint == waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
}
}