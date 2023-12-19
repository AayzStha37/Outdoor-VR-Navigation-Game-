using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Task2And3EventSystem : MonoBehaviour
{
    //TASK 2: Posiiton and Rotation Values of the character 
    private Vector3 firstTask2CustomPosition = new Vector3(-200.5f,-5.89997101f,52.1100006f);
    private Vector3 firstTask2CustomRotation = new Vector3(0f,197.875702f,0f);
    private Vector3 secondTask2CustomPosition = new Vector3(-389.399994f,-5.89997101f,62.3899994f);
    private Vector3 secondTask2CustomRotation = new Vector3(0f,175.300003f,0f);

    //TASK 3: Posiiton and Rotation Values of the character 
    private Vector3 firstTask3CustomPosition = new Vector3(-200.5f,-5.89997101f,52.1100006f);
    private Vector3 firstTask3CustomRotation = new Vector3(0f,197.875702f,0f);
    private Vector3 secondTask3CustomPosition = new Vector3(-389.399994f,-5.89997101f,62.3899994f);
    private Vector3 secondTask3CustomRotation = new Vector3(0f,175.300003f,0f);


    public List<GameObject> prefabsToInstantiate; // List of prefabs to cycle through
    private List<GameObject> instantiatedObjectList; // Reference to the instantiated object
    private List<Tuple<Vector3, Vector3>> characterSpawnPositionAndRotationList = new List<Tuple<Vector3, Vector3>>();
    public TMP_Text textfield;
    public Toggle visualsToggleElement;
    private SceneDataTransfer dataTransfer;
    private bool isVisualsOn = true;
    private string sceneName;
    private const string TASK_1 = "task1";
    private const string TASK_2 = "task2";
    
    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        instantiatedObjectList = new List<GameObject>();
        dataTransfer = FindObjectOfType<SceneDataTransfer>();
        if (dataTransfer != null)
        {
            textfield.text = "Participant ID: "+dataTransfer.GetTransferredText(); // Display the transferred text
        }else
            textfield.text = "Participant ID: null";
        
        characterSpawnPositionAndRotationList.Add(new Tuple<Vector3, Vector3>(firstTask2CustomPosition,firstTask2CustomRotation));
        characterSpawnPositionAndRotationList.Add(new Tuple<Vector3, Vector3>(secondTask2CustomPosition,secondTask2CustomRotation));
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

    private void InstantiateObjects(Tuple<Vector3, Vector3> tuple)
    {
        GameObject instantiatedObject;
        foreach(GameObject gameObject in prefabsToInstantiate){
            instantiatedObject = Instantiate(gameObject, tuple.Item1, Quaternion.Euler(tuple.Item2));
            if(Constants.XR_ORIGIN_MAIN_CHARACTER.Equals(gameObject.name))
                instantiatedObject.AddComponent<Task2CompletionNotfierSystem>();
            instantiatedObjectList.Add(instantiatedObject);
        }
    }

    private void performTask(string taskName)
    {
        switch (sceneName){
            case Constants.TASK2_SCENE_NAME:
                if(TASK_1.Equals(taskName)){   
                    InstantiateObjects(characterSpawnPositionAndRotationList[0]); 
                    Debug.Log($"GAMELOG: Task 2.1 started with visuals: {isVisualsOn}");
                    Debug.Log($"POSLOG: Task 2.1 started with visuals: {isVisualsOn}");
                }
                else if(TASK_2.Equals(taskName)){
                    InstantiateObjects(characterSpawnPositionAndRotationList[1]); 
                    Debug.Log($"GAMELOG: Task 2.2 started with visuals: {isVisualsOn}");
                    Debug.Log($"POSLOG: Task 2.2 started with visuals: {isVisualsOn}");
                }
                break;
            case Constants.TASK3_SCENE_NAME:
                if(TASK_1.Equals(taskName)){   
                    InstantiateObjects(characterSpawnPositionAndRotationList[2]); 
                    Debug.Log($"GAMELOG: Task 3.1 started with visuals: {isVisualsOn}");
                    Debug.Log($"POSLOG: Task 3.1 started with visuals: {isVisualsOn}");

                }
                else if(TASK_2.Equals(taskName)){
                    InstantiateObjects(characterSpawnPositionAndRotationList[3]); 
                    Debug.Log($"GAMELOG: Task 3.2 started with visuals: {isVisualsOn}");
                    Debug.Log($"POSLOG: Task 3.2 started with visuals: {isVisualsOn}");
                }
                break;
        }
    }
}
