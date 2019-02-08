using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExbandMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public GameObject sbTarget;
    public bool isShooting = false;
    public Vector3 BUOffset;
    int numFrames;
    bool sideB;
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
    public override void bSideGrab(GameObject playerHit)
    {
        anim.SetTrigger("BSide2");
        sbTarget = playerHit;
        sideB = true;
        attacking();
        canAttack = false;
        canMove = false;
        canJump = false;
        StartCoroutine(moveToTargetPlayer());
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
    public void bSideHit()
    {
        BaseHit charHit = sbTarget.GetComponent<BaseHit>();
        charHit.TakeAttack(10, new Vector3(0, 0.5f, 0.4f), this);
        GameObject Audio = GameObject.Instantiate((GameObject)Resources.Load("Audh2"));
        sideB = false;
    }
    IEnumerator moveToTargetPlayer()
    {
        float DistX;
        Vector3 offsetSB = new Vector3(0, 0, -0.4f);
        if (!isRight)
        {
            offsetSB.z *= -1;
        }
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        DistX = transform.position.z - sbTarget.transform.position.z;
        while (!canMove && damageControl.isKnockedBack == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, sbTarget.transform.position + offsetSB, Time.deltaTime * 5);
            yield return new WaitForEndOfFrame();
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
        rb.useGravity = false;
        StartCoroutine(upB());
    }
    IEnumerator upB()
    {
        while (numFrames <= 20)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 3, 0), Time.deltaTime * 2);
            numFrames += 1;
            yield return new WaitForEndOfFrame();
        }
        numFrames = 0;
        rb.useGravity = true;

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

}