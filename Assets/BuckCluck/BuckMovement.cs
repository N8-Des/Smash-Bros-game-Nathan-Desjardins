using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckMovement : CharacterMove
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
    public void deactivate()
    {
        rb.useGravity = true;
        iCanMove = false;
        me.velocity = new Vector3(0, 0, 0);
        canMove = true;
        canAttack = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("CanAttack", true);
        anim.SetBool("IsIdle", true);
        anim.SetBool("Jumping", false);
    }
    public void loseSpeed()
    {
        if (isRight)
        {
            me.velocity = new Vector3(0, 0, 0.6f);
        } else
        {
            me.velocity = new Vector3(0, 0, -0.6f);
        }
    }
    public override void bRight()
    {
        me = gameObject.GetComponent<Rigidbody>();
        iCanMove = true;
        me.velocity = moveSpeed * 2;
        //Invoke("deactivate", 0.26f);
    }
    public override void bLeft()
    {
        me = gameObject.GetComponent<Rigidbody>();
        iCanMove = true;
        me.velocity = moveSpeed * -2;
        //("deactivate", 0.26f);
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
                Present.transform.position = transform.position + new Vector3(0, 0.3f, -0.4f);
                PresentRB.velocity = new Vector3(0, 0, -5f);
                //Invoke("baseStop", 0.6f);
            }
            else
            {
                Present.transform.position = transform.position + new Vector3(0, 0.3f, 0.4f);
                PresentRB.velocity = new Vector3(0, 0, 5f);
                //Invoke("baseStop", 0.6f);
            }
            //anim.ResetTrigger("NeutB");
        }
    }
    public void Ultimate()
    {
        GameObject Hexagon = GameObject.Instantiate((GameObject)Resources.Load("BuckSky"));
        Rigidbody HexaRB = Hexagon.GetComponent<Rigidbody>();
        //audioStrike.Play();
        if (!isRight)
        {
            Hexagon.transform.position = transform.position + new Vector3(0, 5f, -3.5f);
            HexaRB.velocity = new Vector3(0, -9, 0);
        }
        else
        {
            Hexagon.transform.position = transform.position + new Vector3(0, 5f, 3.5f);
            HexaRB.velocity = new Vector3(0, -9, 0);
        }
    }
}
