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
    public List<GameObject> Waypoints
    {
        get { return waypoints; }
    }
}

public class PedestrainSpawner : MonoBehaviour
{
    public List<GameObject> pedestrianModels;
    public AnimatorController pedestrianAnimation;
    public  float spawnInterval;
     
    public float initalSpeed;
    float timeSinceLastSpawn = 0.0f;
    Random random = new Random();
    
    [SerializeField]
    List<WPArray> waypointsList = new List<WPArray>();
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
         timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            spawnPedestrian(pedestrianModels[random.Next(0,pedestrianModels.Count)]);
            timeSinceLastSpawn = 0.0f; // Reset the timer
        }
    }

    private void spawnPedestrian(GameObject pedestrianModel)
    {
        GameObject spawnedPedestrian = Instantiate(pedestrianModel,transform.position,Quaternion.Euler(0,-90f,0));
        spawnedPedestrian.transform.GetComponent<Animator>().runtimeAnimatorController = pedestrianAnimation;
        spawnedPedestrian.transform.SetParent(this.transform);     
        PedestrianWayPointFollower scriptComponent = spawnedPedestrian.AddComponent<PedestrianWayPointFollower>();
        WPArray wayPointArray  =  waypointsList[random.Next(0,waypointsList.Count)];
        scriptComponent.InitializeWayPointsVal(wayPointArray.Waypoints,initalSpeed,spawnedPedestrian.GetComponent<Rigidbody>());
        spawnedPedestrian.AddComponent<PedestrianWayPointFollower>();   
    }
}
