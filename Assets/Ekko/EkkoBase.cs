using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EkkoBase : BaseCharMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public bool canNB = true;
    public ParticleSystem part;
    public bool hasUB = false;
    public Vector3 timeLoc;
    public GameObject timeChange;
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
        me.velocity = moveSpeed * 1.4f;
        //Invoke("SBpt2", 0.5f);
    }
    public override void bLeft()
    {
        me = gameObject.GetComponent<Rigidbody>();
        iCanMove = true;
        me.velocity = moveSpeed * -1.4f;
        //Invoke("SBpt2", 0.5f);
    }
    public void SBpt2()
    {
        rb.velocity = new Vector3(0, 0, 0);
        if (!isRight)
        {
            transform.position += new Vector3(0, 0, -0.6f);
            Invoke("deactivate", 0.25f);
        }
        else
        {
            transform.position += new Vector3(0, 0, 0.6f);
            Invoke("deactivate", 0.25f);
        }
    }
    public void baseStop()
    {
        canAttack = true;
        canMove = true;
    }

    public override void bUp()
    {
        anim.SetBool("BUp", false);
        iCanMove = false;
        if (!hasUB)
        {
            timeChange = GameObject.Instantiate((GameObject)Resources.Load("EkkoTime"));
            timeChange.transform.position = this.transform.position;
            timeLoc = timeChange.transform.position;
            hasUB = true;
            Invoke("changeUB", 4);
            baseStop();
        }
        else
        {
            transform.position = timeLoc;
            hasUB = false;
            Destroy(timeChange.gameObject);
            baseStop();
        }
    }   


    public void changeUB()
    {
        hasUB = false;
        timeLoc = transform.position;
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
        if (canNB)
        {
            canNB = false;
            GameObject Timewinder = GameObject.Instantiate((GameObject)Resources.Load("EkkoQ"));
            PresentRB = Timewinder.GetComponent<Rigidbody>();
            if (!isRight)
            {
                Timewinder.transform.rotation = new Quaternion(0, 180, 0, 0);
                Timewinder.transform.position = transform.position + new Vector3(0, 0.05f, -0.6f);
                PresentRB.velocity = new Vector3(0, 0, -3);
                Invoke("baseStop", 0.05f);
                Invoke("waitForDeath", 1.7f);
            }
            else
            {
                Timewinder.transform.position = transform.position + new Vector3(0, 0.05f, 0.6f);
                PresentRB.velocity = new Vector3(0, 0, 3);
                Invoke("baseStop", 0.05f);
                Invoke("waitForDeath", 1.7f);
            }
        }
        else
        {
            baseStop();
        }
    }
    public override void bDown()
    {
        baseStop();
    }
    public void waitForDeath()
    {
        canNB = true;
    }
}