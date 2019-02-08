using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeStop : MonoBehaviour {
    void OnTriggerEnter(Collider collider)
    {
        CharacterMove hitPlayer = collider.GetComponent<CharacterMove>();
        WaluigiMovement waluigi = collider.GetComponent<WaluigiMovement>();
        if (hitPlayer != null)
        {
            if (hitPlayer.onGround == true)
            {
                hitPlayer.endVelocityEdge();
            }
        }
        if (waluigi != null)
        {
            if (waluigi.anim.GetCurrentAnimatorStateInfo(0).IsName("SideB"))
            {
                waluigi.endVelocityEdge();
            }
        }
    }
}
