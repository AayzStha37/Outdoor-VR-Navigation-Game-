using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints to follow
    private int currentWaypoint = 0;

    private Rigidbody rigidBody ;
    
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }
    

    void Update()
    {
        // Check if there are waypoints to follow
        if (waypoints.Length == 0)
            return;

        // Calculate the direction to the current waypoint
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;

        // Move towards the current waypoint
        transform.Translate(direction.normalized * 4 * Time.deltaTime, Space.World);

        // Check if the object is close enough to the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.2f)
        {
            // Switch to the next waypoint
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
}
