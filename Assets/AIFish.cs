using UnityEngine;

public class AIFish : Fish
{
    [SerializeField] private int numWaypoints = 5; // Number of random waypoints to generate
    [SerializeField] private float patrolRange = 30f; // Range within which to generate random waypoints
    [SerializeField] private float rotationSpeed = 5f; // Speed to rotate towards the next waypoint
    [SerializeField] private float swimSpeed = 3f; // Speed at which the fish swims

    private Transform[] waypoints; // Array of randomly generated waypoints
    private int currentWaypointIndex; // Index of the current waypoint

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomWaypoints();
        currentWaypointIndex = 0;
        foreach (Transform waypoint in waypoints)
        {
            Debug.Log("Waypoint: " + waypoint.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate towards the current waypoint
        RotateTowardsWaypoint();

        // Move towards the current waypoint
        SwimTowardsWaypoint();

    }

    private void GenerateRandomWaypoints()
    {
        waypoints = new Transform[numWaypoints];

        for (int i = 0; i < numWaypoints; i++)
        {
            // Generate a random position within the patrol range
            Vector3 randomDirection = Random.insideUnitSphere * patrolRange;

            // Calculate the random waypoint position
            Vector3 randomPosition = transform.position + randomDirection;

            // Create an empty game object as the waypoint
            GameObject waypointObject = new GameObject("Waypoint" + i);
            waypointObject.transform.position = randomPosition;

            // Assign the waypoint object to the array
            waypoints[i] = waypointObject.transform;
        }
    }

    private void RotateTowardsWaypoint()
    {
        // Calculate the direction towards the current waypoint
        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;

        if (direction != Vector3.zero)
        {
            // Rotate towards the current waypoint with smooth rotation
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void SwimTowardsWaypoint()
    {
        // Calculate the direction towards the current waypoint
        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;

        if (direction != Vector3.zero)
        {
            // Normalize the direction vector and move towards the current waypoint
            direction.Normalize();
            transform.position += direction * swimSpeed * Time.deltaTime;
        }

        // Check if reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1f)
        {
            // Increment the waypoint index
            currentWaypointIndex++;

            // Reset the waypoint index if it exceeds the array length
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}
