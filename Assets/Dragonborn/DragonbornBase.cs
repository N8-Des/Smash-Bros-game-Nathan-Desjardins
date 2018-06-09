using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonbornBase : BaseCharMove
{
    public Rigidbody me;
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
        anim.SetBool("BUp", false);
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
        Invoke("baseStop", 2.3f);
    }
    public override void bLeft()
    {
        Invoke("baseStop", 2.3f);
    }
    public void baseStop()
    {
        canAttack = true;
        canMove = true;
    }

    public override void bUp()
    {
        Invoke("baseStop", 2f);
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
        Invoke("baseStop", 2.1f);
    }
    void endInvisible()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
    }
    public void shootArrow()
    {
        GameObject Arrow = GameObject.Instantiate((GameObject)Resources.Load("EbonyArrow"));
        Rigidbody arr = Arrow.GetComponent<Rigidbody>();
        //audioStrike.Play();
        if (!isRight)
        {
            Arrow.transform.rotation = new Quaternion(77, 218, 19, 0);
            Arrow.transform.position = transform.position + new Vector3(0, 0.7f, -0.5f);
            arr.velocity = new Vector3(0, 0, -8);
            Invoke("baseStop", 0.4f);
        }
        else
        {
            Arrow.transform.position = transform.position + new Vector3(0, 0.7f, 0.5f);
            arr.velocity = new Vector3(0, 0, 8);
            Invoke("baseStop", 0.4f);
        }
        //anim.ResetTrigger("NeutB");
    }
    public override void bDown()
    {
        Invoke("deactivate", 1.3f);
    }
}