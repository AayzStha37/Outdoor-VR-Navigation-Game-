using UnityEngine;

public class Task3AudioTargetsSoundController : MonoBehaviour
{
    private uint playingId;
    private bool isAlreadyPlayed = false;

    private void Start() {
        AkSoundEngine.RegisterGameObj(gameObject);
        if (!gameObject.tag.Equals(Constants.PEDESTRIAN_CONVERSATION_TAG)) {
            playingId = AkSoundEngine.PostEvent(Constants.AMBIENT_SOUND_EVENT, gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (Constants.MAIN_CHARACTER_BODY.Equals(other.gameObject.name)
            && gameObject.tag.Equals(Constants.PEDESTRIAN_CONVERSATION_TAG) 
            && !isAlreadyPlayed) {
                playingId = AkSoundEngine.PostEvent(Constants.AMBIENT_SOUND_EVENT, gameObject);
                isAlreadyPlayed = true;
        }
    }
}
