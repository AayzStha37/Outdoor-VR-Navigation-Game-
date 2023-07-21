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
    private string RTPC_CharacterNavSpeed = "CharacterNavSpeed";
    private bool firstFrame = true;

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
        if(!isMoving && startsColliding){
            startMovement();
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

    void startMovement(){
        isMoving=true;
        registeredCollidingGameObject = secondaryCollisionGameObj;
        Debug.Log("Movement has been initiated");
    }
    void updateMovement()
    {
        Debug.Log("Movement being updated");
        try{
            if(!_isPlaying){
                playingId = AkSoundEngine.PostEvent("FootstepsPlayEvent",gameObject);
                Debug.Log("Audio play initiated");          
                _isPlaying = true;
            }
            if(_isPlaying){     
                float characterNavSpeed = character.velocity.magnitude;
                AkSoundEngine.SetRTPCValue(RTPC_CharacterNavSpeed,characterNavSpeed);         
                Debug.Log("RTPC footsteps Volume : " + characterNavSpeed);
            }
        }catch(System.Exception e){
            Debug.Log("Exception while playing footsteps audio :" + e);
        }

    }

    void endMovement(){
        isMoving=false;
        _isPlaying = false;
        AkSoundEngine.StopPlayingID(playingId);
        Debug.Log("Movement has been terminated");
    }
}