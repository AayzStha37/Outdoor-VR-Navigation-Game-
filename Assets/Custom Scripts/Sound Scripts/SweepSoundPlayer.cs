 using UnityEngine;

 using System.Collections;
 using System.IO.Ports;

[RequireComponent(typeof(AudioSource))]
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

    private AudioSource _audioSource;
    private bool _isPlaying = false;
    private Vector3 _lastPosition;
    private bool isMoving = false;
    public GameObject secondaryCollisionGameObj;
    //public SerialPort arduinoPort;
    private AudioClip secondaryGameObjectAudioClip;
    
    private void Awake()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _lastPosition = this.transform.position;
    }

    private void Start()
    {
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;
        _audioSource.Stop();
        _isPlaying = false;

        // arduinoPort = new SerialPort("COM5", 9600); 
        // arduinoPort.Open();
    }

    private void Update()
    {
        
        //Debug.Log("****Data from accelerometer =" + arduinoPort.ReadLine());
        secondaryCollisionGameObj = this.transform.parent.gameObject.GetComponent<CollisionDetectionCustomScript>()._secondaryCollsionObject();
        bool isColliding = CollisionDetectionCustomScript.IsTouching(this.transform.parent.gameObject,secondaryCollisionGameObj);

        //Starting the movement
        if(!isMoving && isColliding){
            startMovement(secondaryCollisionGameObj.GetComponent<AudioSource>());
        }
        //Ending the movement
        else if(isMoving && !isColliding){
            endMovement();
        }
        //Updating the movement
        else if(isMoving && isColliding){
            updateMovement();
        }
        
    }

    void startMovement(AudioSource secondaryCollisionObjAudioSource){
        isMoving=true;
        Debug.Log("Movement has been initiated");
        //TODO make a single generic implementation
        if(secondaryCollisionGameObj.name.Equals("Sidewalk_1111")){        
          secondaryGameObjectAudioClip = secondaryCollisionGameObj.GetComponent<CollisionDetectionCustomScript>()._secondaryCollsionObjectAudioClip();
         _audioSource.clip = secondaryGameObjectAudioClip;
        }else{
        _audioSource.clip =  secondaryCollisionObjAudioSource.clip;
        }
    }
    void updateMovement()
    {
        Debug.Log("Movement being updated");
        float velocity = (this.transform.position - _lastPosition).sqrMagnitude;
        _lastPosition = this.transform.position;
        
        if (velocity > velocityThresold 
            && !_isPlaying)
        {
            _audioSource.Play();
            _isPlaying = true;
        }

        if (_isPlaying)
        {
            velocity *= velocityFactor;
            float desiredVolume = volumeCurve.Evaluate(velocity);
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, desiredVolume, dampness * Time.deltaTime);

            float desiredPitch = pitchCurve.Evaluate(velocity);
            _audioSource.pitch = Mathf.Lerp(_audioSource.pitch, desiredPitch, dampness * Time.deltaTime);
        }

        // if(_isPlaying 
        //     && _audioSource.volume == 0f)
        // {
        //     _audioSource.Pause();
        //     _isPlaying = false;
        // }

    }

    void endMovement(){
        isMoving=false;
        _isPlaying = false;
        _audioSource.Pause();
        Debug.Log("Movement has been terminated");
    }
}
