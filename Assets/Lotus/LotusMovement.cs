using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody MelonRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
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
    public override void jump()
    {
        anim.ResetTrigger("Jump");
        rb.AddForce(0, 8000, 0);
        canMove = true;
        Invoke("stopJump", 0.2f);
    }
    public void stopJump()
    {
        isJumping = false;
        iCanMove = false;
        //rb.velocity = new Vector3(0, 0, 0);
        anim.ResetTrigger("Jump");
    }
    public void deactivate()
    {
        iCanMove = false;
        me.velocity = new Vector3(0, 0, 0);
        canMove = true;
        canAttack = true;
        anim.SetBool("isAttacking", false);
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
            me.velocity = moveSpeed * 2;
            Invoke("deactivate", 0.26f);
        }
        else
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * -2;
            Invoke("deactivate", 0.26f);
        }

    }
    public void baseStop()
    {
        isShooting = false;
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        if (onGround)
        {
            anim.SetBool("IsIdle", true);
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
    public void LotusBall()
    {
        GameObject Melon = GameObject.Instantiate((GameObject)Resources.Load("LotusMelonSB"));
        MelonRB = Melon.GetComponent<Rigidbody>();
        LotusMelonSB ball = Melon.GetComponent<LotusMelonSB>();
        ball.Lotus = this.gameObject;
        if (!isRight)
        {
            Melon.transform.rotation = new Quaternion(0, 180, 0, 0);
            Melon.transform.position = transform.position + new Vector3(0, 0.6f, -0.1f);
            MelonRB.velocity = new Vector3(0, 0, -4);
        }
        else
        {
            Melon.transform.position = transform.position + new Vector3(0, 0.6f, 0.1f);
            MelonRB.velocity = new Vector3(0, 0, 4);
        }
    }
    public void Ultimate()
    {
        GameObject Melon = GameObject.Instantiate((GameObject)Resources.Load("LotusWaterult"));
        Rigidbody melonUp = Melon.GetComponent<Rigidbody>();
        Melon.transform.position = this.transform.position;
        melonUp.velocity = new Vector3(0, 8, 0);
    }
    public void Ultimate2()
    {
        GameObject Melon = GameObject.Instantiate((GameObject)Resources.Load("LotusGiantMelon"));
        MelonRB = Melon.GetComponent<Rigidbody>();
        LotusUltHitbox ball = Melon.GetComponent<LotusUltHitbox>();
        ball.Lotus = this.gameObject;
        if (!isRight)
        {
            Melon.transform.rotation = new Quaternion(0, 180, 0, 0);
            Melon.transform.position = transform.position + new Vector3(0, 4f, -1f);
            MelonRB.velocity = new Vector3(0, -8, -1);
        }
        else
        {
            Melon.transform.position = transform.position + new Vector3(0, 4f, 1f);
            MelonRB.velocity = new Vector3(0, -8, 1);
        }
    }
}