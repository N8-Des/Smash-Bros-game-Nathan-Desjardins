using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbbeyMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    private int numFrames;
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
        jumpsLeft = 2;

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
            rb.AddForce(0, 0, 9000);
            //Invoke("deactivate", 0.56f);
        }
        else
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            rb.AddForce(0, 0, -9000);
            //Invoke("deactivate", 0.56f);
        }

    }
    public void downB()
    {
        iCanMove = true;
        StartCoroutine(BDown());
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
    public override void bUp()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        StartCoroutine(upB());
    }
    IEnumerator upB()
    {
        while (numFrames <= 30)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 1.7f, 0), Time.deltaTime * 2);
            numFrames += 1;
            yield return new WaitForEndOfFrame();
        }
        numFrames = 0;
        rb.useGravity = true;

    }
    public IEnumerator BDown()
    {
        if (!inAir)
        {
            if (isRight)
            {
                rb.AddForce(0, 9000, 400);
                yield return new WaitForSeconds(0.6f);
                rb.AddForce(0, -14000, 900);
            }
            else
            {
                rb.AddForce(0, 9000, -400);
                yield return new WaitForSeconds(0.6f);
                rb.AddForce(0, -14000, -900);
            }

        }
        else
        {
            rb.AddForce(0, -12000, 0);
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
    public override void baseB()
    {
        Invoke("baseStop", 3);
    }
    public override void bDown()
    {
        //anim.ResetTrigger("BDown");
        Invoke("deactivate", 1);
    }
    public void ShootCube()
    {
        if (!isShooting)
        {
            isShooting = true;
            GameObject Cube = GameObject.Instantiate((GameObject)Resources.Load("FrcCube "));
            PresentRB = Cube.GetComponent<Rigidbody>();
            FriendlyHitbox friendly = Cube.GetComponent<FriendlyHitbox>();
            friendly.friend = gameObject;
            //audioStrike.Play();
            if (!isRight)
            {
                Cube.transform.rotation = new Quaternion(0, 180, 0, 0);
                Cube.transform.position = transform.position + new Vector3(0, 0.7f, -0.2f);
                PresentRB.velocity = new Vector3(0, 2, -5f);
                //Invoke("baseStop", 0.6f);
            }
            else
            {
                Cube.transform.position = transform.position + new Vector3(0, 0.7f, 0.2f);
                PresentRB.velocity = new Vector3(0, 2, 5f);
                //Invoke("baseStop", 0.6f);
            }
            //anim.ResetTrigger("NeutB");
        }
    }

}