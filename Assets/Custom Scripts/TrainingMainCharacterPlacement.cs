using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingMainCharacterPlacement : MonoBehaviour
{
    private Vector3 session1CustomPosition = new Vector3(-355.570007f,-4.86755228f,46.2699738f);
    private Vector3 session1CustomRotation = new Vector3(0f,-120.6f,0f);
    private Vector3 session2CustomPosition = new Vector3(-469.329987f,-5.07700014f,8.68000031f);
    private Vector3 session2CustomRotation = new Vector3(0f,0,0f);
    public GameObject mainCharacterGameobjectToMove; 
    private string KATDemoWalker = "KATDemoWalker";

    public void PlaceMainCharacterInFirstSession()
    {
        if(mainCharacterGameobjectToMove==null)
            Debug.LogError("The main character gameobject reference is null!");
        else    
            mainCharacterGameobjectToMove.transform.position = session1CustomPosition;
            mainCharacterGameobjectToMove.transform.rotation = Quaternion.Euler(session1CustomRotation);
    }
    public void PlaceMainCharacterInSecondSession()
    {
        if(mainCharacterGameobjectToMove==null)
            Debug.LogError("The main character gameobject reference is null!");
        else{
            mainCharacterGameobjectToMove.transform.position = session2CustomPosition;
            mainCharacterGameobjectToMove.transform.rotation = Quaternion.Euler(session2CustomRotation);
            FindTargetObjectToDiable();
        }
    }

    private void FindTargetObjectToDiable(){
        GameObject targetObject = GameObject.Find(KATDemoWalker);
        if (targetObject != null)
        {
           targetObject.SetActive(false);
        }else{
            Debug.LogWarning("GameObject with name '" + KATDemoWalker + "' not found!");
        }
    }
}
