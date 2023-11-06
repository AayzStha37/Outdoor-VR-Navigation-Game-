using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Input;
using UnityEditor.Animations;
using UnityEngine;
using Random = System.Random;
[System.Serializable]
    public class WPArray
    {
        public List<GameObject> waypoints;
     
        // optionally some other fields
    }
public class PedestrainSpawner : MonoBehaviour
{
    public GameObject pedestrianModel;
    public AnimatorController pedestrianAnimation;
    public  float spawnInterval;
     
    public float initalSpeed;
    float timeSinceLastSpawn = 0.0f;
    Random random;
    
    
    
    List<WPArray> waypointsList = new List<WPArray>();
    // Start is called before the first frame update
    void Start()
    {
        
        random = new Random();
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
            PedestrianWayPointFollower scriptComponent = spawnedPedestrian.AddComponent<PedestrianWayPointFollower>();
            // GameObject[] waypoints  =  waypointsList[random.Next(0,waypointsList.Count)];
            // scriptComponent.InitializeWayPointsVal(waypoints,initalSpeed,spawnedPedestrian.GetComponent<Rigidbody>());
            // spawnedPedestrian.AddComponent<PedestrianWayPointFollower>();      
            timeSinceLastSpawn = 0.0f; // Reset the timer
        }
    }
}
