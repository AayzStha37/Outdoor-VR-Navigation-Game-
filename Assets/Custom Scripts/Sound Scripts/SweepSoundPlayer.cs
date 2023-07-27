 using UnityEngine;

 using System.Collections;
 using System.IO.Ports;
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
    private bool isMoving = false;
    private uint playingId;
    private GameObject secondaryCollisionGameObj;
    private GameObject registeredCollidingGameObject;
    private string RTPC_CaneInteractionPitch = "CaneInteractionPitch";
    private string RTPC_CaneInteractionVolume = "CaneInteractionVolume";
    private void Awake()
    {
        _lastPosition = this.transform.position;
    }

    private void Start()
    {
        AkSoundEngine.RegisterGameObj(gameObject);
        _isPlaying = false;
    }

    private void Update()
    {
        //Debug.Log("****Data from accelerometer =" + arduinoPort.ReadLine());
        secondaryCollisionGameObj = this.transform.gameObject.GetComponent<CollisionDetectionCustomScript>()._secondaryCollsionObject();
        Debug.Log("Secndary Collision object: "+ secondaryCollisionGameObj);

        bool iscolliding = CollisionDetectionCustomScript.IsTouching(this.transform.gameObject,secondaryCollisionGameObj);

        //Starting the movement
        if(!isMoving && iscolliding){
            startMovement();
        }
        //Ending the movement
        else if(isMoving && !iscolliding){
            endMovement();
        }
        //Updating the movement
        else if(isMoving && registeredCollidingGameObject.Equals(secondaryCollisionGameObj)){
            updateMovement();
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
            AkSoundEngine.SetRTPCValue(RTPC_CaneInteractionVolume,caneinteractionVolume);
            Debug.Log("RTPC Volume : " + caneinteractionVolume);

            float desiredPitch = pitchCurve.Evaluate(velocity);
            float caneinteractionPitch = Mathf.Lerp(150f, desiredPitch, dampness * Time.deltaTime);
            AkSoundEngine.SetRTPCValue(RTPC_CaneInteractionPitch,caneinteractionPitch);         
            Debug.Log("RTPC Pitch : " + caneinteractionPitch);
        }
        }catch(System.Exception e){
            Debug.Log("Exception while playing audio :" + e);
        }

    }

    void endMovement(){
        isMoving=false;
        _isPlaying = false;
        AkSoundEngine.StopPlayingID(playingId);
        Debug.Log("Movement has been terminated");
    }
}
