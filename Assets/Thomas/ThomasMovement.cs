using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThomasMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
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
    public override void OnCollisionEnter(Collision other)
    {
        if (!canBUp && other.collider.tag == "Ground")
        {
            canBUp = true;
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
            me.AddForce(0, 0, 4000);
        }
        else
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.AddForce(0, 0, -4000);
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

    public override void bUp()
    {
        canJump = false;
        if (isRight)
        {
            rb.velocity = new Vector3(0, 3.5f, 2);
            //Invoke("StopEverything", 0.2f);
        }
        else
        {
            rb.velocity = new Vector3(0, 3.5f, -2);
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
    public override void baseB()
    {
        Invoke("baseStop", 3);
    }
    public override void bDown()
    {
        //anim.ResetTrigger("BDown");
        Invoke("deactivate", 1);
    }
    public void Ultimate()
    {
        GameObject Tank = GameObject.Instantiate((GameObject)Resources.Load("CorruptThomas"));
        PresentRB = Tank.GetComponent<Rigidbody>();
        LotusUltHitbox ultHB = Tank.GetComponent<LotusUltHitbox>();
        ultHB.Lotus = this.gameObject;
        if (!isRight)
        {
            Tank.transform.rotation = new Quaternion(0, 180, 0, 0);
            Tank.transform.position = transform.position + new Vector3(0, 0.1f, 3);
            PresentRB.velocity = new Vector3(0, 0, -6f);
        }
        else
        {
            Tank.transform.position = transform.position + new Vector3(0, 0.1f, -3);
            PresentRB.velocity = new Vector3(0, 0, 6f);
        }
    }

}