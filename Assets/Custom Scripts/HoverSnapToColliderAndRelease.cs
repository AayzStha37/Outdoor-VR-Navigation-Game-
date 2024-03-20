using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**This function is not currently being used**//

public class HoverSnapToColliderAndRelease : MonoBehaviour
{
    public float triggerHeight = 0.2f; // Height from collider to activate/release
    public float hoverEpsilon = 0.98f; // Minimum distance considered touching

    private bool isSnapped = false; // Flag to track snapping state

    void Update()
    {
        // Raycast down from the object to check for colliders
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Calculate the object's current height from the collider
            float currentHeight = transform.position.y - hit.point.y;

            // Check for snapping and releasing conditions
            if (!isSnapped && currentHeight <= triggerHeight + hoverEpsilon)
            {
                // Snap the object if not snapped and below trigger height
                transform.position = new Vector3(transform.position.x, hit.point.y + hoverEpsilon, transform.position.z);
                isSnapped = true; // Set snapped flag
            }
            else if (isSnapped && currentHeight > triggerHeight + hoverEpsilon)
            {
                // Release the object if snapped and above or equal to trigger height
                isSnapped = false; // Reset snapped flag
            }
        }
        else
        {
            // Reset snapped flag if no collider is hit
            isSnapped = false;
        }
    }
}
