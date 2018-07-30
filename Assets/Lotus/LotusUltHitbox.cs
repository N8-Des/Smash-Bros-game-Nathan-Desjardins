using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusUltHitbox : UltHurtbox {
    public GameObject Lotus;
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Char" && other.gameObject != Lotus)
        {
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                dmgCtrl.takeUlt(damage, KB);
            }
        }
    }
}
