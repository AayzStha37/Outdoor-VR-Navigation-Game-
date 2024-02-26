using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleWayPointFollower : MonoBehaviour
{
    public GameObject[] waypoints; // Array of waypoints to follow
    private int currentWaypoint = 0;
    private float initalSpeed;
    private Rigidbody rb;
    public bool shouldMove = true;
    public void InitializeWayPointsVal(GameObject[] waypointList, float speed, Rigidbody rigidBody){
            this.waypoints = waypointList;  
            this.initalSpeed = speed;
            this.rb = rigidBody;
    }

    void Update()
    {
        // Check if there are waypoints to follow
        if (waypoints.Length == 0)
            rb.velocity = transform.forward*initalSpeed;
        else if(shouldMove){
            // Check if the object is close enough to the current waypoint
            if (Vector3.Distance(this.transform.position, waypoints[currentWaypoint].transform.position) < 0.2f)
                currentWaypoint++;
            if(currentWaypoint<waypoints.Length){
                this.transform.LookAt(waypoints[currentWaypoint].transform);
                this.transform.Translate(0,0,initalSpeed*Time.deltaTime);
            }
            else{
                Destroy(this.gameObject);
            }    
        }
    }
}
