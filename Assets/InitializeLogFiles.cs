using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitializeLogFiles : MonoBehaviour
{
    private static bool isFirstLoad = true;
    //public TMP_Text textfield;
    private GameLogsStorageHandler logToFile = new GameLogsStorageHandler();
    void Start()
    {
        if (isFirstLoad)
        {           
            logToFile.setupGameLogFile(); 
            isFirstLoad = false;
        } 
    }
}
