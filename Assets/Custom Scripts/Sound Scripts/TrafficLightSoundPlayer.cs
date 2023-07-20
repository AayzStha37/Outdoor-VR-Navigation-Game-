using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightSoundPlayer : MonoBehaviour
{
    public GameObject frontTrafficLight;
    public GameObject rearTrafficLight;
    private Renderer frontTrafficLightRenderer;
    private Renderer rearTrafficLightRenderer;
    private float timerValue;
    private bool lockResource = false;
    private const string SPHERE_TIP = "sphere tip";
    public GameObject vehcileBlockGameObj;
    private void Awake() {
        frontTrafficLightRenderer = frontTrafficLight.GetComponentInChildren<MeshRenderer>();
        rearTrafficLightRenderer = rearTrafficLight.GetComponentInChildren<MeshRenderer>();
    }
    void Start()
    {
        timerValue = 10.0f;
        playTrafficLightWaitAudio(true);
        switchTrafficLight(true);
        initFunc();
    }

    private void initFunc()
    {
        vehcileBlockGameObj.GetComponent<RegisterBlockID>().setStopVehicles(false);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name.Equals(SPHERE_TIP) && !lockResource){
            lockResource = true;
            vehcileBlockGameObj.GetComponent<RegisterBlockID>().setStopVehicles(false);
            playTrafficLightWaitAudio(true);
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

    private void playTrafficLightWaitAudio(bool waitSound)
    {
        if(waitSound)
            AkSoundEngine.PostEvent("TrafficLightWaitSoundEvent",gameObject);
        else
            AkSoundEngine.PostEvent("TrafficLightWalkSoundEvent",gameObject);
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
        playTrafficLightWaitAudio(false);
        switchTrafficLight(false);
        vehcileBlockGameObj.GetComponent<RegisterBlockID>().setStopVehicles(true);
        timerValue = 10.0f;
        lockResource = false;
    }
}
