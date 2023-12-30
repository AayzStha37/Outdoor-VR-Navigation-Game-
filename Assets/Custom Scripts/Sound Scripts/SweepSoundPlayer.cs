 using UnityEngine;

 using System.Collections;
 using System.IO.Ports;
using System;
using UnityEngine.InputSystem;
public class SweepSoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve pitchCurve;
    [SerializeField]
    private AnimationCurve volumeCurve;
    [SerializeField]
    private float velocityThresold = 0.5f;
    [SerializeField]
    private float velocityFactor = 0.1f;
    [SerializeField]
    private float dampness = 10f;
    private bool _isPlaying = false;
    private Vector3 _lastPosition;
    private bool startsInteraction = false;
    private uint playingId;
    private GameObject secondaryCollisionGameObj;
    private GameObject registeredCollidingGameObject;
    private Vector3 lastPosition;
    private ServerConnection serverConnection;
    public InputActionProperty controllerVelocityProperty;

    private void Awake()
    {
        _lastPosition = this.transform.position;
    }

    private void Start()
    {
        AkSoundEngine.RegisterGameObj(gameObject);
        serverConnection = GetComponent<ServerConnection>();
        _isPlaying = false;
    }

    private void Update()
    {
        //Debug.Log("****Data from accelerometer =" + arduinoPort.ReadLine());
        secondaryCollisionGameObj = this.transform.gameObject.GetComponent<CollisionDetectionCustomScript>()._secondaryCollsionObject();
        Debug.Log("Secndary Collision object: "+ secondaryCollisionGameObj);

        bool startsColliding = CollisionDetectionCustomScript.IsTouching(this.transform.gameObject,secondaryCollisionGameObj);

        //Starting the movement
        if(!startsInteraction && startsColliding && Constants.COLLIDER_TAG_LIST.Contains(secondaryCollisionGameObj.tag)){
            startMovement();
        }
        //Ending the movement
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
        serverConnection.SendDataToServer(Constants.COLLISION_ON, secondaryCollisionGameObj.tag);
        Debug.Log("Movement has been initiated");
    }
    void updateMovement()
    {
        Debug.Log("Movement being updated");
        float velocity = (this.transform.position - _lastPosition).sqrMagnitude;
        _lastPosition = this.transform.position;
        try{
        if (velocity > velocityThresold 
            && !_isPlaying
            && secondaryCollisionGameObj.transform.parent != null 
            && !Constants.TASK1_THIRD_TASK_OBJECT_TAG.Equals(secondaryCollisionGameObj.transform.parent.tag))
        {
            playingId = AkSoundEngine.PostEvent("GroundSweepEvent",gameObject);
            Debug.Log("Audio play initiated");
            _isPlaying = true;
        }
        
        if (_isPlaying)
        {
            Debug.Log("Movement and audio play ongoing");

            if(secondaryCollisionGameObj.transform.parent != null &&
                !Constants.TASK1_SECOND_TASK_OBJECT_TAG.Equals(secondaryCollisionGameObj.transform.parent.tag))
            {
                serverConnection.SendVelocityDataToServer(controllerVelocityProperty.action.ReadValue<Vector3>());
            }

            velocity *= velocityFactor;
            float desiredVolume = volumeCurve.Evaluate(velocity);
            float caneinteractionVolume = Mathf.Lerp(100f, desiredVolume, dampness * Time.deltaTime);
            AkSoundEngine.SetRTPCValue(Constants.RTPC_CaneInteractionVolume,caneinteractionVolume);
            Debug.Log("RTPC Volume : " + caneinteractionVolume);

            float desiredPitch = pitchCurve.Evaluate(velocity);
            float caneinteractionPitch = Mathf.Lerp(150f, desiredPitch, dampness * Time.deltaTime);
            AkSoundEngine.SetRTPCValue(Constants.RTPC_CaneInteractionPitch,caneinteractionPitch);         
            Debug.Log("RTPC Pitch : " + caneinteractionPitch);
        }
        }catch(System.Exception e){
            Debug.Log("Exception while playing audio :" + e);
        }

    }

    void endMovement(){
        startsInteraction=false;
        _isPlaying = false;
        AkSoundEngine.StopPlayingID(playingId);
        serverConnection.SendDataToServer(Constants.COLLISION_OFF, "");
        Debug.Log("Movement has been terminated");
    }
}
