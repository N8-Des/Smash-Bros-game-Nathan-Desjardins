using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyHitbox : BasicHurtbox {
    public GameObject friend;
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Char" && other.gameObject != friend)
        {
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();

            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                if (!isRight)
                {
                    KB.z *= -1;
                    dmgCtrl.TakeAttack(damage, KB, null);
                }
                else
                {
                    dmgCtrl.TakeAttack(damage, KB, null);
                }
            }
        }
    }
}
