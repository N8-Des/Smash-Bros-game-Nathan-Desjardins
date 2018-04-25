using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainBlast : MonoBehaviour {
    public Rigidbody rb;
    public void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        rb = other.GetComponent<Rigidbody>();
        BaseHit dmgCtrl = other.GetComponent<BaseHit>();
        if (dmgCtrl != null && dmgCtrl.transform.parent != this)
        {
            rb.velocity *= -1;
        }
    }
}
