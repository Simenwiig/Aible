using UnityEngine;
namespace CorCoreInsects
{

    public class CurvyPathFollowing : MonoBehaviour
{
    public Transform[] waypoints;   // Array of waypoints defining the path
    public int subdivisions = 10;   // Number of subdivisions between waypoints
    public float speed = 5f;        // Speed at which the object moves along the path
    private int currentWaypoint = 0; // Index of the current waypoint

    private Vector3[] splinePoints;  // Array to store the interpolated spline points
    private LineRenderer lineRenderer; // LineRenderer component for visualization

    void Start()
    {
        // Initialize the spline points array and LineRenderer
        CalculateSplinePoints();
    }

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

    void CalculateSplinePoints()
    {
        // Calculate additional points using Catmull-Rom spline interpolation
        splinePoints = new Vector3[(waypoints.Length - 1) * subdivisions + 1];
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            for (int j = 0; j < subdivisions; j++)
            {
                float t = j / (float)subdivisions;
                splinePoints[i * subdivisions + j] = CatmullRomSpline(
                    waypoints[WrapIndex(i - 1)].position,
                    waypoints[i].position,
                    waypoints[(i + 1) % waypoints.Length].position,
                    waypoints[WrapIndex(i + 2)].position,
                    t
                );
            }
        }
        splinePoints[splinePoints.Length - 1] = waypoints[waypoints.Length - 1].position;
    }

    Vector3 CatmullRomSpline(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float tt = t * t;
        float ttt = tt * t;
        return 0.5f * ((2.0f * p1) +
                       (-p0 + p2) * t +
                       (2.0f * p0 - 5.0f * p1 + 4.0f * p2 - p3) * tt +
                       (-p0 + 3.0f * p1 - 3.0f * p2 + p3) * ttt);
    }

    void MoveAndRotateTowardsWaypoint()
    {
        // Get the current waypoint position
        Vector3 targetPosition = splinePoints[currentWaypoint];

        // Calculate the direction to the waypoint
        Vector3 directionToWaypoint = targetPosition - transform.position;

        // Calculate the angle between the current forward direction and the direction to the waypoint
        float angle = Mathf.Atan2(directionToWaypoint.y, directionToWaypoint.x) * Mathf.Rad2Deg;

        // Rotate only along the Z-axis
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the object has reached the waypoint
        if (transform.position == targetPosition)
        {
            // Increment to the next waypoint
            currentWaypoint++;

            // Reset to the first waypoint if at the end of the path
            if (currentWaypoint == splinePoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }

    int WrapIndex(int index)
    {
        // Helper function to wrap index around the waypoints array
        return (index + waypoints.Length) % waypoints.Length;
    }
}
}