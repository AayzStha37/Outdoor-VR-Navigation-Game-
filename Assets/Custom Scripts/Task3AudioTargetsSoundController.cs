using UnityEngine;

public class Task3AudioTargetsSoundController : MonoBehaviour
{
    private uint playingId;
    private void Start() {
        AkSoundEngine.RegisterGameObj(gameObject);
        playingId = AkSoundEngine.PostEvent("AmbientSoundEvent",gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if(Constants.MAIN_CHARACTER_BODY.Equals(other.gameObject.name)){
            AkSoundEngine.StopPlayingID(playingId);
        }
    }
}
