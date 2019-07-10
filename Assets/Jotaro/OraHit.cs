using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OraHit : MonoBehaviour
{
    public BaseHit dmgCtrl;
    public JotaroMovement playerControl;
    public void OnTriggerEnter(Collider collider)
    {
        if (gameObject.transform.parent.tag == "Char")
        {
            CharacterMove player = gameObject.transform.parent.GetComponent<CharacterMove>();
            GameObject other = collider.gameObject;
            dmgCtrl = other.GetComponent<BaseHit>();
            CharacterMove playerHit = other.GetComponent<CharacterMove>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                playerControl.anim.SetTrigger("Ora");
                //adSteal.SetActive(true);
            }
        }
    }
}
