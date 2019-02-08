using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigdugMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public GameObject hitPlayer;
    public BaseHit damageControlHit;
    public bool isShooting = false;
    int numFrames;
    void Update()
    {
        if (moveRight == true || moveLeft == true)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("IsIdle", false);
        }
        if (isIdle == true)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("IsIdle", true);
        }
    }
    public override void attacking()
    {
        if (!anim.GetBool("isAttacking"))
        {
            anim.SetBool("CanAttack", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("isAttacking", true);
        }
    }
    public void UpBStart()
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }
    public void deactivate()
    {
        rb.useGravity = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("CanAttack", true);
        anim.SetBool("IsIdle", true);
        iCanMove = false;
        me.velocity = new Vector3(0, 0, 0);
        canMove = true;
        canAttack = true;
        anim.SetBool("Jumping", false);
    }
    public override void bRight()
    {
        me = gameObject.GetComponent<Rigidbody>();
        iCanMove = true;
        me.velocity = moveSpeed * 2;
        Invoke("deactivate", 0.26f);
    }
    public override void bLeft()
    {
        me = gameObject.GetComponent<Rigidbody>();
        iCanMove = true;
        me.velocity = moveSpeed * -2;
        Invoke("deactivate", 0.26f);
    }
    public void NB2()
    {
        anim.ResetTrigger("NeutB");
        anim.SetTrigger("NB2");
    }
    public void increasePlayerSize()
    {
        hitPlayer.transform.localScale *= 1.2f;
        GameObject BlowSound = GameObject.Instantiate((GameObject)Resources.Load("DigdugBlowUp"));
    }
    public void launchHitPlayer()
    {
        hitPlayer.transform.localScale /= 1.6f;
        if (isRight)
        {
            damageControlHit.TakeAttack(16, new Vector3(0, 0.85f, 0.75f), null);
        }
        else
        {
            damageControlHit.TakeAttack(16, new Vector3(0, 0.85f, -0.75f), null);
        }
        GameObject SendSound = GameObject.Instantiate((GameObject)Resources.Load("DigdugSendOff"));

    }
    public void bSide()
    {
        if (isRight)
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * 2.4f;
            //Invoke("deactivate", 0.56f);
        }
        else
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * -2.4f;
            //Invoke("deactivate", 0.56f);
        }

    }
    public void baseStop()
    {
        isShooting = false;
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
        anim.SetBool("Jumping", false);
    }

    public override void bUp()
    {
        StartCoroutine(upB());
        stopVelocity();
    }
    IEnumerator upB()
    {
        while (numFrames <= 50)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 3, 0), Time.deltaTime * 2);
            numFrames += 1;
            yield return new WaitForEndOfFrame();
        }
        numFrames = 0;
    }
    public override void baseB()
    {
        Invoke("baseStop", 3);
    }
    public void Dair()
    {
        me.AddForce(0, -10000, 0);
    }
}