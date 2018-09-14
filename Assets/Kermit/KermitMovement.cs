using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KermitMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    public GameObject hitPlayer;
    public BaseHit damageControlHit;
    public BaseHit playerHit;
    Vector3 originalPosition = Vector3.zero;
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
        rb.useGravity = true;
        me.velocity = new Vector3(0, 0, 0);
        canMove = true;
        canAttack = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("CanAttack", true);
        anim.SetBool("IsIdle", true);
        anim.SetBool("Jumping", false);

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
            me.velocity = moveSpeed * 2f;
        }
        else
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * -2f;
        }
    }
    public override void SpecialDir4()
    {
        if (lastInput == "Down")
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
            anim.SetBool("IsIdle", true);
            anim.SetBool("CanAttack", true);
            if (!inAir)
            {
                canJump = true;

            }
        }
    }
    public void startUlt()
    {
        anim.SetTrigger("Ult2");
    }
    public void NB2()
    {
        hitPlayer.transform.localScale *= 0.01f;
        hitPlayer.transform.position = this.transform.position;
        anim.SetTrigger("Chewing");
    }
    public void ThrowPlayer()
    {
        hitPlayer.transform.localScale *= 100;
        if (!isRight)
        {
            damageControlHit.TakeAttack(10, new Vector3(0, 0.5f, -0.5f));

        }
        else
        {
            damageControlHit.TakeAttack(10, new Vector3(0, 0.5f, 0.5f));
        }
    }
    public void ultHitPlayer()
    {
        if (!isRight)
        {
            playerHit.TakeAttack(17, new Vector3(0, 1f, -1f));
        }else
        {
            playerHit.TakeAttack(17, new Vector3(0, 1f, 1f));
        }
    }
    public void DamagePlayer()
    {
        damageControlHit.takeDamage(2);
    }
    public void baseStop()
    {
        isShooting = false;
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
        anim.SetBool("Jumping", false);
    }

    public override void bUp()
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
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
    public void ShootBilly()
    {
        if (!isShooting)
        {
            isShooting = true;
            GameObject Present = GameObject.Instantiate((GameObject)Resources.Load("BillyBullet"));
            PresentRB = Present.GetComponent<Rigidbody>();
            //audioStrike.Play();
            if (!isRight)
            {
                Present.transform.rotation = new Quaternion(0, 180, 0, 0);
                Present.transform.position = transform.position + new Vector3(0, 0.3f, -0.6f);
                PresentRB.velocity = new Vector3(0, 2, -5f);
                //Invoke("baseStop", 0.6f);
            }
            else
            {
                Present.transform.position = transform.position + new Vector3(0, 0.3f, 0.6f);
                PresentRB.velocity = new Vector3(0, 2, 5f);
                //Invoke("baseStop", 0.6f);
            }
            //anim.ResetTrigger("NeutB");
        }
    }

}