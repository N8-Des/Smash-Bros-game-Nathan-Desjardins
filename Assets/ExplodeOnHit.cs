using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnHit : MonoBehaviour {
    public bool isRight = true;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Char" || other.tag == "Ground")
        {
            GameObject explode = GameObject.Instantiate((GameObject)Resources.Load("Explode"));
            explode.transform.position = transform.position;
            if (!isRight)
            {
                explode.GetComponentInChildren<BasicHurtbox>().KB.z *= -1;
            }
            kill();
        }
    }
    void kill()
    {
        GameObject dad = gameObject.transform.parent.gameObject;
        Destroy(dad);
    }
}
