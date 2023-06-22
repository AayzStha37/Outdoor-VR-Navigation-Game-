using UnityEngine;

[CreateAssetMenu(menuName = "Sweep Sound Audio Clip Randomizer")]
public class SweepSoundAudioClipRandomizer : ScriptableObject
{
    [SerializeField]
    private AudioClip[] clips;

    public void  RandomAuidoClipPicker(out AudioClip clip)
    {
        clip = clips[Random.Range(0, clips.Length)];
    }

    public static implicit operator AudioClip(SweepSoundAudioClipRandomizer v)
    {
        throw new System.NotImplementedException();
    }
}
