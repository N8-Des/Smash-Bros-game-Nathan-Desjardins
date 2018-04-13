﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyBase : BaseCharMove
{
    public Rigidbody donkeyrb;
    public AudioSource audioDonkey;
    public ParticleSystem part;
    public GameObject JimmyHand;
    public GameObject Jetpack;
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
        GameObject goddard = GameObject.Instantiate((GameObject)Resources.Load("Goddard"));
        goddard.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
        Invoke("dogDelay", 0.7f);
    }
    public void dogDelay()
    {
        canMove = true;
        canAttack = true;
        canJump = true;
    }
    public override void jump()
    {
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
        JimmyHand.SetActive(true);
        Invoke("deactivate", 0.4f);
    }
    public void deactivate()
    {
        JimmyHand.SetActive(false);
        canMove = true;
        canAttack = true;
    }
    public override void uAir()
    {
        JimmyHand.SetActive(true);
        Invoke("deactivate", 0.3f);
    }
    public override void dair()
    {
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
        GameObject donkey = GameObject.Instantiate((GameObject)Resources.Load("JimmyRocket"));
        donkey.transform.position = this.transform.position - new Vector3(0, 0, -0.4f);
        donkeyrb = donkey.GetComponent<Rigidbody>();
        donkeyrb.velocity = new Vector3(0, 0, 6);
        //audioDonkey.Play();
        Invoke("deactivate", 0.3f);

    }
    public override void bLeft()
    {
        GameObject donkey = GameObject.Instantiate((GameObject)Resources.Load("JimmyRocket"));
        donkey.transform.position = this.transform.position - new Vector3(0, 0, 0.4f);
        donkey.transform.rotation = new Quaternion(0, 180, 0, 0);
        donkeyrb = donkey.GetComponent<Rigidbody>();
        donkeyrb.velocity = new Vector3(0, 0, -6);
        //audioDonkey.Play();
        Invoke("deactivate", 0.3f);

    }
    public override void bUp()
    {
        rb.velocity = new Vector3(0, 6, 0);
        Jetpack.SetActive(true);
        Invoke("StopEverything", 0.5f);
    }
    public void StopEverything()
    {
        Jetpack.SetActive(false);
        anim.SetBool("BUp", false);
        iCanMove = true;
        canAttack = true;
        canMove = true;
    }
    public override void baseB()
    {
        anim.ResetTrigger("NeutB");
        canAttack = true;
        canMove = true;
    }
    public override void bDown()
    {
        part.Play();
        //anim.ResetTrigger("BDown");
        Invoke("deactivate", 1f);
    }
}