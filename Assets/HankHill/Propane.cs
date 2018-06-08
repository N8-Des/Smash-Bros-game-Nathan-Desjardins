using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propane : MonoBehaviour {
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "KillRad")
        {
            GameObject explosion = Instantiate((GameObject)Resources.Load("explode"));
            explosion.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }
}
