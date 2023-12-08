using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Task2EventSystem : MonoBehaviour
{
    public GameObject prefabToInstantiate; // List of prefabs to cycle through
    private GameObject instantiatedObject; // Reference to the instantiated object
    Vector3 firstTaskCustomPosition = new Vector3(-200.5f,-5.89997101f,52.1100006f);
    Vector3 firstTaskCustomRotation = new Vector3(0f,197.875702f,0f);

    Vector3 secondTaskCustomPosition = new Vector3(-389.399994f,-5.89997101f,62.3899994f);
    Vector3 secondTaskCustomRotation = new Vector3(0f,175.300003f,0f);
    public TMP_Text textfield;
    private SceneDataTransfer dataTransfer;
    
    private void Start()
    {
        dataTransfer = FindObjectOfType<SceneDataTransfer>();
        if (dataTransfer != null)
        {
            textfield.text = "Participant ID: "+dataTransfer.GetTransferredText(); // Display the transferred text
        }else
            textfield.text = "Participant ID: null";
    }

    public string getParticipantId()
    {   
        return dataTransfer.GetTransferredText();
    }

    public void taskButtonClickAction()
    {
        if (instantiatedObject != null)
        {
            Destroy(instantiatedObject); // Destroy the previously instantiated object
            InstantiateObject(secondTaskCustomPosition,secondTaskCustomRotation);
            Debug.Log("GAMELOG: Task 2.2 started"); 
        }
        else{
            InstantiateObject(firstTaskCustomPosition, firstTaskCustomRotation); 
            Debug.Log("GAMELOG: Task 2.1 started");
        }
    }

    private void InstantiateObject(Vector3 position, Vector3 rotation)
    {
        instantiatedObject = Instantiate(prefabToInstantiate, position, Quaternion.Euler(rotation));
        instantiatedObject.AddComponent<Task2CompletionNotfierSystem>();
    }
}
