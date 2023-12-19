using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeRegisterBlockID : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<VehicleTagger>().deRegisterVehicle();
    }
}
