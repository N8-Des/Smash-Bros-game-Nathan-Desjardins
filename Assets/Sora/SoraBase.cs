using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoraBase : BaseCharMove
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
        Invoke("baseStop", 1.1f);
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
        Invoke("baseStop", 1.1f);
    }
    public override void dair()
    {
        Invoke("baseStop", 1.2f);

    }
    public override void nair()
    {
        canAttack = true;
        canMove = true;
    }
    public override void bRight()
    {
        Invoke("baseStop", 2f);
    }
    public override void bLeft()
    {
        Invoke("baseStop", 2f);
    }
    public void baseStop()
    {
        canAttack = true;
        canMove = true;
    }
    public override void bUp()
    {
        rb.velocity = new Vector3(0, 3.3f, 0);
        Invoke("deactivate", 0.4f);
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
        Invoke("baseStop", 1.2f);
    }
    public override void bDown()
    {
        anim.ResetTrigger("DoneBDown");
        Invoke("deactivate", 4f);
    }
}