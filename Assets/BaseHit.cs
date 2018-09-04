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

    public void invulnStart()
    {
        invulnIndicator.SetActive(true);
        isInvuln = true;
        Invoke("invulnEnd", 2.2f);
    }
    public void invulnEnd()
    {
        invulnIndicator.SetActive(false);
        isInvuln = false;
    }
    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        OriginalShieldSize = shield.transform.localScale.x;
        charMove = gameObject.GetComponent<CharacterMove>();
        SSM = OriginalShieldSize / 50;
        pdisplay = GameObject.Find(UIname).GetComponent<PercentDisplay>();
        if (ultOn)
        {
            progressBar.selectedPlayer = gameObject.GetComponent<CharacterMove>();
            charMove.progMan = progressBar;
        }
    }
    public virtual void TakeAttack(int damage, Vector3 knockback)
    {
        if (!isInvuln)
        {
            if (charMove.isCountering)
            {
                charMove.counter(damage);
            }
            else if (isBlocking)
            {
                shieldHealth -= damage;
                shield.transform.localScale -= new Vector3((damage * SSM), (damage * SSM), (damage * SSM));
                if (shieldHealth <= 0)
                {
                    isBlocking = false;
                    charMove.canBlock = false;
                    TakeAttack(30, new Vector3(0, 0.8f, 0));
                    shield.transform.localScale = new Vector3(OriginalShieldSize, OriginalShieldSize, OriginalShieldSize);
                    shieldHealth = 50;
                }
            }
            else
            {
                pdisplay = GameObject.Find(UIname).GetComponent<PercentDisplay>();
                anim.SetBool("knockedBack", true);
                anim.SetBool("IsIdle", false);
                anim.SetBool("CanAttack", false);
                anim.SetBool("isAttacking", false);
                charMove.charging = false;
                isKnockedBack = true;
                pdisplay.takeDamage(damage);
                //Debug.Log((knockback.y + knockback.z) * (percent / 4) * (kbResist));
                percent += damage;
                rb.velocity = knockback * (percent / 10) * kbResist;
                Invoke("stopKB", ((knockback.y + knockback.z) * (percent / 10) * (kbResist)) / 10);
                if (ultOn)
                {
                    progressBar.ChangeValue(damage);
                }
            }
        }
    }
    void stopKB()
    {
        anim.SetBool("knockedBack", false);
        anim.SetBool("IsIdle", true);
        anim.SetBool("CanAttack", true);
        charMove.canMove = true;
        charMove.canAttack = true;
        isKnockedBack = false;
        charMove.canBlock = true;
    }
    public void resetPerc() {
        percent = 0;
        pdisplay.resetPercentDisplay();
    }
    public void heal()
    {
        pdisplay.takeDamage(-12);
    }
    public void takeUlt(int damage, Vector3 knockback)
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
    public void EkkoUlt()
    {
        double healthReduce = Math.Ceiling((double)percent / 5);
        int healthReduceInt = Convert.ToInt32(healthReduce);
        pdisplay.takeDamage(healthReduceInt * -1);
    }
    public void setInvBoolTrue()
    {
        isInvuln = true;
    }
    public void setInvBoolFalse()
    {
        isInvuln = false;
    }
}
