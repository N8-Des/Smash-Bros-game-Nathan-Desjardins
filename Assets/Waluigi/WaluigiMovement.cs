using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaluigiMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    public BasicHurtbox NBHurtbox;
    public bool BSiding = false;
    public bool Dashing = false;
    public Vector3 OriginalPosition;
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
    public void EndUltiamte()
    {
        rb.useGravity = true;
        iCanMove = false;
        canMove = true;
        canAttack = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("CanAttack", true);
        anim.SetBool("IsIdle", true);
        anim.SetBool("Jumping", false);
        transform.position = OriginalPosition;
    }

    public void deactivate()
    {
        if (!Dashing)
        {
            me.velocity = new Vector3(0, 0, 0);
        }
        Dashing = false;
        rb.useGravity = true;
        iCanMove = false;
        canMove = true;
        canAttack = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("CanAttack", true);
        anim.SetBool("IsIdle", true);
        anim.SetBool("Jumping", false);
    }
    public void BSide()
    {
        iCanMove = true;
        Dashing = true;
        me.useGravity = false;
        if (isRight)
        {
            me.velocity = new Vector3(0, 0, 13f);
            BSiding = true;
            StartCoroutine(spawningThorns());
        }
        else
        {
            me.velocity = new Vector3(0, 0, -13f);
            BSiding = true;
            StartCoroutine(spawningThorns());
        }
    }
    #region bunch of overrides
    public override void SpecialDir1()
    {
        if (lastInput == "Right")
        {
            isRight = true;
            canJump = false;
            canAttack = false;
            canMove = false;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            anim.SetTrigger("BSide");
            attacking();

        }
        else
        {
            SpecialDir2();
        }
    }
    public override void SpecialDir2()
    {
        if (lastInput == "Left")
        {
            isRight = false;
            canJump = false;
            canAttack = false;
            canMove = false;
            transform.rotation = new Quaternion(0, 180, 0, 0);
            anim.SetTrigger("BSide");
            attacking();
        }
        else
        {
            SpecialDir3();
        }
    }
    #endregion
    public IEnumerator spawningThorns()
    {
        yield return new WaitForSeconds(0.03f);
        if (BSiding)
        {
            GameObject Thorns = GameObject.Instantiate((GameObject)Resources.Load("WaluigiThorns"));
            FriendlyHitbox friend = Thorns.GetComponent<FriendlyHitbox>();
            friend.friend = gameObject;
            if (isRight)
            {
                Thorns.transform.position = transform.position - new Vector3(0, 0, 0.3f);
            }
            else
            {
                Thorns.transform.position = transform.position + new Vector3(0, 0, 0.3f);
            }
            StartCoroutine(spawningThorns());
        }
    }
    public void SideBSlide()
    {
        BSiding = false;
        me.velocity = Vector3.zero;
        me.useGravity = true;
        if (isRight)
        {
            me.AddForce(0, 0, 3000);
        }
        else
        {
            me.AddForce(0, 0, -3000);
        }
    }
    #region charging NB stuff
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
                anim.SetBool("Charging", true);
                if (NBHurtbox.KB.z <= 2f)
                {
                    NBHurtbox.KB += new Vector3(0, 0.005f, 0.005f);

                }else
                {
                    anim.SetBool("Charging", false);
                    anim.SetTrigger("NBFullPower");
                    charging = false;
                    NBHurtbox.KB = new Vector3(0, 0.15f, 0.15f);
                    attacking();
                }
            }
            else
            {
                anim.SetBool("Charging", false);
                anim.SetTrigger("NBAttack");
                NBHurtbox.KB = new Vector3(0, 0.15f, 0.15f);
                charging = false;
                attacking();
            }
        }
    }
    #endregion
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
    public void Ult()
    {
        iCanMove = true;
        rb.useGravity = false;
        OriginalPosition = transform.position;
        if (isRight)
        {
            rb.velocity = new Vector3(0, 0, 20);           
        }
        else
        {
            rb.velocity = new Vector3(0, 0, -20);
        }
    }
}