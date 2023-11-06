using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawnner : MonoBehaviour
{
    public List<GameObject> spawnItems;
    public GameObject[] waypoints;
    public  float spawnInterval;
    public float initalSpeed;
    float timeSinceLastSpawn = 0.0f;
    private bool isSpawn = true;
    private bool haultedVehicles = false;

    int currentWayPoint=0; 
    [Obsolete]
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval && isSpawn)
        {
            SpawnVehicle(spawnItems[UnityEngine.Random.Range(0,spawnItems.Count)]);
            timeSinceLastSpawn = 0.0f; // Reset the timer
        }else if (!isSpawn){
            haultedVehicles = true;
            int i;
            //TODO refactor block
            for(i=0;i<this.transform.GetChildCount();i++){
                GameObject childObj = this.transform.GetChild(i).gameObject;
                if(childObj.GetComponent<vehicleTagger>().checkIfVehicleBlockRegitered()){
                    break;
                }
            }
            for(int j=i;j<this.transform.GetChildCount();j++){
                GameObject childObj = this.transform.GetChild(j).gameObject;
                childObj.GetComponent<VehicleWayPointFollower>().shouldMove = false;
            }

        }
    }

    [Obsolete]
    private void SpawnVehicle(GameObject spawnItem)
    {
        //TODO refactor block
       if(haultedVehicles){
            for(int i=0;i<this.transform.GetChildCount();i++){
                GameObject childObj = this.transform.GetChild(i).gameObject;
                childObj.GetComponent<VehicleWayPointFollower>().shouldMove = true;
            }
       }
       GameObject newSpawnObj = Instantiate(spawnItem, transform.position, Quaternion.Euler(0,180,0));
       newSpawnObj.transform.SetParent(this.transform);
       VehicleWayPointFollower scriptComponent = newSpawnObj.AddComponent<VehicleWayPointFollower>();
       scriptComponent.InitializeWayPointsVal(waypoints,initalSpeed,newSpawnObj.GetComponent<Rigidbody>());
       newSpawnObj.name +=  "_"+uniquieIDGenerator();
       newSpawnObj.transform.Rotate(0f, 180f, 0f);
    }

    private string uniquieIDGenerator()
    {
        string uniquieID = "";
        string charSet = "0123456789abcdefghijklmnopqrstuvwxABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for(int i=0;i<5;i++){
            uniquieID += charSet[UnityEngine.Random.Range(0,charSet.Length)];
        }
        return uniquieID;
    }

    public void setSpawnVal(bool val){
        isSpawn = val;
    }
}
