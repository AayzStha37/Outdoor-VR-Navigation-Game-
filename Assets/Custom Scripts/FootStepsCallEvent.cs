using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsCallEvent : MonoBehaviour
{
    public string eventName;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("**Start footesteps");
        //AkSoundEngine.PostEvent(eventName, gameObject);
        Debug.Log("**End footesteps");
    }
}
