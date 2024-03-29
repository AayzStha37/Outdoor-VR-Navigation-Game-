using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingMainCharacterPlacement : MonoBehaviour
{
    private Vector3 firstTask2CustomPosition = new Vector3(-355.570007f,-4.86755228f,46.2699738f);
    private Vector3 firstTask2CustomRotation = new Vector3(0f,-120.6f,0f);
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
