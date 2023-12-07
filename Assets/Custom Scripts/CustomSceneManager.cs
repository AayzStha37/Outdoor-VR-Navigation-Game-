using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public void LoadTrainingPhaseScene()
    {
        SceneManager.LoadScene("Main navigation start scene");
    }
     public void LoadTaskListUIScene()
    {
        // SceneDataTransfer dataTransfer = FindObjectOfType<SceneDataTransfer>();
        // if (dataTransfer != null)
        // {
        //     dataTransfer.StoreInputText(); // Store text before switching scenes
        // }
        SceneManager.LoadScene("Task list UI");
    }
    public void LoadTask1Scene()
    {
        SceneManager.LoadScene("Task 1 scene");
    }
    public void LoadTask2Scene()
    {
        SceneManager.LoadScene("Main navigation start scene");
    }
    public void LoadTask3Scene()
    {
        SceneManager.LoadScene("Main navigation start scene");
    }
    
}
