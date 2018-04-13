using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHurtbox : MonoBehaviour {
    public Vector3 KB;
    public int damage;

    public void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        BaseHit dmgCtrl = other.GetComponent<BaseHit>();
        if (dmgCtrl != null && dmgCtrl.transform.parent != this)
        {
            dmgCtrl.TakeAttack(damage, KB);
        }
    }
}
