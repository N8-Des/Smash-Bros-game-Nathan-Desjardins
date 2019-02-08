using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HankMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource bwaaa;
    public ParticleSystem part;
    public GameObject propane;
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
            bwaaa.Play();
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = new Vector3(0, 0, 2.8f);
            //Invoke("deactivate", 0.4f);
        }
        else
        {
            bwaaa.Play();
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = new Vector3(0, 0, -2.8f);
            //Invoke("deactivate", 0.4f);
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
        rb.AddForce(0, 6700, 0);
        Invoke("deactivate", 0.3f);
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
    public void propaneThrow()
    {
        GameObject Present = GameObject.Instantiate((GameObject)Resources.Load("Propane Tank"));
        PresentRB = Present.GetComponent<Rigidbody>();
        if (!isRight)
        {
            Present.transform.rotation = new Quaternion(0, 180, 0, 0);
            Present.transform.position = transform.position + new Vector3(0, 0.3f, -0.4f);
            PresentRB.velocity = new Vector3(0, 1.7f, -2.3f);
            //Invoke("baseStop", 0.6f);
        }
        else
        {
            Present.transform.position = transform.position + new Vector3(0, 0.3f, 0.4f);
            PresentRB.velocity = new Vector3(0, 1.7f, 2.3f);
            //Invoke("baseStop", 0.6f)
        }
        //anim.ResetTrigger("NeutB");
    }
    public void stopUltimate()
    {
        isShooting = false;
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
        damageControl.isInvuln = false;
        Destroy(propane);
    }
    public void Ultimate()
    {
        damageControl.isInvuln = true;
        propane = GameObject.Instantiate((GameObject)Resources.Load("PropaneNightmare"));
        Rigidbody propRB = propane.GetComponent<Rigidbody>();
        if (!isRight)
        {
            propane.transform.rotation = new Quaternion(0, 180, 0, 0);
            propane.transform.position = transform.position + new Vector3(0, 0.2f, 1.3f);
            propRB.velocity = new Vector3(0, 0, -3.5f);
        }
        else
        {
            propane.transform.position = transform.position + new Vector3(0, 0.2f, -1.3f);
            propRB.velocity = new Vector3(0, 0, 3.5f);
        }
    }
}