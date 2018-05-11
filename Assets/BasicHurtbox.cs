using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHurtbox : MonoBehaviour {
    public Vector3 KB;
    public int damage;

    public void OnTriggerEnter(Collider collider)
    {
        if (gameObject.transform.parent.gameObject.transform.eulerAngles.y >= 1) //so hitboxes can work left and right.
        {
            KB.z *= -1;
        }
        GameObject other = collider.gameObject;
        BaseHit dmgCtrl = other.GetComponent<BaseHit>();
        if (dmgCtrl != null && dmgCtrl.transform.parent != this)
        {
            dmgCtrl.TakeAttack(damage, KB);
        }
    }
}
