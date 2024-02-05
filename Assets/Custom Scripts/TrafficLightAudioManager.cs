using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrafficLightAudioManager : MonoBehaviour
{
    public float variableDelay = 0f;
    private uint waitSoundplayId = 0;
    private uint walkSoundplayId = 0;
    private float waitInterval = 10f; // Time interval between e1 events
    private float walkInterval = 15f;
    public GameObject vehcileBlockGameObj;

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
               waitSoundplayId = AkSoundEngine.PostEvent(Constants.TRAFFIC_LIGHT_WAIT_SOUND_EVENT,gameObject);
          }
          else if(TrafficLightSound.Equals(Constants.WalkSound)){
               if(waitSoundplayId > 0){
                    AkSoundEngine.StopPlayingID(waitSoundplayId);
                    waitSoundplayId = 0;
               }
               walkSoundplayId = AkSoundEngine.PostEvent(Constants.TRAFFIC_LIGHT_WALK_SOUND_EVENT,gameObject);
          }
     }

     
    private void switchTrafficLightToRed(bool isRed)
    {  
        Renderer trafficLightRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        Material[] materials = trafficLightRenderer.materials;
        if(isRed){
            materials[5].color = materials[2].color;
            trafficLightRenderer.materials = materials;
        }else{
            materials[5].color = materials[3].color;
            trafficLightRenderer.materials = materials;
        }
    }
}
