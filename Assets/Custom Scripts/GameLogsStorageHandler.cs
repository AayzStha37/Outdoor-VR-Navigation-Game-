using UnityEngine;
using System.IO;
using System;
public class GameLogsStorageHandler : MonoBehaviour
{
    private string logFilePath; // Path to the log file
    private string positionDataFilePath; // Path to position data file
    private bool isLogging = false;

    public void setupGameLogFile()
    {
        string gameStatsFileName = "GameLog_";
        string positionDataFileName = "PositionData_";

        if (FindObjectOfType<SceneDataTransfer>() != null)
        {
            SceneDataTransfer dataTransfer = FindObjectOfType<SceneDataTransfer>();
            gameStatsFileName = gameStatsFileName + dataTransfer.StoreInputText() + ".txt";
            positionDataFileName = positionDataFileName + dataTransfer.StoreInputText() + ".txt";
            
        }
        else if (FindObjectOfType<Task1Eventsystem>() != null)
        {
            Task1Eventsystem task1EventSystem = FindObjectOfType<Task1Eventsystem>();
            gameStatsFileName = gameStatsFileName + task1EventSystem.getParticipantId() + ".txt";
        }
        else if (FindObjectOfType<Task2EventSystem>() != null)
        {
            Task2EventSystem task2EventSystem = FindObjectOfType<Task2EventSystem>();
            gameStatsFileName = gameStatsFileName + task2EventSystem.getParticipantId() + ".txt";
            positionDataFileName = positionDataFileName + task2EventSystem.getParticipantId() + ".txt";
        }
        else if (FindObjectOfType<Task3EventSystem>() != null)
        {
            Task3EventSystem task3EventSystem = FindObjectOfType<Task3EventSystem>();
            gameStatsFileName = gameStatsFileName + task3EventSystem.getParticipantId() + ".txt";
            positionDataFileName = positionDataFileName + task3EventSystem.getParticipantId() + ".txt";
        }

        logFilePath = Path.Combine("C:/Users/Admin/Desktop/Unity logs", gameStatsFileName);
        positionDataFilePath = Path.Combine("C:/Users/Admin/Desktop/Unity logs", positionDataFileName);

        isLogging = true;
        Application.logMessageReceived += LogToFileHandler;
    }

    void LogToFileHandler(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Log && logString.StartsWith("GAMELOG"))
        {
            // Write logs starting with "GAMELOG" to the log file
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(logString);
            }
        }

        if (type == LogType.Log && logString.StartsWith("POSLOG"))
        {
            // Write logs starting with "POSLOG" to the log file
            using (StreamWriter writer = new StreamWriter(positionDataFilePath, true))
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
