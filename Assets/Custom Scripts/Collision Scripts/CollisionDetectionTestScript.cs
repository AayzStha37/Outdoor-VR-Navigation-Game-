using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionTest : MonoBehaviour
{
    public GameObject primaryObject;
    public List<GameObject> gameObjectList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         foreach(var gameObject in gameObjectList){
            if (CollisionDetectionCustomScript.IsTouching(primaryObject, gameObject)){
                Debug.Log("<color=green>**Collision!**</color>");
            }
            else{
                Debug.Log("<color=red>##NO Collision!##</color>");
            }
        }
    }
}
