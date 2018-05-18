﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraumBase : BaseCharMove
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
        }

        if (isIdle == true)
        {
            anim.SetBool("isWalking", false);
        }
    }
    public override void BaseA()
    {
        canMove = true;
        canAttack = true;
        canJump = true;
    }
    public override void SideA()
    {
        canMove = true;
        canAttack = true;
        canJump = true;
    }
    public override void UpA()
    {
        canMove = true;
        canAttack = true;
    }

    public override void jump()
    {
        anim.ResetTrigger("Jump");
        rb.velocity = new Vector3(0, 6, 0);
        canMove = true;
        Invoke("stopJump", 0.2f);
    }
    public void stopJump()
    {
        rb.velocity = new Vector3(0, 0, 0);
        anim.ResetTrigger("Jump");
    }
    public override void DownA()
    {
        canMove = true;
        canAttack = true;
        canJump = true;
    }
    public override void bAir()
    {
        fair();
    }
    public override void fair()
    {
        anim.ResetTrigger("Fair");
        canMove = true;
        canAttack = true;
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
    public override void uAir()
    {
        canMove = true;
        canAttack = true;
    }
    public override void dair()
    {
        anim.ResetTrigger("Fair");
        canAttack = true;
        canMove = true;
    }
    public override void nair()
    {
        canAttack = true;
        canMove = true;
    }
    public override void bRight()
    {
        me = gameObject.GetComponent<Rigidbody>();
        iCanMove = true;
        me.velocity = moveSpeed * 5;
        Invoke("deactivate", 0.26f);
    }
    public override void bLeft()
    {
        me = gameObject.GetComponent<Rigidbody>();
        iCanMove = true;
        me.velocity = moveSpeed * -5;
        Invoke("deactivate", 0.26f);

    }
    public void baseStop()
    {
        canAttack = true;
        canMove = true;
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
    public void present() {
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