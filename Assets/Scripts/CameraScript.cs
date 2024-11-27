using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target; // Reference to the object the camera will follow
    public Vector3 offset = new Vector3(0, 18, -20); // Offset to keep the camera at a fixed distance and angle
    public float smoothSpeed = 0.125f; // Smoothing speed for camera movement

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Calculate the desired position of the camera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Smoothly transition to the desired position
        transform.position = smoothedPosition; // Update the camera's position

        transform.LookAt(target); //Make the camera look at the target
    }
}
