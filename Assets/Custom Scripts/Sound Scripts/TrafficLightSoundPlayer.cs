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
    private const string SPHERE_TIP = "sphere tip";
    public GameObject vehcileBlockGameObj;
    private void Awake() {
        frontTrafficLightAudioSource = frontTrafficLight.GetComponent<AudioSource>();
        rearTrafficLightaudioSource =  rearTrafficLight.GetComponent<AudioSource>();

        frontTrafficLightRenderer = frontTrafficLight.GetComponentInChildren<MeshRenderer>();
        rearTrafficLightRenderer = rearTrafficLight.GetComponentInChildren<MeshRenderer>();
    }
    void Start()
    {
        timerValue = 10.0f;
        playTrafficLightAudio(waitSound);
        switchTrafficLight(true);
        initFunc();
    }

    private void initFunc()
    {
        vehcileBlockGameObj.GetComponent<RegisterBlockID>().setStopVehicles(false);
        frontTrafficLightAudioSource.loop = true;
        frontTrafficLightAudioSource.playOnAwake = true;

        rearTrafficLightaudioSource.loop = true;
        rearTrafficLightaudioSource.playOnAwake = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name.Equals(SPHERE_TIP) && !lockResource){
            lockResource = true;
            vehcileBlockGameObj.GetComponent<RegisterBlockID>().setStopVehicles(false);
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

    public IEnumerator PlayWalkSoundAfterTimer()
    {
        while (timerValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timerValue--;
        }
        exitFunc();
    }

    private void exitFunc()
    { 
        playTrafficLightAudio(walkSound);
        switchTrafficLight(false);
        vehcileBlockGameObj.GetComponent<RegisterBlockID>().setStopVehicles(true);
        timerValue = 10.0f;
        lockResource = false;
    }
}
