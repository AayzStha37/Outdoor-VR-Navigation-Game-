using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Task2CompletionNotfierSystem : MonoBehaviour
{    
    private float timer = 0f;
    private bool isTiming = false;
    private bool firstCollision = false;
    private void Start() {
        startTimer();
    }
    private void stopTimer()
    {
        Debug.Log("GAMELOG: Timer stopped. Task Completion time: " + timer.ToString("F2") + " seconds");
        Debug.Log("POSLOG: Task completed");
        timer = 0f;
        isTiming = false;
        StopCoroutine(CustomUpdate());
    }

    private void startTimer()
    {
        Debug.Log("GAMELOG: Timer started.");
        if (!isTiming)
        {
            isTiming = true;
            StartCoroutine(CustomUpdate());
        }
        
    }

    IEnumerator CustomUpdate()
    {
        while (isTiming)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name.Contains("Completion collider") && !firstCollision){
            firstCollision = true;
            stopTimer();
        }
    }
}
