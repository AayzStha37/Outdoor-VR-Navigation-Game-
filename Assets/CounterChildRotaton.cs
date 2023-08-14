using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterChildRotaton : MonoBehaviour
{
    private Quaternion lastParentRotation;
    private Vector3 offsetFromParent;
    private void Start() {
        lastParentRotation = transform.parent.localRotation;
        offsetFromParent = transform.position - transform.parent.position;
   
    }
    void Update ()
    {
        transform.localRotation = Quaternion.Inverse(transform.parent.localRotation) * lastParentRotation * transform.localRotation;
        Vector3 desiredPosition = transform.parent.position + offsetFromParent;
        transform.position = desiredPosition;
        lastParentRotation = transform.parent.localRotation;
    }
}
