using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSound : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] clips;

    public AudioClip  RandomAuidoClipPicker()
    {
        AudioClip clip = clips[Random.Range(0, clips.Length)];
        return clip;
    }
}
