using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFighter : MonoBehaviour {

    public void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Char")
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            Debug.Log(collider.gameObject.name);
            rb.AddForce(0, 300, 0);
        }
    }
}
