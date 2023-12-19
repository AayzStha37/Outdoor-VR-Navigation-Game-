using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task1Eventsystem : MonoBehaviour
{
    public List<GameObject> prefabsToInstantiate; // List of prefabs to cycle through
    private int currentIndex = 0; // Index to track the current prefab
    private GameObject instantiatedObject; // Reference to the instantiated object
    Vector3 customPosition = new Vector3(-190.070007f,-6.01000023f,105.050003f);
    public TMP_Text textfield;
    private SceneDataTransfer dataTransfer;
    private void Start()
    {
        Debug.Log("GAMELOG: "+getTaskname(currentIndex)+" started");
        dataTransfer = FindObjectOfType<SceneDataTransfer>();
        if (dataTransfer != null)
        {
            textfield.text = "Participant ID: "+dataTransfer.GetTransferredText(); // Display the transferred text
        }else
            textfield.text = "Participant ID: null";
        InstantiateObject(customPosition);
    }

    public string getParticipantId()
    {   
        return dataTransfer.GetTransferredText();
    }

    public void NextTaskButtonClickAction()
    {
        
        if (instantiatedObject != null)
        {
            Destroy(instantiatedObject); // Destroy the previously instantiated object
        }

        // Increment the index to cycle through the prefab list
        currentIndex = (currentIndex + 1) % prefabsToInstantiate.Count;
        Debug.Log("GAMELOG: "+getTaskname(currentIndex)+" started");
        InstantiateObject(customPosition); 
    }

    private void InstantiateObject(Vector3 position)
    {
        instantiatedObject = Instantiate(prefabsToInstantiate[currentIndex], position, Quaternion.identity);
    }

    private string getTaskname(int currentIndex)
    {
       string taskName = prefabsToInstantiate[currentIndex].transform.name;
       return taskName.Substring(taskName.IndexOf("Textures")+"Textures".Length).Trim();
    }
}
