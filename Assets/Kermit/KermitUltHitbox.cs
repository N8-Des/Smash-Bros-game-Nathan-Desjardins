using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KermitUltHitbox : UltHurtbox
{
    public BaseHit dmgCtrl;
    public KermitMovement kerm;
    public override void OnTriggerEnter(Collider collider)
    {
        if (gameObject.transform.parent.tag == "Char")
        {
            CharacterMove player = gameObject.transform.parent.GetComponent<CharacterMove>();
            if (player.isRight == false)
            {
                KB.z *= -1;
            }
            GameObject other = collider.gameObject;
            dmgCtrl = other.GetComponent<BaseHit>();
            CharacterMove playerHit = other.GetComponent<CharacterMove>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                playerHit.canAttack = false;
                playerHit.canMove = false;
                playerHit.canJump = false;
                kerm.playerHit = dmgCtrl;
                kerm.startUlt();
            }
        }
    }
}
