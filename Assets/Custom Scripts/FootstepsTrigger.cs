using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class FootstepsTrigger : MonoBehaviour
{
    private Rigidbody character;   
    private bool _isPlaying = false;
    private bool startsInteraction = false;
    private uint playingId;
    private GameObject secondaryCollisionGameObj;
    private GameObject registeredCollidingGameObject;
    //public SerialPort arduinoPort;
    private bool firstFrame = true;
    private Vector3 lastPosition;
    bool isStationary;

    // Start is called before the first frame update
    void Start()
    {
        character = gameObject.GetComponent<Rigidbody>();
        AkSoundEngine.RegisterGameObj(gameObject);
        _isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        secondaryCollisionGameObj = this.transform.gameObject.GetComponent<CollisionDetectionCustomScript>()._secondaryCollsionObject();
        Debug.Log("Secndary Collision object: "+ secondaryCollisionGameObj);

        bool startsColliding = CollisionDetectionCustomScript.IsTouching(this.transform.gameObject,secondaryCollisionGameObj);
        
        Debug.Log("charatcer moving - "+character.velocity.magnitude);
        //Starting the movement
        if(!startsInteraction && startsColliding){
            startMovement();
        }
        //ending the movement
        else if(startsInteraction && (!startsColliding || CheckIfStationary() || !registeredCollidingGameObject.Equals(secondaryCollisionGameObj) )){
            endMovement();
        }
        //Updating the movement
        else if(startsInteraction 
                && registeredCollidingGameObject.Equals(secondaryCollisionGameObj)
                && !CheckIfStationary()){
            updateMovement();
        }   
        lastPosition = this.transform.position;
    }

    bool CheckIfStationary() {
        float travelSquared = (this.transform.position - lastPosition).sqrMagnitude;
        return travelSquared < Math.Pow(Constants.stationaryTolerance,2);
    }

    void startMovement(){
        startsInteraction=true;
        registeredCollidingGameObject = secondaryCollisionGameObj;
        Debug.Log("Character movement has been initiated");
    }
    void updateMovement()
    {
        Debug.Log("Character movement being updated");
        try{
            if(!_isPlaying){
                playingId = AkSoundEngine.PostEvent(Constants.FOOTSTEPS_PLAY_EVENT,gameObject);
                Debug.Log("Footsteps audio play initiated");          
                _isPlaying = true;
            }
            //TODO fix this block of code
            if(_isPlaying){     
                float characterNavSpeed = character.velocity.magnitude;
                AkSoundEngine.SetRTPCValue(Constants.RTPC_CharacterNavSpeed,characterNavSpeed);         
                Debug.Log("RTPC footsteps Volume : " + characterNavSpeed);
            }
        }catch(System.Exception e){
            Debug.Log("Exception while playing footsteps audio :" + e);
        }

    }

    void endMovement(){
        startsInteraction=false;
        _isPlaying = false;
        AkSoundEngine.StopPlayingID(playingId);
        Debug.Log("Character movement has been terminated");
    }
}