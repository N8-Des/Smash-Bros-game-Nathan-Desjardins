using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propane : MonoBehaviour {
    public void OnTriggerEnter(Collider other)
    {
        GameObject explosion = Instantiate((GameObject)Resources.Load("explode"));
        explosion.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }
}
