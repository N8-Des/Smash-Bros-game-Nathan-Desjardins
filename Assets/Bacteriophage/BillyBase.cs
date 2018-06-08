using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillyBase : BaseCharMove
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
        Invoke("baseStop", 1.1f);
    }
    public override void bLeft()
    {
        Invoke("baseStop", 1.1f);
    }
    public void baseStop()
    {
        canAttack = true;
        canMove = true;
    }
    public override void bUp()
    {
        rb.velocity = new Vector3(0, 3.3f, 0);
        Invoke("deactivate", 1.2f);
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

    public void ShootBilly()
    {
        GameObject bullet = GameObject.Instantiate((GameObject)Resources.Load("BillyBullet"));
        Rigidbody arr = bullet.GetComponent<Rigidbody>();
        //audioStrike.Play();
        if (!isRight)
        {
            bullet.transform.rotation = new Quaternion(0, 180, 0, 0);
            bullet.transform.position = transform.position + new Vector3(0, 0.2f, -0.3f);
            arr.velocity = new Vector3(0, 2, -8);
            Invoke("baseStop", 0.7f);
        }
        else
        {
            bullet.transform.position = transform.position + new Vector3(0, 0.2f, 0.3f);
            arr.velocity = new Vector3(0, 2, 8);
            Invoke("baseStop", 0.7f);
        }
        //anim.ResetTrigger("NeutB");
    }
    public override void bDown()
    {
        Invoke("deactivate", 1.3f);
    }
}