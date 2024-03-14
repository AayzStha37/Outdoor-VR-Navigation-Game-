using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Task2CompletionNotfierSystem : MonoBehaviour
{    
    private float timer = 0f;
    private bool isTiming = false;
    private bool colliderLocked = false;
    private Coroutine timerCoroutine;

    private void StopTimer()
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
        if(otherGameObject.name.Contains(Constants.TASK2_COMPLETION_COLLIDER) && !colliderLocked){
            colliderLocked = true;
            StopTimer();
            otherGameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
