using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThomasBase : BaseCharMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
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
        me.velocity = moveSpeed * 3.5f;
        Invoke("deactivate", 0.5f);
    }
    public override void bLeft()
    {
        me = gameObject.GetComponent<Rigidbody>();
        iCanMove = true;
        me.velocity = moveSpeed * -3.5f;
        Invoke("deactivate", 0.5f);

    }
    public void baseStop()
    {
        canAttack = true;
        canMove = true;
    }

    public override void bUp()
    {
        if (isRight)
        {
            rb.velocity = new Vector3(0, 3.5f, 2);
            Invoke("BaseStop", 6f);
            //Invoke("StopEverything", 0.2f);
        }
        else
        {
            rb.velocity = new Vector3(0, 3.5f, -2);
            Invoke("BaseStop", 6f);
        }
    }
    public void bUp2()
    {
        if (isRight)
        {
            rb.velocity = new Vector3(0, -3.5f, 2);
        }
        else
        {
            rb.velocity = new Vector3(0, -3.5f, -2);
        }
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
        baseStop();
    }
    public override void bDown()
    {
        anim.ResetTrigger("DoneBDown");
        Invoke("deactivate", 0.4f);
    }
}