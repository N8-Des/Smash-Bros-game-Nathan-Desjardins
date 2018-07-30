using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHurtbox : MonoBehaviour {
    public Vector3 KB;
    public int damage;

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (gameObject.transform.parent.tag == "Char")
        {
            CharacterMove player = gameObject.transform.parent.GetComponent<CharacterMove>();
            if (player.isRight == false)
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
        else if (gameObject.transform.parent.transform.rotation.eulerAngles.y == 180)
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
