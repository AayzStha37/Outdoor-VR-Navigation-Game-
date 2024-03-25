 using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Interhaptics.Core;
using Interhaptics.HapticBodyMapping;
using Interhaptics;

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
    private bool isAudioPlaying = false;
     private bool isHapticPlaying = false;
    private Vector3 _lastPosition;
    private bool startsInteraction = false;
    private uint playingId;
    private GameObject secondaryCollisionGameObj;
    private GameObject registeredCollidingGameObject;
    private Vector3 lastPosition;
    public InputActionProperty controllerVelocityProperty;
    private HapticMaterial dynamicHapticMaterial;
    private RandomHapticMaterialSelector hapticsSelector;

    private void Awake()
    {
        _lastPosition = this.transform.position;
    }

    private void Start()
    {
        AkSoundEngine.RegisterGameObj(gameObject);
        isAudioPlaying = false;
        isHapticPlaying =false;
        hapticsSelector = GetComponent<RandomHapticMaterialSelector>();
    }

    private void Update()
    {
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
        setHapticMaterial();
        Debug.Log("Movement has been initiated");
    }

    void updateMovement()
    {
        Debug.Log("Movement being updated");
        float controllerVelocityMag = controllerVelocityProperty.action.ReadValue<Vector3>().magnitude;
        Debug.Log("Controller Velcoity : " + controllerVelocityMag);
        //(this.transform.position - _lastPosition).sqrMagnitude;
        //_lastPosition = this.transform.position;
        try{
        if (controllerVelocityMag > velocityThresold 
            && secondaryCollisionGameObj.transform.parent != null )
        {
            //Play audio
            if(!isAudioPlaying)
                playAudio();
            //Play haptics   
            if(!isHapticPlaying)     
                playHaptics();    
        }
            
        if (isAudioPlaying || isHapticPlaying)
        {
            Debug.Log("Movement and audio play ongoing");

            controllerVelocityMag *= velocityFactor;
            float desiredVolume = volumeCurve.Evaluate(controllerVelocityMag);
            float caneinteractionVolume = Mathf.Lerp(100f, desiredVolume, dampness * Time.deltaTime);

            float desiredPitch = pitchCurve.Evaluate(controllerVelocityMag);
            float caneinteractionPitch = Mathf.Lerp(150f, desiredPitch, dampness * Time.deltaTime);

            if(isAudioPlaying)
                caneSweepAudioControl(caneinteractionVolume,caneinteractionPitch);
            if(isHapticPlaying)
                caneHapticFeedbackControl(caneinteractionVolume);
        }
        }catch(System.Exception e){
            Debug.LogError("Exception while playing audio :" + e);
        }

    }

    private void caneSweepAudioControl(float caneinteractionVolume, float caneinteractionPitch)
    {
        AkSoundEngine.SetRTPCValue(Constants.RTPC_CaneInteractionVolume,caneinteractionVolume);
        Debug.Log("RTPC Volume : " + caneinteractionVolume);
        AkSoundEngine.SetRTPCValue(Constants.RTPC_CaneInteractionPitch,caneinteractionPitch);         
        Debug.Log("RTPC Pitch : " + caneinteractionPitch);
    }

    private void playHaptics()
    {
        if(!Constants.TASK1_SECOND_TASK_OBJECT_TAG.Equals(secondaryCollisionGameObj.transform.parent.tag)){
            caneHapticFeedbackControl(0);
            isHapticPlaying = true;
        }
    }

    private void playAudio()
    {
        if(!Constants.TASK1_THIRD_TASK_OBJECT_TAG.Equals(secondaryCollisionGameObj.transform.parent.tag)){
            playingId = AkSoundEngine.PostEvent(Constants.GROUND_SWEEP_EVENT,gameObject);
            Debug.Log("Audio play initiated");
            isAudioPlaying = true;
        }
    }

    private void setHapticMaterial()
    {           
        HapticMaterial hapticMaterial = hapticsSelector.GetRandomHapticFile(secondaryCollisionGameObj.tag);
        if(null!=hapticMaterial){
            dynamicHapticMaterial = hapticMaterial;
        }else{
            Debug.LogError("Invalid haptic material/Material List Empty");
        }
    }

    private void caneHapticFeedbackControl(double customIntensityVal)
    {
        HAR.SetGlobalIntensity(customIntensityVal == 0 ? 0.5 : customIntensityVal);
        HAR.StopCurrentHapticEffect();
        Debug.Log("Haptics Intensity: "+HAR.GetGlobalIntensity()/100);
        HAR.PlayHapticEffect(dynamicHapticMaterial,HAR.GetGlobalIntensity()/100,10,LateralFlag.Right);
    }

    void endMovement(){
        startsInteraction=false;
        isAudioPlaying = false;
        AkSoundEngine.StopPlayingID(playingId);
        HAR.StopCurrentHapticEffect();
        Debug.Log("Movement has been terminated");
    }
}
