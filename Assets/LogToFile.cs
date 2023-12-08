using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class LogToFile : MonoBehaviour
{
    private string logFilePath; // Path to the log file
    private bool isLogging = false;

    public void setupLogFile()
    {
        string logFileName = "GameLog_";
        
        if (FindObjectOfType<SceneDataTransfer>() != null)
        {
            SceneDataTransfer dataTransfer = FindObjectOfType<SceneDataTransfer>();
            logFileName = logFileName+dataTransfer.StoreInputText()+".txt";
        }else if(FindObjectOfType<Task1Eventsystem>() != null){
            Task1Eventsystem task1EventSystem = FindObjectOfType<Task1Eventsystem>(); 
            logFileName = logFileName+task1EventSystem.getParticipantId()+".txt";
        }else if(FindObjectOfType<Task2EventSystem>() != null){
            Task2EventSystem task2EventSystem = FindObjectOfType<Task2EventSystem>(); 
            logFileName = logFileName+task2EventSystem.getParticipantId()+".txt";
        }

        logFilePath = Path.Combine("C:/Users/Admin/Desktop/Unity logs", logFileName);
        
        isLogging = true;
        Application.logMessageReceived += LogToFileHandler;
    }

    void LogToFileHandler(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Log && logString.StartsWith("GAMELOG"))
        {
            // Write logs starting with "GAMELOG" to the file
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(logString);
            }
        }
    }

    void OnDestroy()
    {
        // Stop capturing logs
        isLogging = false;
        Application.logMessageReceived -= LogToFileHandler;
    }
}
