using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO.Ports;
using System;
using System.Threading;
public class ControllerVelocityHandler : MonoBehaviour
{
    public InputActionProperty controllerVelocityProperty;

    public string threeAxesVelocity = "";
    
    private GameLogsStorageHandler logToFile = new GameLogsStorageHandler();

    bool startRecording = false;
    
    public static SerialPort arduinoPort = new SerialPort("COM5");

    private Thread readThread;
    
    private bool isThreadReading = true;
    
    void Start() {
        logToFile.setupGameLogFile(); 
        initArduinoSerialPort();
        openArduinoPortConnection();
        initSerialInputThread();
    }

    private void initSerialInputThread()
    {
        readThread = new Thread(ReadSerialData)
        {            
            IsBackground = true
        };
        readThread.Start();
    }

    private void ReadSerialData()
    {
        while (isThreadReading)
        {
            try
            {
                if (arduinoPort.IsOpen && startRecording)
                {
                    string data = arduinoPort.ReadLine(); // Read the incoming data
                    threeAxesVelocity = ParseVectorValue(controllerVelocityProperty.action.ReadValue<Vector3>());
                    MainThreadDispatcher.ExecuteInMainThread(() =>{
                        Debug.Log($"TEXLOG: {data}, {threeAxesVelocity}");
                    });
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    private string ParseVectorValue(Vector3 vector)
    {
        string vectorString = vector.x.ToString("F2") + "," + vector.y.ToString("F2") + "," + vector.z.ToString("F2");
        return vectorString;
    }

    private void SendMessageToMainThread(object v)
    {
        throw new NotImplementedException();
    }

    private void OnDestroy() {
        isThreadReading = false; // Stop the reading thread
        if (arduinoPort != null && arduinoPort.IsOpen)
        {
            CloseArduinoPort();
        }
        if (readThread != null && readThread.IsAlive)
        {
            readThread.Join(); // Wait for the thread to terminate
        }
    }

     private void initArduinoSerialPort()
    {
        arduinoPort.BaudRate = 115200;
        arduinoPort.Parity = Parity.None;
        arduinoPort.StopBits = StopBits.One;
        arduinoPort.DataBits = 8;
        arduinoPort.Handshake = Handshake.None;
        arduinoPort.RtsEnable = true;
    }

    public void openArduinoPortConnection(){
        if(arduinoPort != null){
            if(arduinoPort.IsOpen){
                Debug.Log("Arduino Port is arleady opened");
            }
            else{
                arduinoPort.Open();
                arduinoPort.ReadTimeout = 16;
                Debug.Log("Arduino Port Opened!");
            }
        }
        else{
                Debug.Log("Arduino Port == null");
        }
    }
    private void CloseArduinoPort(){   
        arduinoPort.Close();
        Debug.Log("Arduino Port closed");
    }

    public void StartVelocityCapture(){
        if(!startRecording){
            startRecording = true;
            Debug.Log("Start Record");
        }
        else {   
            startRecording = false;            
            Debug.Log("End Record");
        }
    }
}
