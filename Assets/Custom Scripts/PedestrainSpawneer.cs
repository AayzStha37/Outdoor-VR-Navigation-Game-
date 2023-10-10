using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Input;
using UnityEditor.Animations;
using UnityEngine;

public class PedestrainSpawneer : MonoBehaviour
{
    public GameObject pedestrianModel;
    public List<GameObject> waypoints;
    public AnimatorController pedestrianAnimation;
     public  float spawnInterval;
    float timeSinceLastSpawn = 0.0f;
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        pedestrianModel.transform.GetComponent<Animator>().runtimeAnimatorController = pedestrianAnimation;
    }

    // Update is called once per frame
    void Update()
    {
         timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            GameObject spawnedPedestrian = Instantiate(pedestrianModel,transform.position,Quaternion.Euler(0,-90f,0));
            spawnedPedestrian.transform.SetParent(this.transform);     
            spawnedPedestrian.AddComponent<WayPointFollower>();      
            timeSinceLastSpawn = 0.0f; // Reset the timer
        }
    }
}
