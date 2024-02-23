using UnityEngine;

public class Task3AudioTargetsSoundController : MonoBehaviour
{
    private uint playingId;
    private bool isAlreadyPlayed;

    private void Start() {
        isAlreadyPlayed = false;
        AkSoundEngine.RegisterGameObj(gameObject);
        if (!gameObject.tag.Equals(Constants.PEDESTRIAN_CONVERSATION_TAG)) {
            playingId = AkSoundEngine.PostEvent(Constants.AMBIENT_SOUND_EVENT, gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (Constants.MAIN_CHARACTER_BODY.Equals(other.gameObject.name)) {
            if (gameObject.tag.Equals(Constants.PEDESTRIAN_CONVERSATION_TAG) && !isAlreadyPlayed) {
                playingId = AkSoundEngine.PostEvent(Constants.AMBIENT_SOUND_EVENT, gameObject);
                isAlreadyPlayed = true;
            } else if(!gameObject.tag.Equals(Constants.LOOP_SOUND_TAG)) {
                AkSoundEngine.StopPlayingID(playingId);
            }
        }
    }
}
