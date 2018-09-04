using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {
    public void OnCollisionEnter(Collision collider)
    {
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(0, 0, -2000);
    }
}
