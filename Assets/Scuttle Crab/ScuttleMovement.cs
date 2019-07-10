using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScuttleMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    private int numFrames;
    public GameObject UltParticle;
    private float maxMoveSpeed;
    private int acceleration;
    private bool canNB = true;
    private bool isWarded = false;
    void Start()
    {
        maxMoveSpeed = MaxSpeedGround;
        acceleration = AccelerationGround;
    }
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
        me.AddForce(0, 0, 2000);
        Invoke("deactivate", 0.26f);
    }
    public override void bLeft()
    {
        me = gameObject.GetComponent<Rigidbody>();
        iCanMove = true;
        me.AddForce(0, 0, -2000);
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
    public void upB()
    {
        rb.useGravity = false;
        rb.AddForce(0, 4500, 0);
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
        if (canNB)
        {
            canNB = false;
            Invoke("NBCooldown", 4);
            damageControl.percent -= 6;
            damageControl.pdisplay.takeDamage(-6);
            MaxSpeedGround = 1.2f;
            AccelerationGround = 100;
        }
    }
    public void DownB()
    {
        isWarded = true;
    }
    void NBCooldown()
    {
        canNB = true;
        MaxSpeedGround = maxMoveSpeed;
        AccelerationGround = acceleration;
    }
    public override void bDown()
    {
        //anim.ResetTrigger("BDown");
        Invoke("deactivate", 1);
    }
    public override void attackUpdate()
    {
        lastInput = getLastInput();
        if ((Input.GetButton(A) || Input.GetKey(KeyCode.Z)) && ((canAttack && onGround && !damageControl.isKnockedBack && !isGrabbing) || isWarded))
        {
            canJump = false;
            canAttack = false;
            canMove = false;
            anim.SetBool("IsIdle", false);
            if (neutralY && neutralX)
            {
                if (isWarded)
                {
                    anim.SetTrigger("DownBAttack");
                    isWarded = false;
                    attacking();
                }
                else
                {
                    anim.SetTrigger("NeutA");
                    attacking();
                }

            }
            else
            {
                attackDir1();
            }
        }
        else if ((Input.GetButton(A) || Input.GetKey(KeyCode.Z)) && canAttack && inAir && !damageControl.isKnockedBack)
        {
            lastInput = getLastInput();
            canAttack = false;
            canMove = false;
            canJump = false;
            anim.SetBool("IsIdle", false);
            if (neutralY && neutralX)
            {
                anim.ResetTrigger("Jump");
                anim.SetTrigger("Nair");
                anim.SetBool("Jumping", false);
                canJump = false;
                attacking();
            }
            else
            {
                attackAirDir1();
            }
        }
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
    public void Ult()
    {
        UltParticle.SetActive(true);
        damageControl.isInvuln = true;
        Invoke("EndUlt", 8);
    }
    private void EndUlt()
    {
        UltParticle.SetActive(false);
        damageControl.isInvuln = false;
    }

}