using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public float sensitivity = 2f;     // Mouse sensitivity
    public float pitchLimit = 80f;     // Clamp look up/down

    private float pitch = 0f;          // Current up/down rotation

    void Update()
    {
        // --- Mouse Input ---
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Apply pitch (camera only looks up/down)
        pitch -= mouseY * sensitivity;
        pitch = Mathf.Clamp(pitch, -pitchLimit, pitchLimit);

        // Rotate parent drone around Y (yaw) with mouseX
        transform.parent.Rotate(Vector3.up * mouseX * sensitivity);

        // Apply pitch rotation only to camera (local X rotation)
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
