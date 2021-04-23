using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonker : MonoBehaviour
{
    public float torque = 100f;

    Rigidbody rb;
    ConfigurableJoint cj;

    bool isAttacking;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cj = GetComponent<ConfigurableJoint>();

        isAttacking = false;

        Debug.Log(cj.targetRotation);
    }

    IEnumerator Attack(float seconds)
    {
        Debug.Log("In Coroutine");
        cj.targetRotation = new Quaternion(0f, 0f, -1f, 1f);
        isAttacking = true;
        yield return new WaitForSeconds(seconds);
        cj.targetRotation = new Quaternion(0f, 0f, 0f, 1f);
        yield return new WaitForSeconds(seconds);
        isAttacking = false;
    }

    void FixedUpdate()
    {
        if(Input.GetKey("q") && !isAttacking)
        {
            StartCoroutine(Attack(0.5f));
        }
    }
}
