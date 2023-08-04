using UnityEngine;

public class FollowParentPosition : MonoBehaviour
{
    private Transform parentTransform;  // Reference to the parent's transform

    private void Start()
    {
        // Get the parent's transform
        parentTransform = transform.parent;
        
        // Check if a parent transform exists
        if (parentTransform == null)
        {
            Debug.LogWarning("Parent transform not found!");
            enabled = false;  // Disable this script
        }
    }

    private void Update()
    {
        // Update the child's position to match the parent's position
        transform.position = parentTransform.position;
    }
}
