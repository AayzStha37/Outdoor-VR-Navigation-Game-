using UnityEngine;

public class HoverSnapToCollider : MonoBehaviour
{
    public float hoverEpsilon; // Minimum distance considered touching

    void Update()
    {
        // Raycast down from the object to check for colliders
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Check if collider is not attached to a child of this GameObject
            if (!IsDescendant(transform, hit.collider.transform))
            {
                // Calculate the desired y position to exactly touch the collider
                float targetY = hit.point.y + hoverEpsilon;

                // Snap the object's y position to the target
                transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            }
        }
    }

    // Helper function to check if a transform is a descendant of another
    private bool IsDescendant(Transform parent, Transform child)
    {
        while (child != null)
        {
            if (child == parent)
            {
                return true;
            }
            child = child.parent;
        }
        return false;
    }
}