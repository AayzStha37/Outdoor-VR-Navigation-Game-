using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
   public AudioSource audioSource;
   public AudioClip audioClip;
   public string audioFilePath;

    private void Start() {
        audioFilePath = "file://" + Application.persistentDataPath+"/SoundFiles";
        //audioSource = gameObject.AddComponent<AudioSource>();
        //StartCoroutine(loadAudio("concretePlane_sweep_soundInfo.mp3"));
    }

    public void playGestureAudio(string audioFileName){
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(loadAudio(audioFileName));
    }

   private IEnumerator loadAudio(string audioFileName){
    
    Debug.Log("Inside loadAudio");
        WWW request = GetAudioFromFile(audioFilePath, audioFileName);
        yield return request;

        audioClip = request.GetAudioClip();
        audioClip.name = audioFileName;

        PlayAudioFile();
   }

   private void PlayAudioFile(){
    audioSource.clip = audioClip;
    audioSource.Play();
    audioSource.loop = false;
    Debug.Log("Inside PlayAudioFile");
   }

   private WWW GetAudioFromFile(string audioFilePath, string audioFileName){
        string audioToPlay = string.Format(audioFilePath+"{0}",audioFileName);
        WWW request = new WWW(audioToPlay);
        
    Debug.Log("Inside GetAudioFromFile");
        return request;
   }
}
