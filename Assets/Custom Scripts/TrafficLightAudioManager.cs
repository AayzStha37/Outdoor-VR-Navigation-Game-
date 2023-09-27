using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrafficLightAudioManager : MonoBehaviour
{
    private uint waitSoundplayId = 0;
    private uint walkSoundplayId = 0;
    public GameObject frontTrafficLight;
    public GameObject rearTrafficLight;
    private Renderer frontTrafficLightRenderer;
    private Renderer rearTrafficLightRenderer;
    private float timerValue;
    public GameObject vehcileBlockGameObj;
    private void Awake() {
        frontTrafficLightRenderer = frontTrafficLight.GetComponentInChildren<MeshRenderer>();
        rearTrafficLightRenderer = rearTrafficLight.GetComponentInChildren<MeshRenderer>();
    }
    private void Start() {
        playTrafficLightAudio(Constants.WaitSound);
        timerValue = 7.0f;
        switchTrafficLight(true);
        vehcileBlockGameObj.GetComponent<RegisterBlockID>().setStopVehicles(false);
    }

     public void playTrafficLightAudio(String TrafficLightSound)
     {
          if(TrafficLightSound.Equals(Constants.WaitSound) && waitSoundplayId ==0){
               if(walkSoundplayId > 0){
                    AkSoundEngine.StopPlayingID(walkSoundplayId);
                    walkSoundplayId = 0;
               }
               waitSoundplayId = AkSoundEngine.PostEvent("TrafficLightWaitSoundEvent",gameObject);
               vehcileBlockGameObj.GetComponent<RegisterBlockID>().setStopVehicles(false);
               switchTrafficLight(true);
               StartCoroutine(PlayWalkSoundAfterTimer());
          }
          else if(TrafficLightSound.Equals(Constants.WalkSound)){
               if(waitSoundplayId > 0){
                    AkSoundEngine.StopPlayingID(waitSoundplayId);
                    waitSoundplayId = 0;
               }
               walkSoundplayId = AkSoundEngine.PostEvent("TrafficLightWalkSoundEvent",gameObject);
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
        playTrafficLightAudio(Constants.WalkSound);
        switchTrafficLight(false);
        vehcileBlockGameObj.GetComponent<RegisterBlockID>().setStopVehicles(true);
        timerValue = 7.0f;
    }
}
