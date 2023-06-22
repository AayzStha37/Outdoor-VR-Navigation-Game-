using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TapSoundScript : MonoBehaviour
{
    [SerializeField]
    private TapSoundAudioClipRandomizer clip;
    public AudioSource _audioSource;

    [SerializeField]
    private float velocityFactor = 0.1f;
    [SerializeField]
    private float velocityThresold = 0.5f;

    //private AudioSource _audioSource;
    private float _lastPlayed;

    private const float WAIT_TIME = 0.1f;


    private void Awake()
    {
        _audioSource = this.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(CanPlay())
        {
            Debug.Log("TAP TAP TAP");
            float vel = collision.relativeVelocity.sqrMagnitude;
            if (vel > velocityThresold)
            {
                clip.PlayClip(_audioSource, out float lenght, vel * velocityFactor);
                _lastPlayed = Time.timeSinceLevelLoad;
            }
        }
    }

    private bool CanPlay()
    {
        return true;
       // return Time.timeSinceLevelLoad - _lastPlayed > WAIT_TIME;
    }
}

