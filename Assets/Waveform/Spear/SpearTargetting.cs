using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTargetting : MonoBehaviour {
    public float speed;
    public Rigidbody rb;
    public CharacterMove friend;
    public Vector3 offset;
    public bool hasTarget = false;
    public Vector3 unhomeSpeed;
    public void OnTriggerStay(Collider other)
    {
        CharacterMove target = other.GetComponent<CharacterMove>();
        if (target != null && target != friend)
        {
            if (target.isWet)
            {
                hasTarget = true;
                /*Vector3 delta = other.transform.position - this.transform.position;
                //Vector3 newDir = Vector3.RotateTowards(transform.right, delta, step, 0.5f);
                //transform.rotation = Quaternion.LookRotation(delta);
                Vector3 rotateAmount = Vector3.Cross(transform.right, delta);
                delta.Normalize();
                rb.angularVelocity = rotateAmount * speed;
                rb.velocity = transform.right * speed;
                */
                Vector3 delta = other.transform.position - this.transform.position;
                delta += offset;
                rb.velocity = delta.normalized * speed;
            }
        }
    }
    public void onTriggerExit(Collider other)
    {
        CharacterMove target = other.GetComponent<CharacterMove>();
        if (target != null && target != friend)
        {
            hasTarget = false;
        }
    }
    public void Update()
    {
        if (!hasTarget)
        {
            rb.velocity = unhomeSpeed;
        }
    }
}
