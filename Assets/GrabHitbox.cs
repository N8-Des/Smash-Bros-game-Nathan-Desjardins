using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHitbox : MonoBehaviour {
    CharacterMove player;
    public void OnTriggerEnter(Collider collider)
    {
        bool right = false;
        CharacterMove hitPlayer = collider.GetComponent<CharacterMove>();
        if (hitPlayer != null)
        {
            if (hitPlayer.gameObject != transform.parent.gameObject)
            {
                player = gameObject.transform.parent.GetComponent<CharacterMove>();
                right = player.isRight;
                player.anim.SetBool("GrabIdle", true);
                player.anim.ResetTrigger("Grab");
                player.grabbedPlayer = hitPlayer;
                player.isGrabbing = true;
                hitPlayer.grabbed(player, right);
            }
        }
    }

}
