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
    private GameObject registeredCollidingGameObject;
    public static SerialPort arduinoPort = new SerialPort("COM5",9600);
    private string StartHapticsFlag = "startHaptics";
    private string StopHapticsFlag = "stopHaptics";
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
        if(!isMoving && startsColliding){
            startMovement(secondaryCollisionGameObj);
        }
        //Updating the movement
        else if(isMoving && registeredCollidingGameObject.Equals(secondaryCollisionGameObj)){
            updateMovement();
        }
        //Ending the movement
        else if(isMoving && !registeredCollidingGameObject.Equals(secondaryCollisionGameObj)){
            endMovement();
        }
    }

    void startMovement(GameObject secondaryCollisionGameObj){
        isMoving=true;
        registeredCollidingGameObject = secondaryCollisionGameObj;
        if(!hapticsStarted){
            //double[] dft321AccelerationMagnitudeArray = secondaryCollisionGameObj.GetComponent<HapticsProfileSelector>().RetunHapticsProfile();
            sendDataToArduino(StartHapticsFlag);
            //arduinoPort.Write(dft321AccelerationMagnitudeArray.ToString());
            Debug.Log("Haptics initiation sent to Arduino");
            hapticsStarted = true;
        }
    }
    void updateMovement()
    {
        //TODO future usage
    }

    void endMovement(){
        isMoving=false;
        if(hapticsStarted){
            sendDataToArduino(StopHapticsFlag);
            Debug.Log("Haptics termination sent to Arduino");
        }
    }

    public void openArduinoPortConnection(){
        if(arduinoPort != null){
            if(arduinoPort.IsOpen){
                arduinoPort.Close();
                Debug.Log("Closing arduino port, because it was already open!");
            }
            else{
                arduinoPort.Open();
                arduinoPort.ReadTimeout = 16;
                Debug.Log("Arduino Port Opened!");
            }
        }
        else{
            if(arduinoPort.IsOpen){
                Debug.Log("Arduino Port is arleady opened");
            }
            else{
                Debug.Log("Arduino Port == null");
            }
        }
    }
}
