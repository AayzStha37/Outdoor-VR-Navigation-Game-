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
    private bool isMoving = false;
    private uint playingId;
    private GameObject secondaryCollisionGameObj;
    public static SerialPort arduinoPort = new SerialPort("COM5",9600);
    private bool hapticsStarted = false;    

    private void Awake()
    {
        _lastPosition = this.transform.position;
    }

    private void Start()
    {
        AkSoundEngine.RegisterGameObj(gameObject);
        _isPlaying = false;
        openArduinoPortConnection();
    }
    
    private void sendDataToArduino(String flag)
    {
        byte[] dataBytes = Encoding.ASCII.GetBytes(flag); // Convert string to bytes
        arduinoPort.Write(dataBytes, 0, dataBytes.Length); // Send the bytes over the serial port
        Debug.Log("Data sent to Arduino - "+flag);
    }

    private void OnDestroy() {
        if (arduinoPort != null && arduinoPort.IsOpen)
        {
            arduinoPort.Close();
            Debug.Log("Arduino Port closed");
        }
    }

    private void Update()
    {
        secondaryCollisionGameObj = this.transform.gameObject.GetComponent<CollisionDetectionCustomScript>()._secondaryCollsionObject();
        bool startsColliding = CollisionDetectionCustomScript.IsTouching(this.transform.gameObject,secondaryCollisionGameObj);

        //Starting the movement
        if(!isMoving && startsColliding && Constants.WhiteCaneTipTag.Equals(secondaryCollisionGameObj.tag)){
            startMovement();
        }
        //Ending the movement
        else if(isMoving && !startsColliding){
            endMovement();
        }
        //Updating the movement
        // else if(isMoving && registeredCollidingGameObject.Equals(secondaryCollisionGameObj)){
        //     updateMovement();
        // }
    }

    void startMovement(){
        isMoving=true;
        // if(!hapticsStarted){
        //     //double[] dft321AccelerationMagnitudeArray = secondaryCollisionGameObj.GetComponent<HapticsProfileSelector>().RetunHapticsProfile();
        //     sendDataToArduino(StartHapticsFlag);        
        //     Debug.Log("Haptics initiation sent to Arduino")
        //     //arduinoPort.Write(dft321AccelerationMagnitudeArray.ToString());
        //     hapticsStarted = true;
        // }

        sendDataToArduino(Constants.StartHapticsFlag);        
        Debug.Log("Haptics initiation sent to Arduino");
    }
    void updateMovement()
    {
        //TODO future usage
    }

    void endMovement(){
        isMoving=false;
        // if(hapticsStarted){
        //     sendDataToArduino(StopHapticsFlag);
        //     Debug.Log("Haptics termination sent to Arduino");
        // }
        sendDataToArduino(Constants.StopHapticsFlag);
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
}
