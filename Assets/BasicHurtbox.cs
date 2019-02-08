using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHurtbox : MonoBehaviour {
    public Vector3 KB;
    public int damage;
    public bool alreadyNegative;
    public bool isRight = true;
    public float AudioHitNumber = 1;
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
                dmgCtrl.TakeAttack(damage, KB, player);
                if (!dmgCtrl.isBlocking)
                {
                    GameObject AudioHit = GameObject.Instantiate((GameObject)Resources.Load("Audh" + AudioHitNumber));
                }
                else
                {
                    GameObject AudioShieldHit = GameObject.Instantiate((GameObject)Resources.Load("Audh7"));

                }
            }
            else if (thanosCar != null)
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
                dmgCtrl.TakeAttack(damage, KB, null);
                GameObject Audio = GameObject.Instantiate((GameObject)Resources.Load("Audh" + AudioHitNumber));
            }
        }
        else
        {
            GameObject other = collider.gameObject;
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                dmgCtrl.TakeAttack(damage, KB, null);
                GameObject Audio = GameObject.Instantiate((GameObject)Resources.Load("Audh" + AudioHitNumber));
            }
        }
    }
}
