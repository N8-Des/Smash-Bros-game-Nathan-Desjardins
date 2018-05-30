using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbreaker : MonoBehaviour {
    public Rigidbody rb;
    public void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        rb = other.GetComponent<Rigidbody>();
        BaseHit dmgCtrl = other.GetComponent<BaseHit>();
        if (other.tag == ("Projectile"))
        {
            rb.velocity *= -1;
        }
    }
}
