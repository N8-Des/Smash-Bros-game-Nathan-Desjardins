using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexRocket : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Char" || other.tag == "Ground")
        {
            GameObject explode = GameObject.Instantiate((GameObject)Resources.Load("VortexExplode"));
            explode.transform.position = transform.position;
            kill();
        }
    }
    void kill()
    {
        GameObject dad = gameObject.transform.parent.gameObject;
        Destroy(dad);
    }
}
