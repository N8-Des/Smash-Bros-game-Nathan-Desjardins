using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHurtbox : MonoBehaviour {
    public Vector3 KB;
    public int damage;
    public bool alreadyNegative;
    public bool isRight = true;

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (gameObject.transform.parent.tag == "Char")
        {
            CharacterMove player = gameObject.transform.parent.GetComponent<CharacterMove>();
            if (!player.isRight)
            {
                if (!alreadyNegative)
                {
                    KB.z *= -1;
                }
                alreadyNegative = true;
            } else
            {
                KB.z = Mathf.Abs(KB.z);
                alreadyNegative = false;
            }
            GameObject other = collider.gameObject;
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            ThanosCar thanosCar = other.GetComponent<ThanosCar>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                dmgCtrl.TakeAttack(damage, KB);
            } else if (thanosCar != null)
            {
                thanosCar.Explode();
            }
        }
        else if (!isRight)
        {
            KB.z *= -1;
            GameObject other = collider.gameObject;
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                dmgCtrl.TakeAttack(damage, KB);
            }
        }else
        {
            GameObject other = collider.gameObject;
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                dmgCtrl.TakeAttack(damage, KB);
            }
        }
    }
}
