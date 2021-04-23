using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float maxSpeed = 100f;
    public float acceleration = 10f;
    public float jumpForce = 100f;
    public float rotationSpeed = 5f;

    Rigidbody rb;
    ConfigurableJoint cj;
    bool grounded = false;

    Vector3 forward;
    Vector3 right;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cj = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
    }

    void SetForwardAndRightVectors()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float jump = Input.GetAxisRaw("Jump");
        float run = Input.GetAxisRaw("Run");

        SetForwardAndRightVectors();

        Vector3 direction = (right * h + forward * v).normalized;
        Vector3 eyesForward = transform.forward;
        eyesForward.y = 0f;



        if (grounded)
        {
            if (direction.magnitude > 0.001f)
            {
                
                float angle = Vector3.SignedAngle(eyesForward, direction, Vector3.up);

                if (Mathf.Abs(angle) > 2.5f)
                {
                    Quaternion clockwise = cj.targetRotation * Quaternion.AngleAxis(-angle * Time.deltaTime * rotationSpeed, Vector3.up);
                    cj.targetRotation = clockwise;
                }
                

            }

            rb.velocity = Vector3.ClampMagnitude(rb.velocity + direction * acceleration * Time.deltaTime, maxSpeed + maxSpeed*run);
            rb.AddForce(new Vector3(0f, jumpForce * jump, 0f));
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag == "Ground")
        {
            grounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            grounded = true;
        }
    }
}
