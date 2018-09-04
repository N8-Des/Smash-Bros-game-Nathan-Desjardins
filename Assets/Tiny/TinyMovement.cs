using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool canThrow = true;
    public BasicHurtbox bsHitbox;
    void Update()
    {
        Charge();
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
    public void bSide()
    {
        if (isRight)
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * 2.4f;
            //Invoke("deactivate", 0.56f);
        }
        else
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * -2.4f;
            //Invoke("deactivate", 0.56f);
        }

    }
    public void chargeUp()
    {
        charging = true;
    }
    public void Charge()
    {
        if (charging)
        {
            if (Input.GetButton(B) || Input.GetKey(KeyCode.A))
            {
                anim.SetBool("Charging", true);
                bsHitbox.KB += new Vector3(0, 0.005f, 0.005f);
            }
            else
            {
                anim.SetBool("Charging", false);
                anim.SetTrigger("SBAttack");
                charging = false;
                attacking();
            }
        }
    }
    public void baseStop()
    {
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
    }
    public void stopSideB()
    {
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
        bsHitbox.KB = new Vector3(0, 0.4f, 0.4f);
    }
    public void grabbed()
    {
        anim.SetBool("grabbing", true);
        anim.SetTrigger("grab");
    }
    public void endGrab()
    {
        anim.SetBool("grabbing", false);
        baseStop();
    }
    public override void bUp()
    {
        rb.AddForce(0, 11000, 0);
        Invoke("deactivate", 0.3f);
    }
    public void BDown()
    {
        rb.AddForce(0, -9000, 0);
        Invoke("deactivate", 0.3f);
    }
    public override void OnCollisionEnter(Collision other)
    {
        if (!canBUp && other.collider.tag == "Ground")
        {
            canBUp = true;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("DownB") && other.collider.tag == "Ground")
        {
            {
                canJump = true;
                anim.SetBool("isAttacking", false);
                anim.SetBool("IsIdle", true);
                canAttack = true;
                canMove = true;
                if (!iCanMove)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                }
            }
        }
    }
    public override void SpecialDir4()
    {
        if (lastInput == "Down" && inAir)
        {
            canJump = false;
            canAttack = false;
            canMove = false;
            anim.SetTrigger("BDown");
            attacking();
        }
        else
        {
            canAttack = true;
            canMove = true;
            canJump = true;
            anim.SetBool("IsIdle", true);
            anim.SetBool("CanAttack", true);
        }
    }
    public override void bDown()
    {
        //anim.ResetTrigger("BDown");
        Invoke("deactivate", 1);
    }
    public void ThrowAxe()
    {
        if (canThrow) {
            GameObject Axe = GameObject.Instantiate((GameObject)Resources.Load("TinyAxeThrown"));
            PresentRB = Axe.GetComponent<Rigidbody>();
            TinyAxe AxeThrown = Axe.GetComponent<TinyAxe>();
            AxeThrown.Tiny = this.gameObject;
            canThrow = false;
            if (!isRight)
            {
                Axe.transform.rotation = new Quaternion(0, 180, 0, 0);
                Axe.transform.position = transform.position + new Vector3(0, 0.3f, -0.1f);
                AxeThrown.speed *= -1;
            }
            else
            {
                Axe.transform.position = transform.position + new Vector3(0, 0.3f, 0.1f);
            }
        }
    }

}