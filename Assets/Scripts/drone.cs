using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;         // Forward/sideways speed
    public float ascendSpeed = 3f;       // Up/Down speed
    public float acceleration = 5f;      // How fast drone reaches target velocity

    [Header("Rotation Settings")]
    public float tiltAmount = 15f;       // Max tilt angle (degrees) for pitch/roll
    public float tiltSmooth = 5f;        // Smoothness of tilt
    public float yawSpeed = 2f;          // Mouse horizontal sensitivity

    private Rigidbody rb;
    private Vector3 currentVelocity;

    private float yaw;   // Yaw rotation (left/right)
    private float pitch; // Tilt forward/back
    private float roll;  // Tilt sideways

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  
        rb.drag = 1f;
        // Hide the cursor
        Cursor.visible = false;

        // Lock it to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- Input ---
        float moveX = Input.GetAxis("Horizontal");  // A/D
        float moveZ = Input.GetAxis("Vertical");    // W/S

        float moveY = 0f;
        if (Input.GetKey(KeyCode.Space)) moveY = 1f;
        if (Input.GetKey(KeyCode.LeftShift)) moveY = -1f;

        // Target velocity
        Vector3 targetVelocity = 
            (transform.forward * moveZ * moveSpeed) +
            (transform.right * moveX * moveSpeed) +
            (Vector3.up * moveY * ascendSpeed);

        // Smoothly move towards target velocity
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * acceleration);

        // Apply velocity
        rb.velocity = currentVelocity;

        // --- Rotation ---
        // Mouse X controls yaw
        yaw += Input.GetAxis("Mouse X") * yawSpeed;

        // Tilt based on movement input
        pitch = Mathf.Lerp(pitch, -moveZ * tiltAmount, Time.deltaTime * tiltSmooth);
        roll = Mathf.Lerp(roll, -moveX * tiltAmount, Time.deltaTime * tiltSmooth);

        // Apply rotation
        transform.rotation = Quaternion.Euler(-pitch, yaw, roll);
    }
}
