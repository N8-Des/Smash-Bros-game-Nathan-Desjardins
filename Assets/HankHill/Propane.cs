using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propane : MonoBehaviour {
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Char" || other.tag == "Ground" )
        {
            GameObject explosion = Instantiate((GameObject)Resources.Load("explode"));
            explosion.transform.position = gameObject.transform.position + new Vector3 (0, 0.3f, 0);
            Destroy(gameObject);
        }
    }
}
