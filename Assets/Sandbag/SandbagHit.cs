using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandbagHit : BaseHit
{

    public override void invulnStart()
    {
        invulnIndicator.SetActive(true);
        isInvuln = true;
        Invoke("invulnEnd", 2.2f);
    }
    public override void invulnEnd()
    {
        invulnIndicator.SetActive(false);
        isInvuln = false;
    }
    public override void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        SSM = OriginalShieldSize / 50;
        pdisplay = GameObject.Find(UIname).GetComponent<PercentDisplay>();
    }
    public override void TakeAttack(int damage, Vector3 knockback)
    {
        if (!isInvuln)
        {
            pdisplay = GameObject.Find(UIname).GetComponent<PercentDisplay>();
            anim.SetBool("IsIdle", false);
            anim.SetBool("knockedBack", true);
            isKnockedBack = true;
            pdisplay.takeDamage(damage);
            percent += damage;
            rb.velocity = knockback * (percent / 10) * kbResist;
            Invoke("stopKB", ((knockback.y + knockback.z) * (percent / 10) * (kbResist)) / 10);
        }
    }
    public override void stopKB()
    {
        anim.SetBool("knockedBack", false);
        anim.SetBool("IsIdle", true);
        isKnockedBack = false;
    }
    public override  void resetPerc()
    {
        percent = 0;
        pdisplay.resetPercentDisplay();
    }
    public override void heal()
    {
        pdisplay.takeDamage(-12);
    }
    public override void takeUlt(int damage, Vector3 knockback)
    {
        if (!isInvuln)
        {

            pdisplay = GameObject.Find(UIname).GetComponent<PercentDisplay>();
            anim.SetBool("knockedBack", true);
            anim.SetBool("IsIdle", false);
            isKnockedBack = true;
            pdisplay.takeDamage(damage);
            percent += damage;
            rb.velocity = knockback * (percent / 10) * kbResist;
            Invoke("stopKB", ((knockback.y + knockback.z) * (percent / 10) * (kbResist)) / 10);
        }
    }
    public override void EkkoUlt()
    {
        double healthReduce = Math.Ceiling((double)percent / 5);
        int healthReduceInt = Convert.ToInt32(healthReduce);
        pdisplay.takeDamage(healthReduceInt * -1);
    }
    public override void setInvBoolTrue()
    {
        isInvuln = true;
    }
    public override void setInvBoolFalse()
    {
        isInvuln = false;
    }
}
