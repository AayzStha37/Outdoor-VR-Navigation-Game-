using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightSoundPlayer : MonoBehaviour
{
    public GameObject frontTrafficLight;
    public GameObject rearTrafficLight;
    public AudioClip waitSound;
    public AudioClip walkSound;
    private AudioSource frontTrafficLightAudioSource;
    private AudioSource rearTrafficLightaudioSource; 
    private Renderer frontTrafficLightRenderer;
    private Renderer rearTrafficLightRenderer;
    private float timerValue;
    private bool lockResource = false;

    private void Awake() {
        frontTrafficLightAudioSource = frontTrafficLight.GetComponent<AudioSource>();
        rearTrafficLightaudioSource =  rearTrafficLight.GetComponent<AudioSource>();

        frontTrafficLightRenderer = frontTrafficLight.GetComponent<MeshRenderer>();
        rearTrafficLightRenderer = rearTrafficLight.GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name.Equals("sphere tip")
            && !lockResource){
            lockResource = true;
            playTrafficLightAudio(waitSound);
            switchTrafficLight(true);
            StartCoroutine(PlayWalkSoundAfterTimer());
        }
    }

    private void switchTrafficLight(bool isRed)
    {
        Material[] materials =  frontTrafficLightRenderer.materials;
        if(isRed){
            materials[5].color = materials[2].color;
            frontTrafficLightRenderer.materials = materials;
            rearTrafficLightRenderer.materials = materials;
        }else{
            materials[5].color = materials[3].color;
            frontTrafficLightRenderer.materials = materials;
            rearTrafficLightRenderer.materials = materials;
        }
    }

    private void playTrafficLightAudio(AudioClip audioClip)
    {
        frontTrafficLightAudioSource.clip = audioClip;
        rearTrafficLightaudioSource.clip = audioClip;

        frontTrafficLightAudioSource.Play();
        rearTrafficLightaudioSource.Play();
    }
    
    void Start()
    {
        timerValue = 10.0f;
        playTrafficLightAudio(waitSound);
        switchTrafficLight(true);
        frontTrafficLightAudioSource.loop = true;
        frontTrafficLightAudioSource.playOnAwake = true;

        rearTrafficLightaudioSource.loop = true;
        rearTrafficLightaudioSource.playOnAwake = true;
    }
     public IEnumerator PlayWalkSoundAfterTimer()
    {
        while (timerValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timerValue--;
        }
        playTrafficLightAudio(walkSound);
        switchTrafficLight(false);
        timerValue = 10.0f;
        lockResource = false;
    }
}
