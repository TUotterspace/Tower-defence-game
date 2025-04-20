using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;      // Array to hold waypoints (SP1, SP2, SP3, etc.)
    public Transform spawnPoint;       // The spawn point where the enemy starts
    private int currentWaypointIndex = 0; // Index to track current waypoint
    public float moveSpeed = 5f;       // Speed of the enemy's movement

    public void Setup(Transform[] newWaypoints, Transform newSpawnPoint)
   {
        spawnPoint = newSpawnPoint;
        waypoints = newWaypoints;
        float difficulty = DifficultyScaler.GetDifficultyMultiplier();
        moveSpeed *= difficulty; // scale speed

        // Start the enemy at the spawn point (e.g., SP1, SP2, SP3, etc.)
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position; // Set the position to the spawn point
        }

        // If waypoints are available, start moving towards the first waypoint
        if (waypoints.Length > 0)
        {
            currentWaypointIndex = 0; // Start from the first waypoint
        }
    }

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            MoveTowardsWaypoint();
        }
    }

    void MoveTowardsWaypoint()
    {
        // Get the current waypoint the enemy is heading towards
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Move the enemy towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // Check if the enemy has reached the waypoint
        if (transform.position == targetWaypoint.position)
        {
            currentWaypointIndex++; // Move to the next waypoint
        }
    }
}