using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrafficLightAudioManager : MonoBehaviour
{
    public float variableDelay = 0f;
    private uint waitSoundplayId = 0;
    private uint walkSoundplayId = 0;
    public GameObject frontTrafficLight;
    public GameObject rearTrafficLight;
    private Renderer frontTrafficLightRenderer;
    private Renderer rearTrafficLightRenderer;
    private float waitInterval = 10f; // Time interval between e1 events
    private float walkInterval = 15f;
    public GameObject vehcileBlockGameObj;
    private void Awake() {
        frontTrafficLightRenderer = frontTrafficLight.GetComponentInChildren<MeshRenderer>();
        rearTrafficLightRenderer = rearTrafficLight.GetComponentInChildren<MeshRenderer>();
    }
    private void Start() {
        StartCoroutine(PlayTrafficEvents());
    }
    private IEnumerator PlayTrafficEvents()
    {
        yield return new WaitForSeconds(variableDelay);

        while (true)
        {
            playTrafficLightAudio(Constants.WaitSound);
            switchTrafficLightToRed(true);
            vehcileBlockGameObj.GetComponent<RegisterBlockID>().setStopVehicles(false);
            yield return new WaitForSeconds(walkInterval);

            playTrafficLightAudio(Constants.WalkSound);
            switchTrafficLightToRed(false);
            vehcileBlockGameObj.GetComponent<RegisterBlockID>().setStopVehicles(true);
            yield return new WaitForSeconds(waitInterval);
        }
    }

     public void playTrafficLightAudio(String TrafficLightSound)
     {
          if(TrafficLightSound.Equals(Constants.WaitSound) && waitSoundplayId ==0){
               if(walkSoundplayId > 0){
                    AkSoundEngine.StopPlayingID(walkSoundplayId);
                    walkSoundplayId = 0;
               }
               waitSoundplayId = AkSoundEngine.PostEvent("TrafficLightWaitSoundEvent",gameObject);
          }
          else if(TrafficLightSound.Equals(Constants.WalkSound)){
               if(waitSoundplayId > 0){
                    AkSoundEngine.StopPlayingID(waitSoundplayId);
                    waitSoundplayId = 0;
               }
               walkSoundplayId = AkSoundEngine.PostEvent("TrafficLightWalkSoundEvent",gameObject);
          }
     }

     
    private void switchTrafficLightToRed(bool isRed)
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
}
