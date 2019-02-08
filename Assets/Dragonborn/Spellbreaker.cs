using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbreaker : MonoBehaviour {
    public Rigidbody rb;
    public void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        rb = other.transform.parent.GetComponent<Rigidbody>();
        //BaseHit dmgCtrl = other.GetComponent<BaseHit>();
        if (other.tag == ("Projectile") && rb != null)
        {
            rb.velocity *= -1;
        }
    }
}
