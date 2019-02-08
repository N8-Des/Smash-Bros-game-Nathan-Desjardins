using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorstMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    public BasicHurtbox NBHitbox;
    public bool CarOut = false;
    void Update()
    {
        Charge();
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
            gameObject.transform.position += new Vector3(0, 0, 2.3f);
            anim.SetTrigger("Reappear");
        }
        else
        {
            gameObject.transform.position += new Vector3(0, 0, -2.3f);
            anim.SetTrigger("Reappear");
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
    public void chargeUp()
    {
        charging = true;
    }
    public void Charge()
    {
        if (charging)
        {
            if (Input.GetButton(B) || Input.GetKey(KeyCode.A))
            {
                if (NBHitbox.KB.z >= 0.95f)
                {
                    anim.SetBool("Charging", false);
                    anim.SetTrigger("NBAttack");
                    charging = false;
                    attacking();
                }
                else
                {
                    anim.SetBool("Charging", true);
                    NBHitbox.KB += new Vector3(0, 0.004f, 0.004f);
                }
            }
            else
            {
                anim.SetBool("Charging", false);
                anim.SetTrigger("NBAttack");
                charging = false;
                attacking();
            }
        }
    }
    public void startUlt()
    {
        anim.SetBool("UltCharge", true);
    }
    public void stopUlt()
    {
        anim.SetBool("UltCharge", false);
        baseStop();
    }
    public void StopNB()
    {
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
        NBHitbox.KB = new Vector3(0, 0.1f, 0.1f);
    }
    public override void bUp()
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        rb.AddForce(0, 10000, 0);
        Invoke("deactivate", 0.2f);
    }
    public void SpawnCar()
    {
        if (!CarOut)
        {
            CarOut = true;
            GameObject Car = GameObject.Instantiate((GameObject)Resources.Load("ThanosCar"));
            FriendlyHitbox fhb = Car.GetComponent<FriendlyHitbox>();
            fhb.friend = gameObject;
            ThanosCar thanosCar = Car.GetComponent<ThanosCar>();
            thanosCar.spawner = this;
            if (!isRight)
            {
                Car.transform.rotation = new Quaternion(0, 180, 0, 0);
                Car.transform.position = transform.position + new Vector3(0, 0.15f, -0.6f);
                thanosCar.isRight = false;
                thanosCar.moving();
            }
            else
            {
                Car.transform.position = transform.position + new Vector3(0, 0.15f, 0.6f);
                thanosCar.moving();
            }
        }
    }
    public void StopGravity()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }
}