using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeteMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    public bool needToTurn = false;
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

    public override void TurnLeft()
    {
        needToTurn = true;
    }
    public void turnIfNeeded()
    {
        if (needToTurn)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            isRight = false;
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
            me.velocity = moveSpeed * 3.5f;
            //Invoke("deactivate", 0.56f);
        }
        else
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * -3.5f;
            //Invoke("deactivate", 0.56f);
        }
    }
    public override void bUp()
    {
        rb.AddForce(0, 4500, 0);
        Invoke("deactivate", 0.3f);
    }
    public void StopMovement()
    {
        me.velocity = new Vector3(0, 0, 0);
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

    public void Snowball()
    {
        {
            GameObject Present = GameObject.Instantiate((GameObject)Resources.Load("Snowball"));
            PresentRB = Present.GetComponent<Rigidbody>();
            //audioStrike.Play();
            if (!isRight)
            {
                Present.transform.rotation = new Quaternion(0, 180, 0, 0);
                Present.transform.position = transform.position + new Vector3(0, 0.3f, -0.5f);
                PresentRB.velocity = new Vector3(0, 0, -4);
                //Invoke("baseStop", 0.6f);
            }
            else
            {
                Present.transform.position = transform.position + new Vector3(0, 0.3f, 0.5f);
                PresentRB.velocity = new Vector3(0, 0, 4);
                //Invoke("baseStop", 0.6f);
            }
            //anim.ResetTrigger("NeutB");
        }
    }
    public void IcebergUltimate()
    {
        GameObject Iceberg = GameObject.Instantiate((GameObject)Resources.Load("Iceberg"));
        PresentRB = Iceberg.GetComponent<Rigidbody>();
        LotusUltHitbox hurtbox = Iceberg.GetComponent<LotusUltHitbox>();
        hurtbox.Lotus = this.gameObject;
        if (!isRight)
        {
            Iceberg.transform.rotation = new Quaternion(0, 180, 0, 0);
            Iceberg.transform.position = transform.position + new Vector3(0, 2.4f, -0.5f);
            PresentRB.velocity = new Vector3(0, 0, -5);
            //Invoke("baseStop", 0.6f);
        }
        else
        {
            Iceberg.transform.position = transform.position + new Vector3(0, 2.4f, 0.5f);
            PresentRB.velocity = new Vector3(0, 0, 5);
            //Invoke("baseStop", 0.6f);
        }
    }
}