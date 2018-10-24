using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDamage : MonoBehaviour {
    public Vector3 KB;
    public int damage;
    public void OnTriggerEnter(Collider collider)
    {
        if (gameObject.transform.parent.transform.rotation.eulerAngles.y == 180)
        {
            KB.z *= -1;
            GameObject other = collider.gameObject;
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                dmgCtrl.TakeAttack(damage, KB, null);
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
        else
        {
            GameObject other = collider.gameObject;
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                dmgCtrl.TakeAttack(damage, KB, null);
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
} 
