using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadToHitbox : UltHurtbox
{
    public BaseHit dmgCtrl;
    public WorstMovement worst;
    public GameObject YaKnow;
    public override void OnTriggerEnter(Collider collider)
    {
        if (gameObject.transform.parent.tag == "Char")
        {
            CharacterMove player = gameObject.transform.parent.GetComponent<CharacterMove>();
            if (player.isRight == false)
            {
                KB.z *= -1;
            }
            GameObject other = collider.gameObject;
            dmgCtrl = other.GetComponent<BaseHit>();
            CharacterMove playerHit = other.GetComponent<CharacterMove>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                playerHit.canAttack = false;
                playerHit.canMove = false;
                playerHit.canJump = false;
                YaKnow = GameObject.Instantiate((GameObject)Resources.Load("DoItToEm"));
                GameObject youknowanim = YaKnow.transform.GetChild(0).GetChild(0).gameObject;            
                YouKnow youKnow = youknowanim.GetComponentInChildren<YouKnow>();
                Debug.Log(youKnow);
                youKnow.PeterDinklage = worst;
                youKnow.playerHit = dmgCtrl;
                youKnow.hurtbox = this;
                YaKnow.transform.position = other.transform.position + new Vector3(0.6f, 0.4f, 0);
                worst.startUlt();
            }
        }
    }
    public void takeHit(BaseHit dmgCtrlReal)
    {
        dmgCtrlReal.takeUlt(damage, KB);
        worst.stopUlt();
        Destroy(YaKnow);
    }
}
