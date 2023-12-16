 using UnityEngine;

 using System.Collections;
 using System.IO.Ports;
using System;
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
            serverConnection.SendDataToServer(Constants.COLLISION_ON, secondaryCollisionGameObj.tag);
        }
        //Ending the movement
        else if(startsInteraction && (!startsColliding || CheckIfStationary() || !registeredCollidingGameObject.Equals(secondaryCollisionGameObj) )){
            endMovement();
            serverConnection.SendDataToServer(Constants.COLLISION_OFF, "");
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
        Debug.Log("Movement has been initiated");
    }
    void updateMovement()
    {
        Debug.Log("Movement being updated");
        float velocity = (this.transform.position - _lastPosition).sqrMagnitude;
        _lastPosition = this.transform.position;
        try{
            if (velocity > velocityThresold 
            && !_isPlaying)
        {
            playingId = AkSoundEngine.PostEvent("GroundSweepEvent",gameObject);
            Debug.Log("Audio play initiated");
            _isPlaying = true;
        }
        
        if (_isPlaying)
        {
            Debug.Log("Movement and audio play ongoing");
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
        Debug.Log("Movement has been terminated");
    }
}
