using System;
using UnityEngine;
using System.Collections.Generic;

public class MainThreadDispatcher : MonoBehaviour
{
    private static readonly Queue<Action> executionQueue = new Queue<Action>();

    private void Update()
    {
        while (executionQueue.Count > 0)
        {
            Action action;
            lock (executionQueue)
            {
                action = executionQueue.Dequeue();
            }
            action?.Invoke();
        }
    }

    public static void ExecuteInMainThread(Action action)
    {
        lock (executionQueue)
        {
            executionQueue.Enqueue(action);
        }
    }
}
