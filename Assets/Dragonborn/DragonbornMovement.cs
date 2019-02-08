using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonbornMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    bool isSpellbreaker;
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
        if (!Input.GetButton(B) && isSpellbreaker)
        {
            anim.SetBool("BDown", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isIdle", true);
            canJump = true;
            canAttack = true;
            canMove = true;
            isSpellbreaker = false;
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
    public void baseStop()
    {
        isShooting = false;
        canAttack = true;
        canMove = true;
        canBUp = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
        anim.SetBool("Jumping", false);

    }
    public override void SpecialDir4()
    {
        if (lastInput == "Down" && !inAir)
        {
            isSpellbreaker = true;
            anim.SetBool("BDown", true);
            anim.SetBool("isAttacking", true);
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
    public void shootArrow()
    {
        GameObject Arrow = GameObject.Instantiate((GameObject)Resources.Load("EbonyArrow"));
        Rigidbody arr = Arrow.GetComponent<Rigidbody>();
        BasicHurtbox arrowHitbox = Arrow.GetComponentInChildren<BasicHurtbox>();
        //audioStrike.Play();
        if (!isRight)
        {
            arrowHitbox.KB *= -1;
            Arrow.transform.rotation = new Quaternion(0, 180, 0, 0);
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
    public void Ultimate()
    {
        GameObject DragonOda = GameObject.Instantiate((GameObject)Resources.Load("Odahviing"));
        Rigidbody drag = DragonOda.GetComponent<Rigidbody>();
        //audioStrike.Play();
        if (!isRight)
        {
            DragonOda.transform.rotation = new Quaternion(180, 0, 0, 0);
            DragonOda.transform.position = transform.position + new Vector3(0, 2f, 0.8f);
            drag.velocity = new Vector3(0, 0, -6);
        }
        else
        {
            DragonOda.transform.position = transform.position + new Vector3(0, 2f, -0.8f);
            drag.velocity = new Vector3(0, 0, 6);
        }
        //anim.ResetTrigger("NeutB");
    }

}