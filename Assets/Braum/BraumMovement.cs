using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraumMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
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
    public override void jump()
    {
        anim.ResetTrigger("Jump");
        rb.AddForce(0, 10000, 0);
        canMove = true;
        Invoke("stopJump", 0.2f);
    }
    public void stopJump()
    {
        isJumping = false;
        iCanMove = false;
        rb.velocity = new Vector3(0, 0, 0);
        anim.ResetTrigger("Jump");
    }
    public void deactivate()
    {
        iCanMove = false;
        me.velocity = new Vector3(0, 0, 0);
        anim.SetTrigger("DoneBSide");
        anim.SetTrigger("DoneBDown");
        canMove = true;
        canAttack = true;
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
    public void baseStop()
    {
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        if (onGround)
        {
            anim.SetBool("IsIdle", true);
        }
    }

    public override void bUp()
    {
        rb.velocity = new Vector3(0, 6, 0);
        Invoke("StopEverything", 0.3f);
    }
    public void StopEverything()
    {
        anim.SetBool("BUp", false);
        canAttack = true;
        canMove = true;
        iCanMove = false;
        //rb.velocity = new Vector3(0, 0, 0);
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
    public void present()
    {
        GameObject Present = GameObject.Instantiate((GameObject)Resources.Load("BraumPresent"));
        PresentRB = Present.GetComponent<Rigidbody>();
        audioStrike.Play();
        if (!isRight)
        {
            Present.transform.rotation = new Quaternion(0, 180, 0, 0);
            Present.transform.position = transform.position + new Vector3(0, 0.3f, -1);
            PresentRB.velocity = new Vector3(0, 0, -4);
            Invoke("baseStop", 0.6f);
        }
        else
        {
            Present.transform.position = transform.position + new Vector3(0, 0.3f, 1);
            PresentRB.velocity = new Vector3(0, 0, 4);
            Invoke("baseStop", 0.6f);
        }
        //anim.ResetTrigger("NeutB");
    }

}