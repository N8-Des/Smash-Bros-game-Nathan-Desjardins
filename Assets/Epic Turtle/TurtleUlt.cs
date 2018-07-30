using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleUlt : UltHurtbox
{
    public BaseHit dmgCtrl;
    public AudioSource hitNoise;
    public Animator anim;
    public void Start()
    {
        anim = gameObject.transform.parent.GetComponent<Animator>();
    }

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
                playerHit.StopEveryThing();
                anim.SetBool("UltHit", true);
                //hitNoise.Play();
            }
        }
    }
}
