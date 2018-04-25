using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionBase : BaseCharMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public Material denseVision;
    public Material normalVision;
    public bool isDense = false;
    void Update()
    {
        if (moveRight == true || moveLeft == true)
        {
            anim.SetBool("isWalking", true);
        }

        if (isIdle == true)
        {
            anim.SetBool("isWalking", false);
        }
    }
    public override void BaseA()
    {
        anim.ResetTrigger("NeutA");
        canMove = true;
        canAttack = true;
        canJump = true;
    }
    public override void SideA()
    {
        canMove = true;
        canAttack = true;
        canJump = true;
    }
    public override void UpA()
    {
        canMove = true;
        canAttack = true;
    }

    public override void jump()
    {
        anim.ResetTrigger("Jump");
        rb.velocity = new Vector3(0, 6, 0);
        canMove = true;
        Invoke("stopJump", 0.2f);
    }
    public void stopJump()
    {
        rb.velocity = new Vector3(0, 0, 0);
        anim.ResetTrigger("Jump");
    }
    public override void DownA()
    {
        anim.ResetTrigger("DownA");
        canMove = true;
        canAttack = true;
        canJump = true;
    }
    public override void bAir()
    {
        fair();
    }
    public override void fair()
    {
        anim.ResetTrigger("Fair");
        canMove = true;
        canAttack = true;
    }
    public void deactivate()
    {
        iCanMove = false;
        me.velocity = new Vector3(0, 0, 0);
        anim.SetTrigger("DoneBSide");
        anim.SetTrigger("DoneBDown");
        canMove = true;
        canAttack = true;
    }
    public override void uAir()
    {
        canMove = true;
        canAttack = true;
    }
    public override void dair()
    {
        anim.ResetTrigger("Dair");
        canAttack = true;
        canMove = true;
    }
    public override void nair()
    {
        anim.ResetTrigger("Nair");
        canAttack = true;
        canMove = true;
    }
    public override void bRight()
    {
        anim.ResetTrigger("BSide");
        baseStop();
    }
    public override void bLeft()
    {
        anim.ResetTrigger("BSide");
        baseStop();
    }
    public void baseStop()
    {
        canAttack = true;
        canMove = true;
    }

    public override void bUp()
    {
        canBUp = false;
        rb.velocity = new Vector3(0, 6, 0);
        Invoke("StopEverything", 0.34f);
    }
    public void StopEverything()
    {
        anim.SetBool("BUp", false);
        canAttack = true;
        canMove = true;
        iCanMove = false;
        //rb.velocity = new Vector3(0, 0, 0);
    }
    public override void baseB()
    {
        baseStop();
    }
    public override void bDown()
    {
        BaseHit bh = gameObject.GetComponent<BaseHit>();
        GameObject visionM = gameObject.transform.Find("VisionMat").gameObject;
        Renderer rend = visionM.GetComponent<Renderer>();
        if (!isDense)
        {
            isDense = true;
            bh.kbResist = 0.3f;
            moveSpeed.z = 0.5f;
            moveSpeedAirL.z = 0.5f;
            moveSpeedAirR.z = 0.5f;
            rend.material = denseVision;
            baseStop();
        }
        else
        {
            bh.kbResist = 0.9f;
            isDense = false;
            moveSpeed.z = 2f;
            moveSpeedAirL.z = 2f;
            moveSpeedAirR.z = 2f;
            rend.material = normalVision;
            baseStop();
        }
    }
}