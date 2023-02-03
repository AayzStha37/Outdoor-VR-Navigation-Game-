using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicleTagger : MonoBehaviour
{
    public bool vehicleBlockRegitered;

    public bool checkIfVehicleBlockRegitered(){
        return vehicleBlockRegitered;
    }
    public void registerVehicle(){
        vehicleBlockRegitered=true;
    }
    public void deRegisterVehicle(){
        vehicleBlockRegitered=false;
    }
}
