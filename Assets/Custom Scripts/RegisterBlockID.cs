using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterBlockID : MonoBehaviour
{
    public List<string> registeredVehicles;
    public List<GameObject> spawnnerList;
    public bool stopVehicles;
    
    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<VehicleTagger>().registerVehicle();
    }

    public List<string> getRegistredVehiclesIDs(){
        return registeredVehicles;
    }
    public void setStopVehicles(bool val){
        stopVehicles = val;
    }
  
    private void Update() {
        if(stopVehicles){
            foreach(GameObject obj in spawnnerList){
                obj.GetComponent<VehicleSpawnner>().setSpawnVal(false);
            }
        }else{
            foreach(GameObject obj in spawnnerList){
                obj.GetComponent<VehicleSpawnner>().setSpawnVal(true);
            }
        }
    }
}
