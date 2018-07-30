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
                dmgCtrl.TakeAttack(damage, KB);
            }
        }
    }
}
