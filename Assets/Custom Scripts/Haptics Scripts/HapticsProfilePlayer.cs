using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Text;
using System;

public class HapticsProfilePlayer : MonoBehaviour
{
  
    private bool _isPlaying = false;
    private Vector3 _lastPosition;
    private bool startsInteraction = false;
    private uint playingId;
    private GameObject secondaryCollisionGameObj;
    public static SerialPort arduinoPort = new SerialPort("COM9");
    private bool hapticsStarted = false;    
    private Queue<string> sendQueue = new Queue<string>();

    int count = 0;
    private void Awake()
    {
        _lastPosition = this.transform.position;
    }

    private void Start()
    {
        AkSoundEngine.RegisterGameObj(gameObject);
        _isPlaying = false;
        initArduinoSerialPort();
        openArduinoPortConnection();
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

    private void sendDataToArduino()
    {
        String dataToSend = sendQueue.Dequeue();
        arduinoPort.WriteLine(dataToSend+'\n');
        arduinoPort.BaseStream.Flush();
        Debug.Log("Data sent to Arduino - "+dataToSend);
    }

    private void OnDestroy() {
        if (arduinoPort != null && arduinoPort.IsOpen)
        {
            CloseArduinoPort();
        }
    }

    private void Update()
    {
        secondaryCollisionGameObj = this.transform.gameObject.GetComponent<CollisionDetectionCustomScript>()._secondaryCollsionObject();
        bool startsColliding = CollisionDetectionCustomScript.IsTouching(this.transform.gameObject,secondaryCollisionGameObj);
        
        if(count>5){
            CloseArduinoPort();
            openArduinoPortConnection();
            count = 0;
        }
        try{
        //Starting the movement
        if(!startsInteraction && startsColliding && Constants.WhiteCaneTipTag.Equals(secondaryCollisionGameObj.tag)){
            count++;
            startMovement();
        }
        //Ending the movement
        else if(startsInteraction && !startsColliding){
            endMovement();
        }
        //Updating the movement
        // else if(startsInteraction && registeredCollidingGameObject.Equals(secondaryCollisionGameObj)){
        //     updateMovement();
        // }
        }catch(System.Exception e){
            Debug.LogError("Exception while sending data to Arduino - "+e.GetBaseException());
        }
    }

    void startMovement(){
        startsInteraction=true;
        // if(!hapticsStarted){
        //     //double[] dft321AccelerationMagnitudeArray = secondaryCollisionGameObj.GetComponent<HapticsProfileSelector>().RetunHapticsProfile();
        //     sendDataToArduino(StartHapticsFlag);        
        //     Debug.Log("Haptics initiation sent to Arduino")
        //     //arduinoPort.Write(dft321AccelerationMagnitudeArray.ToString());
        //     hapticsStarted = true;
        // }
        
        sendQueue.Enqueue(Constants.StartHapticsFlag+"_"+gameObject.tag);
        sendDataToArduino();        
        Debug.Log("Haptics initiation sent to Arduino");
    }
    void updateMovement()
    {
        //TODO future usage
    }

    void endMovement(){
        startsInteraction=false;
        // if(hapticsStarted){
        //     sendDataToArduino(StopHapticsFlag);
        //     Debug.Log("Haptics termination sent to Arduino");
        // }
        sendQueue.Enqueue(Constants.StopHapticsFlag);
        sendDataToArduino();
        Debug.Log("Haptics termination sent to Arduino");
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
}
