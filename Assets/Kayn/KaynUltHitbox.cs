using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaynUltHitbox : UltHurtbox
{
    public BaseHit dmgCtrl;
    public KaynMovement kayn;
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
                kayn.playerHit = dmgCtrl;
                kayn.startUlt();
            }
        }
    }
}
