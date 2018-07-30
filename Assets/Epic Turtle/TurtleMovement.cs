using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public GameObject counterObject;
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
    public override void jump()
    {
        anim.ResetTrigger("Jump");
        rb.AddForce(0, 7500, 0);
        canMove = true;
        Invoke("stopJump", 0.2f);
    }
    public void stopJump()
    {
        isJumping = false;
        iCanMove = false;
        //rb.velocity = new Vector3(0, 0, 0);
        anim.ResetTrigger("Jump");
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
        anim.SetBool("UltHit", false);
    }

    public override void bUp()
    {
        rb.AddForce(0, 20000, 0);
        Invoke("deactivate", 0.12f);
    }
    public override void baseB()
    {
        Invoke("baseStop", 3);
    }
    public override void counter(int damage)
    {
        anim.SetTrigger("counter");
        BasicHurtbox countHB = counterObject.GetComponent<BasicHurtbox>();
        countHB.damage = damage;
    }
    public void countering()
    {
        isCountering = true;
    }
    public void notCountering()
    {
        isCountering = false;
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

}