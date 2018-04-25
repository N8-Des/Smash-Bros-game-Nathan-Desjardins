using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeBase : BaseCharMove
{
    public Rigidbody me;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public Rigidbody pattyrb;
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
        //anim.ResetTrigger("Jump");
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
        anim.ResetTrigger("Dair");
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
        anim.ResetTrigger("BSide");
        baseStop();
    }
    public override void bLeft()
    {
        anim.ResetTrigger("BSide");
        baseStop();
    }
    public void baseStop()
    {
        canAttack = true;
        canMove = true;
    }

    public override void bUp()
    {
        rb.velocity = new Vector3(0, 3, 0);
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
        GameObject Patty = GameObject.Instantiate((GameObject)Resources.Load("KrabbyPatty"));
        pattyrb = Patty.GetComponent<Rigidbody>();
        //audioStrike.Play();
        if (transform.eulerAngles.y >= 180)
        {
            Patty.transform.rotation = new Quaternion(0, 180, 0, 0);
            Patty.transform.position = transform.position + new Vector3(0, 0.3f, -0.3f);
            pattyrb.velocity = new Vector3(0, 0, -4);
            Invoke("baseStop", 0.4f);
        }
        else
        {
            Patty.transform.position = transform.position + new Vector3(0, 0.3f, 0.3f);
            pattyrb.velocity = new Vector3(0, 0, 4);
            Invoke("baseStop", 0.4f);
        }
    }
        void endInvisible()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
    }
    public override void bDown()
    {
        GameObject Jelly = GameObject.Instantiate((GameObject)Resources.Load("Jellyfish"));
        pattyrb = Jelly.GetComponent<Rigidbody>();
        //audioStrike.Play();
        if (transform.eulerAngles.y >= 180)
        {
            Jelly.transform.rotation = new Quaternion(0, 180, 0, 0);
            Jelly.transform.position = transform.position + new Vector3(0, 0.3f, -0.3f);
            pattyrb.velocity = new Vector3(0, 0, -4);
            Invoke("baseStop", 0.2f);
        }
        else
        {
            Jelly.transform.position = transform.position + new Vector3(0, 0.3f, 0.3f);
            pattyrb.velocity = new Vector3(0, 0, 4);
            Invoke("baseStop", 0.2f);
        }
    }
}