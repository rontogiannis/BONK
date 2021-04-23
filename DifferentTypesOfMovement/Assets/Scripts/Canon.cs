using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject lookTrasnform;
    public GameObject bullet;
    public Transform tip;


    public float ForceMagn = 2000f;
    public float secondsToDestroy = 5f;

    void Update()
    {
        transform.LookAt(lookTrasnform.transform);

        if (Input.GetKeyDown("z"))
        {
            GameObject bulletInstance = GameObject.Instantiate(bullet, tip.position, tip.rotation);
            Destroy(bulletInstance, secondsToDestroy);
            Rigidbody rg = bulletInstance.GetComponent<Rigidbody>();
            rg.AddForce(tip.forward * ForceMagn * rg.mass);
        }
    }
}
