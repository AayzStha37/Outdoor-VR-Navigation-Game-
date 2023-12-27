using UnityEngine;
using System.IO;
using System;
public class GameLogsStorageHandler : MonoBehaviour
{
    private string gameLogFilePath; // Path to the log file
    private string positionDataFilePath; // Path to position data file
    private string textureLogFilePath; // Path to the log file
    private bool isLogging = false;

    public void setupGameLogFile()
    {
        string gameStatsFileName = "GameLog_";
        string positionDataFileName = "PositionData_";
        string textureRecordingFileName = "TextureData_";

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
        else if (FindObjectOfType<ControllerVelocityHandler>() != null)
        {
            textureRecordingFileName = textureRecordingFileName + ".txt";
            textureLogFilePath = Path.Combine(Constants.LOG_PATH, textureRecordingFileName);
        }

        gameLogFilePath = Path.Combine(Constants.LOG_PATH, gameStatsFileName);
        positionDataFilePath = Path.Combine(Constants.LOG_PATH, positionDataFileName);

        isLogging = true;
        Application.logMessageReceived += LogToFileHandler;
    }

    void LogToFileHandler(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Log && logString.StartsWith("GAMELOG"))
        {
            // Write logs starting with "GAMELOG" to the log file
            using (StreamWriter writer = new StreamWriter(gameLogFilePath, true))
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

        if (type == LogType.Log && logString.StartsWith("TEXLOG"))
        {
            // Write logs starting with "TEXLOG" to the log file
            using (StreamWriter writer = new StreamWriter(textureLogFilePath, true))
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
