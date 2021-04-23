using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{

    public float horizontalRotationLimit = 45f;
    public float mouseSensitivity = 100f;

    Quaternion rotation;
    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        rotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = rotation;

        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        yRotation += mouseX;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        rotation = transform.rotation;
    }
}
