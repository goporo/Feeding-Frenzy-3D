using UnityEngine;

public class AIFish : Fish
{
    private float patrolRange = 100f; // Range within which to generate random waypoints
    private float rotationSpeed = 5f; // Speed to rotate towards the next waypoint
    private int numWaypoints = 4; // Number of random waypoints to generate
    private float fleeDuration = 2f; // Duration for which the fish will flee
    private Vector3[] waypoints; // Array of randomly generated waypoint positions
    private int currentWaypointIndex; // Index of the current waypoint
    private bool isFleeing; // Flag indicating if the fish is currently fleeing
    private bool isChasing;
    private float currentSwimSpeed; // The normal swim speed
    private float fleeTimer; // Timer for tracking the duration of fleeing


    private void Start()
    {
        GenerateRandomWaypoints();
        currentWaypointIndex = 0;
        isFleeing = false;
        currentSwimSpeed = swimSpeed;
        fleeTimer = 0f;
    }

    private void Update()
    {
        if (!isFleeing && !isChasing)
        {
            RotateTowardsWaypoint();
            SwimTowardsWaypoint();
        }
        else if (isFleeing)
        {
            FleeFromPlayer();
            UpdateFleeTimer();
        }
        else if (isChasing)
        {
            ChasePlayer();
        }
    }
    private void UpdateFleeTimer()
    {
        fleeTimer += Time.deltaTime;

        if (fleeTimer >= fleeDuration)
        {
            fleeTimer = 0f;
            isFleeing = false;
            currentSwimSpeed = swimSpeed;
            currentWaypointIndex = GetRandomWaypointIndex();
        }
    }

    private void GenerateRandomWaypoints()
    {
        waypoints = new Vector3[numWaypoints];

        for (int i = 0; i < numWaypoints; i++)
        {
            Vector3 randomDirection;
            float terrainHeight;

            do
            {
                randomDirection = Random.insideUnitSphere * patrolRange;
                randomDirection.y = 0f; // Set the Y-coordinate to zero

                float yVariation = Random.Range(-5f, 5f); // Random Y-coordinate variation within +-5 units
                randomDirection.y += yVariation;

                Vector3 randomPosition = transform.position + randomDirection;
                terrainHeight = Terrain.activeTerrain.SampleHeight(randomPosition) + Terrain.activeTerrain.transform.position.y;
            }
            while (randomDirection.y + this.transform.position.y < terrainHeight);

            waypoints[i] = transform.position + randomDirection;
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
            transform.position += direction * currentSwimSpeed * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) < 1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void FleeFromPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        Vector3 fleeDirection = transform.position - playerObject.transform.position;
        fleeDirection.y = 0f; // Ignore vertical component

        if (fleeDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(fleeDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        transform.position += transform.forward * currentSwimSpeed * Time.deltaTime;
    }
    private void ChasePlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Vector3 direction = playerObject.transform.position - transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

                direction.Normalize();
                transform.position += direction * currentSwimSpeed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Skip interactions between AI fishes
        if (other.transform.parent && other.transform.parent.CompareTag("Player"))
        {
            // if player fish size bigger, other fish flee 
            if (!isFleeing && this.GetSize() < other.GetComponentInParent<Fish>().GetSize())
            {
                isFleeing = true;
                currentSwimSpeed = sprintSpeed;
            }

            // if player fish size smaller, other fish chase player
            if (this.GetSize() > other.GetComponentInParent<Fish>().GetSize())
            {
                isChasing = true;
                currentSwimSpeed = sprintSpeed;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (isChasing)
        {
            isChasing = false;
            currentSwimSpeed = swimSpeed;
        }

    }
    private int GetRandomWaypointIndex()
    {
        int randomIndex = currentWaypointIndex;

        while (randomIndex == currentWaypointIndex)
        {
            randomIndex = Random.Range(0, waypoints.Length);
        }
        return randomIndex;
    }
}
