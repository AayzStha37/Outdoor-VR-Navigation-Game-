using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLogFiles : MonoBehaviour
{
    private static bool isFirstLoad = true;
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
