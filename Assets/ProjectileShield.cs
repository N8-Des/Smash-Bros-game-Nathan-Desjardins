using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShield : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile") {
            Destroy(other);
        }
    }
}   
