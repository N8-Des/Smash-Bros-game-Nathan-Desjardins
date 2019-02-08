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
            CharacterMove player = friend.GetComponent<CharacterMove>();
            if (player.isRight)
            {
                if (!alreadyNegative)
                {
                    KB.z *= -1;
                }
                alreadyNegative = true;
            }
            else
            {
                KB.z = Mathf.Abs(KB.z);
                alreadyNegative = false;
            }
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                if (player.isRight == false)
                {                  
                    dmgCtrl.TakeAttack(damage, KB, null);
                }
                else
                {
                    dmgCtrl.TakeAttack(damage, KB, null);
                }
                GameObject Audio = GameObject.Instantiate((GameObject)Resources.Load("Audh" + AudioHitNumber));
            }
        }
    }
}
