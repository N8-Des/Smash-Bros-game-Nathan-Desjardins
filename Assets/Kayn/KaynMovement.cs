using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaynMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    public bool isShadowed = false;
    public bool isRhaast = false;
    public GameObject shadowIndication;
    public GameObject rhaastIndication;
    public BaseHit playerHit;
    public Renderer kaynMat;
    public Material BaseKaynMaterial;
    public Material transparent;

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
        }
        else
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * -2.4f;
        }
    }
    public void endUltNoAnim()
    {
        kaynMat.material = BaseKaynMaterial;
        baseStop();
    }
    public void endUlt()
    {
        anim.SetBool("isAttacking", true);
        anim.SetBool("IsIdle", false);
        kaynMat.material = BaseKaynMaterial;
        anim.SetTrigger("Ult2");        
    }
    public void hitPlayerWithUlt()
    {
        iCanMove = true;
        damageControl.setInvBoolTrue();
        if (!isRight)
        {
            rb.AddForce(0, -2000, 0);
            playerHit.TakeAttack(30, new Vector3(0, 1, -1), this);

        }
        else
        {
            rb.AddForce(0, 2000, 0);
            playerHit.TakeAttack(30, new Vector3(0, 1, 1), this);
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
    public void startUlt()
    {
        damageControl.setInvBoolTrue();
        canMove = false;
        canJump = false;
        canAttack = false;
        GameObject Rhaast = GameObject.Instantiate((GameObject)Resources.Load("RhaastUlt"));
        RhaastUlt scythe = Rhaast.GetComponent<RhaastUlt>();
        scythe.Kayn = this.gameObject;
        scythe.hitPlayer = playerHit.gameObject;
        kaynMat.material = transparent;
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
    public void switchSides()
    {
        if (!isShadowed && !isRhaast)
        {
            isShadowed = true;
            shadowIndication.SetActive(true);
            Invoke("stopShadow", 0.45f);
        }
        else if (isShadowed)
        {
            isShadowed = false;
            isRhaast = true;
            shadowIndication.SetActive(false);
            rhaastIndication.SetActive(true);
            Invoke("stopRhaast", 0.45f);
        }
        else if (isRhaast)
        {
            isShadowed = true;
            isRhaast = false;
            shadowIndication.SetActive(true);
            rhaastIndication.SetActive(false);
            Invoke("stopShadow", 0.45f);
        }
    }
    public void healRhaast()
    {
        damageControl.heal(-2);
    }
    public void stopShadow()
    {
        shadowIndication.SetActive(false);
    }
    public void stopRhaast()
    {
        rhaastIndication.SetActive(false);
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
                Present.transform.position = transform.position + new Vector3(0, 0.3f, -0.6f);
                PresentRB.velocity = new Vector3(0, 2, -5f);
                //Invoke("baseStop", 0.6f);
            }
            else
            {
                Present.transform.position = transform.position + new Vector3(0, 0.3f, 0.6f);
                PresentRB.velocity = new Vector3(0, 2, 5f);
                //Invoke("baseStop", 0.6f);
            }
            //anim.ResetTrigger("NeutB");
        }
    }

}