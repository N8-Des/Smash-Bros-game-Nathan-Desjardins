using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillyUltHitbox : UltHurtbox
{
    public BaseHit dmgCtrl;
    public AudioSource hitNoise;
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
                StartCoroutine(takeHit(dmgCtrl));
                GameObject Particle = GameObject.Instantiate((GameObject)Resources.Load("BillyExplosion"));
                Particle.transform.position = other.transform.position;
                hitNoise.Play();
            }
        }
    }
    public IEnumerator takeHit(BaseHit control)
    {
        yield return new WaitForSeconds(1f);
        control.takeUlt(damage, KB);

    }
}
