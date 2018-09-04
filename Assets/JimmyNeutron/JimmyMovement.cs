using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    public Vector3 normalGrav;
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
            GameObject donkey = GameObject.Instantiate((GameObject)Resources.Load("JimmyRocket"));
            donkey.transform.position = this.transform.position - new Vector3(0, -0.2f, -0.6f);
            Rigidbody donkeyrb = donkey.GetComponent<Rigidbody>();
            donkeyrb = donkey.GetComponent<Rigidbody>();
            donkeyrb.velocity = new Vector3(0, 0, 6);
        }
        else
        {
            GameObject donkey = GameObject.Instantiate((GameObject)Resources.Load("JimmyRocket"));
            donkey.transform.position = this.transform.position - new Vector3(0, -0.2f, 0.6f);
            donkey.transform.rotation = new Quaternion(0, 180, 0, 0);
            Rigidbody donkeyrb = donkey.GetComponent<Rigidbody>();
            donkeyrb.velocity = new Vector3(0, 0, -6);
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
    public override void baseB()
    {
        Invoke("baseStop", 3);
    }
    public override void bDown()
    {
        //anim.ResetTrigger("BDown");
        Invoke("deactivate", 1);
    }
    public void ShootBilly()
    {
        if (!isShooting)
        {
            isShooting = true;
            GameObject Present = GameObject.Instantiate((GameObject)Resources.Load("BillyBullet"));
            PresentRB = Present.GetComponent<Rigidbody>();
            //audioStrike.Play();
            if (!isRight)
            {
                Present.transform.rotation = new Quaternion(0, 180, 0, 0);
                Present.transform.position = transform.position + new Vector3(0, 0.3f, -0.4f);
                PresentRB.velocity = new Vector3(0, 0, -5f);
                //Invoke("baseStop", 0.6f);
            }
            else
            {
                Present.transform.position = transform.position + new Vector3(0, 0.3f, 0.4f);
                PresentRB.velocity = new Vector3(0, 0, 5f);
                //Invoke("baseStop", 0.6f);
            }
            //anim.ResetTrigger("NeutB");
        }
    }
    public void Ultimate()
    {
        normalGrav = Physics.gravity;
        rb.useGravity = false;
        Physics.gravity *= -2;
    }
    public void Ultimate2()
    {
        Physics.gravity *= -7;
    }
    public void StopUlt()
    {
        isShooting = false;
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
        Physics.gravity = normalGrav;
        rb.useGravity = true;
    }
}