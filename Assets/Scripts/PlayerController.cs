using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jump = 100f;
    public Rigidbody rb;
    bool isGrounded = true;
    float x, y;
    void Start(){}
    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
    void Update()
    {
        y = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.Translate(x, 0, y);
        if(Input.GetKey("space") && isGrounded){
            rb.AddForce(new Vector3(0f, jump, 0f));
            isGrounded = false;
        }
    }
}
