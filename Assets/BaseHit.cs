using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHit : MonoBehaviour {
    public bool isKnockedBack = false;
    public float kbResist;
    public int percent = 0;
    public Rigidbody rb;
    public string UIname;
    public PercentDisplay pdisplay;
    public CharacterMove charMove;
    public Animator anim;
    public GameObject shield;
    public int shieldHealth = 50;
    public bool isBlocking = false;
    public float SSM;
    public float OriginalShieldSize;
    public bool isInvuln = false;
    public ProgressManager progressBar;
    public bool ultOn = true;
    public GameObject invulnIndicator;
    public bool SuperArmor;
    public virtual void invulnStart()
    {
        invulnIndicator.SetActive(true);
        isInvuln = true;
        Invoke("invulnEnd", 2.2f);
    }
    public virtual void invulnEnd()
    {
        Debug.Log("WHAT");
        invulnIndicator.SetActive(false);
        isInvuln = false;
    }
    public virtual void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        OriginalShieldSize = shield.transform.localScale.x;
        charMove = gameObject.GetComponent<CharacterMove>();
        SSM = OriginalShieldSize / 100;
        pdisplay = GameObject.Find(UIname).GetComponent<PercentDisplay>();
        if (ultOn)
        {
            progressBar.selectedPlayer = gameObject.GetComponent<CharacterMove>();
            charMove.progMan = progressBar;
        }
    }
    public void takeDamage(int damage)
    {
        percent += damage;
        pdisplay.takeDamage(damage);
    }
    public virtual void TakeAttack(int damage, Vector3 knockback, CharacterMove attacker)
    {
        if (charMove.isCountering)
        {
            charMove.isCountering = false;
            charMove.counter(damage);
            if (attacker != null)
            {
                attacker.gotCountered();
                isInvuln = true;
            }
        }
        else if (!isInvuln)
        {
            if (isBlocking)
            {
                shieldHealth -= damage;
                shield.transform.localScale -= new Vector3((damage * SSM), (damage * SSM), (damage * SSM));
                if (shieldHealth <= 0)
                {
                    isBlocking = false;
                    charMove.canBlock = false;
                    rb.AddForce(0, 3000, 0);
                    charMove.takeStun(6);
                    shield.transform.localScale = new Vector3(OriginalShieldSize, OriginalShieldSize, OriginalShieldSize);
                    shieldHealth = 50;
                }
            }
            else
            {
                if (SuperArmor)
                {
                    pdisplay = GameObject.Find(UIname).GetComponent<PercentDisplay>();
                    percent += damage;
                    pdisplay.takeDamage(damage);
                }
                else
                {
                    rb.useGravity = true;
                    pdisplay = GameObject.Find(UIname).GetComponent<PercentDisplay>();
                    anim.SetBool("knockedBack", true);
                    anim.SetBool("IsIdle", false);
                    anim.SetBool("CanAttack", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("FSmashCharge", false);
                    anim.SetBool("DSmashCharge", false);
                    anim.SetBool("USmashCharge", false);
                    anim.SetBool("LedgeGrab", false);
                    charMove.charging = false;
                    isKnockedBack = true;
                    pdisplay.takeDamage(damage);
                    //Debug.Log((knockback.y + knockback.z) * (percent / 4) * (kbResist));
                    percent += damage;
                    rb.velocity = Vector3.zero;
                    Vector3 calculatedKnockback = (new Vector3(0, knockback.y * 1.2f, knockback.z) *1000) * (percent / 7) * kbResist; //version 2 of knockback. change it to (percent / 10) for original.
                    rb.AddForce(calculatedKnockback);
                    Invoke("stopKB", (Mathf.Abs(knockback.y) + Mathf.Abs(knockback.z) * (percent / 10) * (kbResist)) / 10);
                    if (ultOn)
                    {
                        progressBar.ChangeValue(damage);
                    }
                }
            }
        }
    }
    public virtual void stopKB()
    {
        anim.SetBool("knockedBack", false);
        anim.SetBool("CanAttack", true);
        anim.SetBool("isStunned", false);
        anim.SetBool("Charging", false);
        anim.SetBool("IsIdle", true);
        charMove.canMove = true;
        charMove.canAttack = true;
        charMove.iCanMove = false;
        isKnockedBack = false;
        charMove.canBlock = true;
        charMove.charging = false;
        charMove.isGrabbing = false;
    }
    public virtual void resetPerc() {
        percent = 0;
        pdisplay.resetPercentDisplay();
    }
    public virtual void heal(int healAmount)
    {
        percent += healAmount;
        pdisplay.takeDamage(healAmount);
    }
    public virtual void takeUlt(int damage, Vector3 knockback)
    {
        if (!isInvuln)
        {

            pdisplay = GameObject.Find(UIname).GetComponent<PercentDisplay>();
            anim.SetBool("knockedBack", true);
            anim.SetBool("IsIdle", false);
            anim.SetBool("CanAttack", false);
            anim.SetBool("isAttacking", false);
            isKnockedBack = true;
            pdisplay.takeDamage(damage);
            //Debug.Log((knockback.y + knockback.z) * (percent / 4) * (kbResist));
            percent += damage;
            rb.velocity = knockback * (percent / 10) * kbResist;
            Invoke("stopKB", ((knockback.y + knockback.z) * (percent / 10) * (kbResist)) / 10);
        }
    }
    public virtual void EkkoUlt()
    {
        double healthReduce = Math.Ceiling((double)percent / 5);
        int healthReduceInt = Convert.ToInt32(healthReduce);
        pdisplay.takeDamage(healthReduceInt * -1);
    }
    public virtual void setInvBoolTrue()
    {
        isInvuln = true;
    }
    public virtual void setInvBoolFalse()
    {
        isInvuln = false;
    }
    public virtual void setSuperBoolTrue()
    {
        SuperArmor = true;
    }
    public virtual void setSuperBoolFalse()
    {
        SuperArmor = false;
    }
}
