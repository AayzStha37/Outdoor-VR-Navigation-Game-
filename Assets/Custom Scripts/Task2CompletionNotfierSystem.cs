using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Task2CompletionNotfierSystem : MonoBehaviour
{    
    private float timer = 0f;
    private bool isTiming = false;
    private bool firstTaskColliderLocked = false;
    private bool secondTaskColliderLocked = false;
    private Coroutine timerCoroutine;

    public void StopTimer()
    {
        if (isTiming)
        {
            Debug.Log("GAMELOG: Timer stopped. Task Completion time: " + timer.ToString("F2") + " seconds");
            Debug.Log("POSLOG: Task completed");

            isTiming = false;
            timer = 0f;

            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
        }
    }

    public void StartTimer()
    {
        if (!isTiming)
        {
            Debug.Log("GAMELOG: Timer started.");
            isTiming = true;
            timerCoroutine = StartCoroutine(CustomUpdate());
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

    private void OnTriggerEnter(Collider other) {
        GameObject otherGameObject = other.gameObject;
        if(otherGameObject.name.Equals(Constants.TASK2_COMPLETION_COLLIDER_1) && !firstTaskColliderLocked){
            firstTaskColliderLocked = true;
            StopTimer();
           
        }
        else if(otherGameObject.name.Equals(Constants.TASK2_COMPLETION_COLLIDER_2) && !secondTaskColliderLocked){
            secondTaskColliderLocked = true;
            StopTimer();
            
        }
    }

    public void SetColliderFlagsToDefaultValue(){
        firstTaskColliderLocked = false;
        secondTaskColliderLocked = false;
    }
}
