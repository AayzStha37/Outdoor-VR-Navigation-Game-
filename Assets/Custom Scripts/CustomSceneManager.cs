using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class CustomSceneManager : MonoBehaviour
{
    private GameLogsStorageHandler logToFile = new GameLogsStorageHandler();
    private string textfieldvalue = null;

    public void LoadTrainingPhaseScene()
    {
        SceneManager.LoadScene("Trainng Scene");
    }
    public void LoadTaskListUIScene()
    {
        SceneManager.LoadScene("Task list UI");
    }
    public void LoadTStudyStartScene()
    {
        SceneManager.LoadScene("Study start scene");
    }
    public void LoadTask1Scene()
    {        
        logToFile.setupGameLogFile();
        storePartcipantDetail();
        Debug.Log("GAMELOG: Task 1 started for participant");
        SceneManager.LoadScene("Task 1 scene");
    }

    public void LoadTask2Scene()
    {
        storePartcipantDetail();
        Debug.Log("GAMELOG: Task 2 started for participant");        
        Debug.Log("POSLOG: Task 2 started for participant");
        SceneManager.LoadScene("Task 2 scene");
    }
    public void LoadTask3Scene()
    {
        storePartcipantDetail();
        Debug.Log("GAMELOG: Task 3 started for participant");
        Debug.Log("POSLOG: Task 3 started for participant");
        SceneManager.LoadScene("Task 3 scene");
    }

    private void storePartcipantDetail()
    {
        SceneDataTransfer dataTransfer = FindObjectOfType<SceneDataTransfer>();
        if (dataTransfer != null)
        {
            textfieldvalue = dataTransfer.StoreInputText();
            Debug.Log("GAMELOG: Participant number : "+ textfieldvalue + " logging started"); 
        }else 
             Debug.Log("GAMELOG: ERROR - Failed to store participant info");
    }
    
}
