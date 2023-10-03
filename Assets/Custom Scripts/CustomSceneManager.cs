using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public void LoadNavigationScene()
    {
        SceneManager.LoadScene("Main navigation start scene");
    }
     public void LoadInteractionScene()
    {
        SceneManager.LoadScene("Main Interaction Scene Demo");
    }
    
}
