using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnHit : MonoBehaviour {

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
