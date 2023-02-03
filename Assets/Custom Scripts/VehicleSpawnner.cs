using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawnner : MonoBehaviour
{
    public List<GameObject> spawnItems;
    public  float frequency;
    public float initalSpeed;
    float lastSpawnedTime = 1.0f;
    public bool isSpawn = true;
    private bool haultedVehicles = false;

    [Obsolete]
    void Update()
    {
        if(Time.time > lastSpawnedTime * frequency && isSpawn){
           SpawnVehicle(spawnItems[UnityEngine.Random.Range(0,spawnItems.Count)]);
        }else if (!isSpawn){
            haultedVehicles = true;
            //TODO refactor block
            for(int i=0;i<this.transform.GetChildCount();i++){
                GameObject childObj = this.transform.GetChild(i).gameObject;
                if(childObj.GetComponent<vehicleTagger>().checkIfVehicleBlockRegitered()){
                    childObj.GetComponent<Rigidbody>().velocity = transform.forward*0;
                }
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
                childObj.GetComponent<Rigidbody>().velocity = transform.forward*-1*initalSpeed;
            }
       }
       GameObject newSpawnObj = Instantiate(spawnItem, transform.position, Quaternion.Euler(0,180,0));
       newSpawnObj.name +=  "_"+uniquieIDGenerator();
       newSpawnObj.GetComponent<Rigidbody>().velocity = transform.forward*-1*initalSpeed;
       newSpawnObj.transform.parent = transform; 
       lastSpawnedTime = Time.time;
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