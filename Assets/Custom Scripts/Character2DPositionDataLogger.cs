using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DPositionDataLogger : MonoBehaviour
{
    private bool isTrackable = false;

    void Start(){
        isTrackable = false;
    }
    public void SetTrackablePositionLog(bool val){
        isTrackable = val;
    }

    void Update()
    {
        if((gameObject.scene.name.Equals(Constants.TASK2_SCENE_NAME) || gameObject.scene.name.Equals(Constants.TASK3_SCENE_NAME))
            && isTrackable){
            Vector3 position = transform.position;
            Debug.Log($"POSLOG: X: {position.x}, Y:{position.y}, Z: {position.z}"); 
        }
    }
}
