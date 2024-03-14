using UnityEngine;

public class LimitCameraInsideParent : MonoBehaviour
{
   private BoxCollider mainCharacterCollider;

    void Start()
    {
        // Try to find the grandparent's BoxCollider component
        Transform mainCharacterTransform = transform.parent.parent;
        if (mainCharacterTransform != null)
        {
            mainCharacterCollider = mainCharacterTransform.GetComponent<BoxCollider>();
        }
    }

    void LateUpdate()
    {
        // Check if the grandparent collider is found
        if (mainCharacterCollider == null)
            return;

        // Get the main camera's current position
        Vector3 currentPosition = transform.position;

        // Calculate the half dimensions of the grandparent collider
        Vector3 halfExtents = mainCharacterCollider.bounds.extents;

        // Calculate the minimum and maximum allowed positions for the camera
        Vector3 minPosition = mainCharacterCollider.transform.position - halfExtents;
        Vector3 maxPosition = mainCharacterCollider.transform.position + halfExtents;

        // Clamp the camera position within the allowed bounds
        currentPosition.x = Mathf.Clamp(currentPosition.x, minPosition.x, maxPosition.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minPosition.y, maxPosition.y);
        currentPosition.z = Mathf.Clamp(currentPosition.z, minPosition.z, maxPosition.z);

        // Update the camera position
        transform.position = currentPosition;
    }
}
