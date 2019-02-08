using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongebobMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody pattyrb;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
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
        me.velocity = new Vector3(0, 0, 0);
        canMove = true;
        canAttack = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("CanAttack", true);
        anim.SetBool("IsIdle", true);
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
    }

    public override void bUp()
    {
        rb.AddForce(0, 6000, 0);
        Invoke("deactivate", 0.3f);
    }

    public void KrabbyThrow()
    {
        GameObject Patty = GameObject.Instantiate((GameObject)Resources.Load("KrabbyPatty"));
        pattyrb = Patty.GetComponent<Rigidbody>();
        //audioStrike.Play();
        if (!isRight)
        {
            BasicHurtbox pattyHitbox = Patty.GetComponentInChildren<BasicHurtbox>();
            pattyHitbox.KB.z *= -1;
            Patty.transform.rotation = new Quaternion(0, 180, 0, 0);
            Patty.transform.position = transform.position + new Vector3(0, 0.3f, -0.3f);
            pattyrb.velocity = new Vector3(0, 0, -4);
            //Invoke("baseStop", 0.4f);
        }
        else
        {
            Patty.transform.position = transform.position + new Vector3(0, 0.3f, 0.3f);
            pattyrb.velocity = new Vector3(0, 0, 4);
            //Invoke("baseStop", 0.4f);
        }
    }
    public void Jellyfish()
    {
        GameObject Jelly = GameObject.Instantiate((GameObject)Resources.Load("Jellyfish"));
        pattyrb = Jelly.GetComponent<Rigidbody>();
        //audioStrike.Play();
        if (!isRight)
        {
            Jelly.transform.rotation = new Quaternion(0, 180, 0, 0);
            Jelly.transform.position = transform.position + new Vector3(0, 0.3f, -0.3f);
            pattyrb.velocity = new Vector3(0, 0, -0.6f);
            //Invoke("baseStop", 0.2f);
        }
        else
        {
            Jelly.transform.position = transform.position + new Vector3(0, 0.3f, 0.3f);
            pattyrb.velocity = new Vector3(0, 0, 0.6f);
            //Invoke("baseStop", 0.2f);
        }
    }

}