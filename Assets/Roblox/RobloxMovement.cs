using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobloxMovement : CharacterMove
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
        me.velocity = new Vector3(0, 0, 0);
        canMove = true;
        canAttack = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("CanAttack", true);
        anim.SetBool("IsIdle", true);
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
    }

    public override void bUp()
    {
        rb.AddForce(0, 6000, 0);
        Invoke("deactivate", 0.3f);
    }
    public override void bDown()
    {
        //anim.ResetTrigger("BDown");
        Invoke("deactivate", 1);
    }
    public void shotBack()
    {
        if (!isRight)
        {
            //Debug.Log("left");
            rb.AddForce(0, 500, 50000);
            Invoke("deactivate", 0.4f);
        }
        else
        {
            //Debug.Log("right");
            rb.AddForce(0, 500, -50000);
            Invoke("deactivate", 0.4f);
        }
    }
    public void blast()
    {
        GameObject blast = GameObject.Instantiate((GameObject)Resources.Load("RobloxBlast"));
        PresentRB = blast.GetComponent<Rigidbody>();
        if (!isRight)
        {
            blast.transform.rotation = new Quaternion(0, 180, 0, 0);
            blast.transform.position = transform.position + new Vector3(0, 0.1f, -0.4f);
            PresentRB.velocity = new Vector3(0, 0, -3.2f);
            Invoke("baseStop", 1.1f);
        }
        else
        {
            blast.transform.position = transform.position + new Vector3(0, 0.1f, 0.4f);
            PresentRB.velocity = new Vector3(0, 0, 3.2f);
            Invoke("baseStop", 1.1f);
        }
    }

}