using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Burst.CompilerServices;

public class Task3EventSystem : MonoBehaviour
{
    //TASK 3: Posiiton and Rotation Values of the character 
    private Vector3 firstTask3CustomPosition = new Vector3(-215.770004f,-4.68397617f,38.8800011f);
    private Vector3 firstTask3CustomRotation = new Vector3(0f,177.499084f,0f);
    private Vector3 secondTask3CustomPosition = new Vector3(-229.300003f,-5.89997101f,67.0999985f);
    private Vector3 secondTask3CustomRotation = new Vector3(0,197.875702f,0);

    public List<GameObject> prefabsToInstantiate; // List of prefabs to cycle through
    private List<GameObject> instantiatedObjectList; // Reference to the instantiated object
    private List<Tuple<Vector3, Vector3>> characterSpawnPositionAndRotationList = new List<Tuple<Vector3, Vector3>>();
    public TMP_Text textfield;
    public Toggle visualsToggleElement;
    private SceneDataTransfer dataTransfer;
    private bool isVisualsOn = true;
    private const string TASK_1 = "task1";
    private const string TASK_2 = "task2";
    
    private void Start()
    {
        instantiatedObjectList = new List<GameObject>();
        dataTransfer = FindObjectOfType<SceneDataTransfer>();
        if (dataTransfer != null)
        {
            textfield.text = "Participant ID: "+dataTransfer.GetTransferredText(); // Display the transferred text
        }else
            textfield.text = "Participant ID: null";
        
        characterSpawnPositionAndRotationList.Add(new Tuple<Vector3, Vector3>(firstTask3CustomPosition,firstTask3CustomRotation));
        characterSpawnPositionAndRotationList.Add(new Tuple<Vector3, Vector3>(secondTask3CustomPosition,secondTask3CustomRotation));
    }

    public string getParticipantId()
    {   
        return dataTransfer.GetTransferredText();
    }

    public void task1ButtonClickAction()
    {
        isVisualsOn = visualsToggleElement.isOn ? true : false;
        if (instantiatedObjectList.Any())
            DestroyInstantiatedObjects(); // Destroy the previously instantiated object
        performTask(TASK_1);
    }

    public void task2ButtonClickAction()
    {
        isVisualsOn = visualsToggleElement.isOn ? true : false;
        if (instantiatedObjectList.Count > 0)
            DestroyInstantiatedObjects(); // Destroy the previously instantiated object
        performTask(TASK_2);
    }

    private void DestroyInstantiatedObjects()
    {
        foreach(GameObject gameObject in instantiatedObjectList){
            Destroy(gameObject);
        }
        instantiatedObjectList.Clear();
    }

    private void InstantiateObjects(Tuple<Vector3, Vector3> tuple, string taskName)
    {
        GameObject instantiatedObject;
        foreach(GameObject gameObject in prefabsToInstantiate){
            if(Constants.XR_ORIGIN_MAIN_CHARACTER.Equals(gameObject.name)){
                instantiatedObject = Instantiate(gameObject, tuple.Item1, Quaternion.Euler(tuple.Item2));
                instantiatedObject.AddComponent<Task3CompletionNotfierSystem>();
            }
            else if(Constants.TASK3_PATHWAY_1.Equals(gameObject.name) && TASK_2.Equals(taskName))    
                continue;
            else if(Constants.TASK3_PATHWAY_2.Equals(gameObject.name) && TASK_1.Equals(taskName))    
                continue;
            else
                instantiatedObject = Instantiate(gameObject);
                
            instantiatedObjectList.Add(instantiatedObject);
        }
    }

    private void performTask(string taskName)
    {
        if(TASK_1.Equals(taskName)){   
            InstantiateObjects(characterSpawnPositionAndRotationList[0], taskName); 
            Debug.Log($"GAMELOG: Task 3.1 started with visuals: {isVisualsOn}");
            Debug.Log($"POSLOG: Task 3.1 started with visuals: {isVisualsOn}");
        }
        else if(TASK_2.Equals(taskName)){
            InstantiateObjects(characterSpawnPositionAndRotationList[1], taskName); 
            Debug.Log($"GAMELOG: Task 3.2 started with visuals: {isVisualsOn}");
            Debug.Log($"POSLOG: Task 3.2 started with visuals: {isVisualsOn}");
        }
    }
}
