using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitzMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
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
    public void startRunning()
    {
        me = gameObject.GetComponent<Rigidbody>();
        iCanMove = true;
        anim.SetTrigger("SBRun");
        me.useGravity = false;
        if (!isRight)
        {
            me.velocity = moveSpeed * -1.4f;
            Invoke("stopRunning", 0.78f);
        }
        else
        {
            me.velocity = moveSpeed * 1.4f;
            Invoke("stopRunning", 0.78f);
        }
    }
    public void stopRunning()
    {
        rb.useGravity = true;
        me.velocity = Vector3.zero;
        if (!isRight)
        {
            me.AddForce(0, 0, -2000);
            anim.SetTrigger("SlideStop");
        }
        else
        {
            me.AddForce(0, 0, 2000);
            anim.SetTrigger("SlideStop");
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
    public void stopGrav()
    {
        rb.useGravity = false;
    }
    public override void bUp()
    {
        deactivate();
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
        transform.position += new Vector3(0, 2, 0);
    }
    public void Dair()
    {
        me.AddForce(0, -10000, 0);
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
    public void ThrowGrenade()
    {
        GameObject Grenade = GameObject.Instantiate((GameObject)Resources.Load("BlitzGrenade"));
        PresentRB = Grenade.GetComponent<Rigidbody>();
        //audioStrike.Play();
        if (!isRight)
        {
            Grenade.transform.rotation = new Quaternion(0, 180, 0, 0);
            Grenade.transform.position = transform.position + new Vector3(0, 0.3f, -0.5f);
            PresentRB.velocity = new Vector3(0, 2, -2f);
            //Invoke("baseStop", 0.6f);
        }
        else
        {
            Grenade.transform.position = transform.position + new Vector3(0, 0.3f, 0.5f);
            PresentRB.velocity = new Vector3(0, 2, 2f);
            //Invoke("baseStop", 0.6f);
        }
    }

}