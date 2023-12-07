using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CustomSceneManager : MonoBehaviour
{
    private LogToFile logToFile = new LogToFile();

    public void LoadTrainingPhaseScene()
    {
        SceneManager.LoadScene("Main navigation start scene");
    }
     public void LoadTaskListUIScene()
    {
        
        SceneManager.LoadScene("Task list UI");
    }
    public void LoadTask1Scene()
    {        
        logToFile.setupLogFile();
        storePartcipantDetail();
        Debug.Log("GAMELOG: Task 1 started for participant");
        SceneManager.LoadScene("Task 1 scene");
    }

    public void LoadTask2Scene()
    {
        logToFile.setupLogFile();
        storePartcipantDetail();
        Debug.Log("GAMELOG: Task 2 started for participant");
        SceneManager.LoadScene("Main navigation start scene");
    }
    public void LoadTask3Scene()
    {
        logToFile.setupLogFile();
        storePartcipantDetail();
        Debug.Log("GAMELOG: Task 3 started for participant");
        SceneManager.LoadScene("Main navigation start scene");
    }

    private void storePartcipantDetail()
    {
        SceneDataTransfer dataTransfer = FindObjectOfType<SceneDataTransfer>();
        if (dataTransfer != null)
        {
            string textfieldvalue = dataTransfer.StoreInputText();
            Debug.Log("GAMELOG: Participant number : "+ textfieldvalue + " logging started"); 
        }else 
             Debug.Log("GAMELOG: ERROR - Failed to store participant info");
        

    }
    
}
