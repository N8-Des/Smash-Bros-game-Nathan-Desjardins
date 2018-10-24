using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EkkoMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    public bool canNB = true;
    public bool hasUB = false;
    public Vector3 timeLoc;
    public GameObject timeChange;
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
    public void bSide()
    {
        if (isRight)
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * 1.4f;
            //Invoke("deactivate", 0.56f);
        }
        else
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * -1.4f;
            //Invoke("deactivate", 0.56f);
        }
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
        isShooting = false;
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
    }
    public void echo()
    {
        canBUp = true;
        iCanMove = false;
        if (!hasUB)
        {
            timeChange = GameObject.Instantiate((GameObject)Resources.Load("EkkoTime"));
            timeChange.transform.position = this.transform.position;
            timeLoc = timeChange.transform.position;
            hasUB = true;
            //Invoke("changeUB", 4);
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
    public void StopAndHeal()
    {
        BaseHit box = gameObject.GetComponent<BaseHit>();
        box.percent -= 12;
        isShooting = false;
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
    }
    public override void bUp()
    {
        rb.AddForce(0, 6000, 0);
        Invoke("deactivate", 0.3f);
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
    public void TimeWinder()
    {
        if (canNB)
        {
            canNB = false;
            GameObject Timewinder = GameObject.Instantiate((GameObject)Resources.Load("EkkoQ"));
            PresentRB = Timewinder.GetComponent<Rigidbody>();
            if (!isRight)
            {
                BasicHurtbox timeHitbox = Timewinder.GetComponentInChildren<BasicHurtbox>();
                timeHitbox.isRight = false;
                Timewinder.transform.rotation = new Quaternion(0, 180, 0, 0);
                Timewinder.transform.position = transform.position + new Vector3(0, 0.1f, -0.0f);
                PresentRB.velocity = new Vector3(0, 0, -3);
                Invoke("baseStop", 0.05f);
                Invoke("waitForDeath", 1.7f);
            }
            else
            {
                Timewinder.transform.position = transform.position + new Vector3(0, 0.1f, 0.9f);
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
    public void waitForDeath()
    {
        canNB = true;
    }

}