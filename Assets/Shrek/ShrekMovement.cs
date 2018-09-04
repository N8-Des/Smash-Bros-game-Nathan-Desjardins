using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrekMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public Rigidbody OnionRB;
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
        rb.AddForce(0, 6000, 0);
        Invoke("deactivate", 0.3f);
    }
    public void downAir()
    {
        rb.AddForce(0, -9000, 0);
    }
    public override void bDown()
    {
        //anim.ResetTrigger("BDown");
        Invoke("deactivate", 1);
    }
    public void Donkay()
    {
        if (!isShooting)
        {
            isShooting = true;
            GameObject Present = GameObject.Instantiate((GameObject)Resources.Load("DonkeyShrek"));
            PresentRB = Present.GetComponent<Rigidbody>();
            audioStrike.Play();
            if (!isRight)
            {
                Present.transform.rotation = new Quaternion(0, 180, 0, 0);
                Present.transform.position = transform.position + new Vector3(0, 0.1f, -0.4f);
                PresentRB.velocity = new Vector3(0, 0, -3f);
                //Invoke("baseStop", 0.6f);
            }
            else
            {
                Present.transform.position = transform.position + new Vector3(0, 0.1f, 0.4f);
                PresentRB.velocity = new Vector3(0, 0, 3f);
                //Invoke("baseStop", 0.6f);
            }
            //anim.ResetTrigger("NeutB");
        }
    }
    public void Ultimate()
    {
        GameObject GoldenOnion = GameObject.Instantiate((GameObject)Resources.Load("Onion"));
        OnionRB = GoldenOnion.GetComponent<Rigidbody>();
        LotusUltHitbox ultHB = GoldenOnion.GetComponent<LotusUltHitbox>();
        ultHB.Lotus = this.gameObject;
        if (!isRight)
        {
            GoldenOnion.transform.rotation = new Quaternion(0, 180, 0, 0);
            GoldenOnion.transform.position = transform.position + new Vector3(0, 0, -0.2f);
            OnionRB.velocity = new Vector3(0, 0, -4f);
            //Invoke("baseStop", 0.6f);
        }
        else
        {
            GoldenOnion.transform.position = transform.position + new Vector3(0, 0, 0.2f);
            OnionRB.velocity = new Vector3(0, 0, 4f);
            //Invoke("baseStop", 0.6f);
        }
        //anim.ResetTrigger("NeutB");
    }

}