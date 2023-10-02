using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightTriggerManager : MonoBehaviour
{
    private bool lockResource = false;
    private float timerValue = 0.0f;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals(Constants.WhiteCaneTipTag) && !lockResource){
             timerValue = 8.0f;
            lockResource = true;
            //this.gameObject.GetComponentInParent<TrafficLightAudioManager>().playTrafficLightAudio(Constants.WaitSound);
            StartCoroutine(LockResourceTimer());
        }
    }
    public IEnumerator LockResourceTimer()
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
        timerValue = 8.0f;
        lockResource = false;
    }
}
