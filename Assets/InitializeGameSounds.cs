using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGameSounds : MonoBehaviour
{
    
    private uint playingId;
    // Start is called before the first frame update
    void Start()
    {   
        //playing Start scene traffic sound
        playingId = AkSoundEngine.PostEvent("StartSceneTrafficSoundEvent",gameObject);
    }
    private void OnDestroy() {
        AkSoundEngine.StopPlayingID(playingId);
    }

}
