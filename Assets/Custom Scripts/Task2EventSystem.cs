using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Task2EventSystem : MonoBehaviour
{
    //TASK 2: Posiiton and Rotation Values of the character 
    private Vector3 firstTask2CustomPosition = new Vector3(-200.5f,-5.89997101f,52.1100006f);
    private Vector3 firstTask2CustomRotation = new Vector3(0f,197.875702f,0f);
    private Vector3 secondTask2CustomPosition = new Vector3(-358.519958f,-4.86755228f,56.8200493f);
    private Vector3 secondTask2CustomRotation = new Vector3(3.71701208e-05f,175.300308f,9.36971774e-05f);
    

    public GameObject mainCharacterGameobjectToMove; 
    private List<Tuple<Vector3, Vector3>> characterSpawnPositionAndRotationList = new List<Tuple<Vector3, Vector3>>();
    public TMP_Text textfield;
    public Toggle visualsToggleElement;
    private SceneDataTransfer dataTransfer;
    private bool isVisualsOn = true;
    private const string TASK_1 = "task1";
    private const string TASK_2 = "task2";
    
    private void Start()
    {
        if(mainCharacterGameobjectToMove==null)
            Debug.LogError("The main character gameobject reference is null!");

        dataTransfer = FindObjectOfType<SceneDataTransfer>();
        if (dataTransfer != null)
        {
            textfield.text = "Participant ID: "+dataTransfer.GetTransferredText(); // Display the transferred text
        }else
            textfield.text = "Participant ID: null";
        
        characterSpawnPositionAndRotationList.Add(new Tuple<Vector3, Vector3>(firstTask2CustomPosition,firstTask2CustomRotation));
        characterSpawnPositionAndRotationList.Add(new Tuple<Vector3, Vector3>(secondTask2CustomPosition,secondTask2CustomRotation));
    }

    public string getParticipantId()
    {   
        return dataTransfer.GetTransferredText();
    }

    public void task1ButtonClickAction()
    {
        isVisualsOn = visualsToggleElement.isOn ? true : false;
        // if (instantiatedObjectList.Any())
        //     DestroyInstantiatedObjects(); // Destroy the previously instantiated object
        performTask(TASK_1);
    }

    public void task2ButtonClickAction()
    {
        isVisualsOn = visualsToggleElement.isOn ? true : false;
        // if (instantiatedObjectList.Any())
        //     DestroyInstantiatedObjects(); // Destroy the previously instantiated object
        performTask(TASK_2);
    }

    private void DestroyInstantiatedObjects()
    {
        // foreach(GameObject gameObject in instantiatedObjectList){
        //     Destroy(gameObject);
        // }
        // instantiatedObjectList.Clear();
    }

    private void TransformPositionAndRotation(Tuple<Vector3, Vector3> tuple)
    {
        mainCharacterGameobjectToMove.transform.position = tuple.Item1;
        mainCharacterGameobjectToMove.transform.rotation = Quaternion.Euler(tuple.Item2);
        // foreach(GameObject gameObject in gameObjectsToInstantiate){
        //     instantiatedObject = Instantiate(gameObject, tuple.Item1, Quaternion.Euler(tuple.Item2));
        //     if(Constants.XR_ORIGIN_MAIN_CHARACTER.Equals(gameObject.name))
        //             instantiatedObject.AddComponent<Task2CompletionNotfierSystem>();
        //     instantiatedObjectList.Add(instantiatedObject);
        // }
    }

    private void performTask(string taskName)
    {
        if(TASK_1.Equals(taskName)){   
            TransformPositionAndRotation(characterSpawnPositionAndRotationList[0]); 
            Debug.Log($"GAMELOG: Task 2.1 started with visuals: {isVisualsOn}");
            Debug.Log($"POSLOG: Task 2.1 started with visuals: {isVisualsOn}");
            mainCharacterGameobjectToMove.GetComponent<Task2CompletionNotfierSystem>().StartTimer();
            mainCharacterGameobjectToMove.GetComponent<Character2DPositionDataLogger>().SetTrackablePositionLog(true);
        }            
        else if(TASK_2.Equals(taskName)){
            TransformPositionAndRotation(characterSpawnPositionAndRotationList[1]); 
            Debug.Log($"GAMELOG: Task 2.2 started with visuals: {isVisualsOn}");
            Debug.Log($"POSLOG: Task 2.2 started with visuals: {isVisualsOn}");
            mainCharacterGameobjectToMove.GetComponent<Task2CompletionNotfierSystem>().StartTimer();
            mainCharacterGameobjectToMove.GetComponent<Character2DPositionDataLogger>().SetTrackablePositionLog(true);
        }                
    }
}
