using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Task3CompletionNotfierSystem : MonoBehaviour
{    
    private float timer = 0f;
    private bool isTiming = false;
    private bool colliderLocked = false;
    private Coroutine timerCoroutine;
    public List<string> targetReachedNameList = new List<string>();

    public void StopTimer(string logText)
    {
        isTiming = false; // Set flag to stop the timer

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine); // Stop the coroutine
        }

        LogTimerInfo(logText); // Log timer information

        timer = 0f;
    }

    public void StartTimer()
    {
        Debug.Log("GAMELOG: Timer started.");
        if (!isTiming)
        {
            isTiming = true;
            timerCoroutine = StartCoroutine(CustomUpdate());
        }
    }

    private void LogTimerInfo(string logText)
    {
        if (logText.Contains(Constants.TASK3_INTERMEDIATE_TARGET))
        {
            Debug.Log($"GAMELOG: Timer stopped. TTR {logText} Target: " + timer.ToString("F2") + " seconds");
        }
        else if (logText.Contains(Constants.TASK3_DESTINATION_TARGET))
        {
            Debug.Log($"GAMELOG: Timer stopped. TTR {logText} Target: " + timer.ToString("F2") + " seconds");
            Debug.Log("POSLOG: Task completed");
        }else{
            Debug.Log($"GAMELOG: Timer FORCE stopped. Timer log: " + timer.ToString("F2") + " seconds");
            Debug.Log("POSLOG: Task FORCE completed");
        }
    }

    private IEnumerator CustomUpdate()
    {
        while (isTiming)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other){
        string otherObjectName = other.gameObject.name;
        if (!colliderLocked && !CheckIfPreviousTarget(otherObjectName))
        {
            if (other.gameObject.name.Contains(Constants.TASK3_INTERMEDIATE_TARGET))
            {
                colliderLocked = true;
                RegisterCollidedTarget(other.gameObject.name);
                StopTimer(Constants.TASK3_INTERMEDIATE_TARGET);
                StartTimer();
            }
            else if (other.gameObject.name.Contains(Constants.TASK3_DESTINATION_TARGET))
            {
                colliderLocked = true;
                RegisterCollidedTarget(other.gameObject.name);
                StopTimer(Constants.TASK3_DESTINATION_TARGET);
                AkSoundEngine.PostEvent(Constants.LEVEL_COMPLETE_SOUND_EVENT, gameObject);
            }
        }
    }

    private  void OnTriggerExit(Collider other) {
        if(other.gameObject.name.Contains(Constants.TASK3_INTERMEDIATE_TARGET))
            colliderLocked = false;
    }

    private void RegisterCollidedTarget(string targetReachedName){
        targetReachedNameList.Add(targetReachedName);
    }
    private bool CheckIfPreviousTarget(string targetName){
        return  targetReachedNameList.Contains(targetName);
    }
}
