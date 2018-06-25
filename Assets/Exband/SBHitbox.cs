using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBHitbox : MonoBehaviour
{
    public Vector3 KB;
    public int damage;
    public CharacterMove thisChar;

    public void OnTriggerEnter(Collider collider)
    {
        if (gameObject.transform.parent.tag == "Char")
        {
            CharacterMove player = gameObject.transform.parent.GetComponent<CharacterMove>();
            GameObject other = collider.gameObject;
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                dmgCtrl.TakeAttack(damage, KB);
                thisChar.bSideGrab(other);
                player.canAttack = false;
                player.canMove = false;
            }
        }
    }
}
