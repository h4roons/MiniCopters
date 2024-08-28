using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // The target (copter) to follow
    public Vector3 offset;        // Offset position of the camera relative to the target
    public float smoothSpeed = 0.125f;  // How smoothly the camera follows the target

    void LateUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        // Determine the target position for the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera position
        transform.position = smoothedPosition;
    }
}
