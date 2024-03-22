using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingMainCharacterPlacement : MonoBehaviour
{
    private Vector3 firstTask2CustomPosition = new Vector3(-200.5f,-5.89997101f,52.1100006f);
    private Vector3 firstTask2CustomRotation = new Vector3(0f,197.875702f,0f);
    public GameObject mainCharacterGameobjectToMove; 

    public void PlaceMainCharacter()
    {
        if(mainCharacterGameobjectToMove==null)
            Debug.LogError("The main character gameobject reference is null!");
        else    
            mainCharacterGameobjectToMove.transform.position = firstTask2CustomPosition;
            mainCharacterGameobjectToMove.transform.rotation = Quaternion.Euler(firstTask2CustomRotation);
    }
}
