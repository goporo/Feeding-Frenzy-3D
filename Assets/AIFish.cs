using UnityEngine;

public class AIFish : Fish
{
    [SerializeField] private int numWaypoints = 3; // Number of random waypoints to generate
    [SerializeField] private float patrolRange = 100f; // Range within which to generate random waypoints
    [SerializeField] private float rotationSpeed = 5f; // Speed to rotate towards the next waypoint

    private Vector3[] waypoints; // Array of randomly generated waypoint positions
    private int currentWaypointIndex; // Index of the current waypoint

    private void Start()
    {
        GenerateRandomWaypoints();
        currentWaypointIndex = 0;
    }

    private void Update()
    {
        RotateTowardsWaypoint();
        SwimTowardsWaypoint();
    }

    private void GenerateRandomWaypoints()
    {
        waypoints = new Vector3[numWaypoints];

        for (int i = 0; i < numWaypoints; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
            Vector3 randomPosition = transform.position + randomDirection;
            waypoints[i] = randomPosition;
        }
    }

    private void RotateTowardsWaypoint()
    {
        Vector3 direction = waypoints[currentWaypointIndex] - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void SwimTowardsWaypoint()
    {
        Vector3 direction = waypoints[currentWaypointIndex] - transform.position;

        if (direction != Vector3.zero)
        {
            direction.Normalize();
            transform.position += direction * swimSpeed * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) < 1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
