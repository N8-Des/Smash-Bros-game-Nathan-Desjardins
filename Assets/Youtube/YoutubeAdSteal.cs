using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoutubeAdSteal : MonoBehaviour
{
    public BaseHit dmgCtrl;
    public GameObject adSteal;
    public YoutubeMovement playerControl;
    public void OnTriggerEnter(Collider collider)
    {
        if (gameObject.transform.parent.tag == "Char")
        {
            CharacterMove player = gameObject.transform.parent.GetComponent<CharacterMove>();
            GameObject other = collider.gameObject;
            dmgCtrl = other.GetComponent<BaseHit>();
            CharacterMove playerHit = other.GetComponent<CharacterMove>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                playerHit.canAttack = false;
                playerHit.canMove = false;
                playerHit.canJump = false;
                playerHit.StopEveryThing();
                playerHit.SendToIdle();
                playerControl.DownB2(dmgCtrl);
                //adSteal.SetActive(true);

            }
        }
    }
}
