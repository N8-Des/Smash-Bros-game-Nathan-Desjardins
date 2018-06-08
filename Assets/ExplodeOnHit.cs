using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnHit : MonoBehaviour {
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "KillRad")
        {
            GameObject explode = GameObject.Instantiate((GameObject)Resources.Load("Explode"));
            explode.transform.position = transform.position;
            Invoke("kill", 0.01f);
        }
    }
    void kill()
    {
        Destroy(gameObject);
    }
}
