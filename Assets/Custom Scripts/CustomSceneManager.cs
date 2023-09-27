using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Main navigation start scene");
    }
    
}
