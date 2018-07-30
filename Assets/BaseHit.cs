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
    public int shieldHealth = 100;
    public bool isBlocking = false;
    public float SSM;
    public float OriginalShieldSize;
    public bool isInvuln = false;
    //public ProgressManager progressBar;
    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        pdisplay = GameObject.Find(UIname).GetComponent<PercentDisplay>();
        OriginalShieldSize = shield.transform.localScale.x;
        //charMove = gameObject.GetComponent<CharacterMove>();
        //charMove.progMan = progressBar;
        //progressBar.selectedPlayer = gameObject.GetComponent<CharacterMove>();
        SSM = OriginalShieldSize / 100;
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
                    shieldHealth = 100;
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
                //progressBar.ChangeValue(damage);
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
}
