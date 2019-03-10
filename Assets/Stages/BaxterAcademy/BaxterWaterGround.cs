using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaxterWaterGround : MonoBehaviour {
    public bool isFlood;
    public void OnCollisionStay(Collision collider)
    {
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        CharacterMove charHit = collider.gameObject.GetComponent<CharacterMove>();
        if (rb != null && charHit != null)
        {
            if (isFlood)
            {
                rb.AddForce(0, -2000, 0);
            }
        }
    }
}
