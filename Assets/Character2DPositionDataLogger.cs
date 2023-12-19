using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DPositionDataLogger : MonoBehaviour
{
    void Update()
    {
        Vector3 position = transform.position;
        Debug.Log($"POSLOG: X: {position.x}, Y:{position.y}, Z: {position.z}"); 
    }
}
