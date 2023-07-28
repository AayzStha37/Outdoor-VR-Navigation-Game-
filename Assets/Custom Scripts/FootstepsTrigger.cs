using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsTrigger : MonoBehaviour
{
    public Rigidbody character;   
    private bool _isPlaying = false;
    private bool isMoving = false;
    private uint playingId;
    private GameObject secondaryCollisionGameObj;
    private GameObject registeredCollidingGameObject;
    //public SerialPort arduinoPort;
    private bool firstFrame = true;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
         AkSoundEngine.RegisterGameObj(gameObject);
        _isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        secondaryCollisionGameObj = this.transform.gameObject.GetComponent<CollisionDetectionCustomScript>()._secondaryCollsionObject();
        Debug.Log("Secndary Collision object: "+ secondaryCollisionGameObj);

        bool startsColliding = CollisionDetectionCustomScript.IsTouching(this.transform.gameObject,secondaryCollisionGameObj);

        //Starting the movement
        if(!isMoving 
            && startsColliding
            && CheckForColliderTag(secondaryCollisionGameObj)){
            startMovement();
        }
        //Ending the movement
        else if(isMoving 
                && !registeredCollidingGameObject.Equals(secondaryCollisionGameObj)){
            endMovement();
        }
        //Updating the movement
        else if(isMoving 
                && registeredCollidingGameObject.Equals(secondaryCollisionGameObj)){
            updateMovement();
        }
    }

    private bool CheckForColliderTag(GameObject secondaryCollisionGameObj)
    {
        return (Constants.colliderTagList.Contains(secondaryCollisionGameObj.tag));
    }

    private bool GameObjectInMotion()
    {
        // Calculate the distance between the current position and the previous position
        float distance = Vector3.Distance(transform.position, previousPosition);
        previousPosition = transform.position;
        return (distance > 0f);
    }

    void startMovement(){
        isMoving=true;
        registeredCollidingGameObject = secondaryCollisionGameObj;
        previousPosition = transform.position;
        Debug.Log("Character footsteps movement has been initiated");
    }
    void updateMovement()
    {
        Debug.Log("Character footsteps movement being updated");
        try{
            if(!_isPlaying){
                playingId = AkSoundEngine.PostEvent("FootstepsPlayEvent",gameObject);
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
            Debug.LogError("Exception while playing footsteps audio :" + e);
        }

    }

    void endMovement(){
        isMoving=false;
        _isPlaying = false;
        AkSoundEngine.StopPlayingID(playingId);
        Debug.Log("Character footsteps movement has been terminated");
    }
}