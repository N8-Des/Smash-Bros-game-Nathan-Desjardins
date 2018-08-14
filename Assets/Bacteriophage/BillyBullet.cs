using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillyBullet : MonoBehaviour {
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
