using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GilmanMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PaperRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    public GameObject paper;
    public GameObject counterObject;
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

    public void deactivate()
    {
        iCanMove = false;
        rb.useGravity = true;
        me.velocity = new Vector3(0, 0, 0);
        canMove = true;
        canAttack = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("CanAttack", true);
        anim.SetBool("IsIdle", true);
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
        transform.position += new Vector3(0, 1, 0);
    }
    public override void baseB()
    {
        Invoke("baseStop", 3);
    }
    public override void bDown()
    {
        //anim.ResetTrigger("BDown");
        Invoke("deactivate", 1);
    }
    public override void counter(int damage)
    {

        anim.SetTrigger("counter");
        BasicHurtbox countHB = counterObject.GetComponent<BasicHurtbox>();
        countHB.damage = damage;
        damageControl.isInvuln = false;
    }
    public void countering()
    {
        isCountering = true;
    }
    public void notCountering()
    {
        isCountering = false;
    }
    public void ThrowPaper()
    {
            GameObject PaperProj = Instantiate(paper);
            PaperRB = PaperProj.GetComponent<Rigidbody>();
            //audioStrike.Play();
            if (!isRight)
            {
                PaperProj.transform.rotation = new Quaternion(0, 180, 0, 0);
                PaperProj.transform.position = transform.position + new Vector3(0, 0.7f, -0.6f);
                PaperRB.velocity = new Vector3(0, 0, -5f);
                //Invoke("baseStop", 0.6f);
            }
            else
            {
                PaperProj.transform.position = transform.position + new Vector3(0, 0.7f, 0.6f);
                PaperRB.velocity = new Vector3(0, 0, 5f);
                //Invoke("baseStop", 0.6f);
            }
            //anim.ResetTrigger("NeutB");
    }

}