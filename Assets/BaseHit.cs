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
    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        pdisplay = GameObject.Find(UIname).GetComponent<PercentDisplay>();
    }
    public virtual void TakeAttack(int damage, Vector3 knockback)
    {
        charMove = gameObject.GetComponent<CharacterMove>();
        if (charMove.isCountering)
        {
            charMove.counter(damage);
        }
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
    void stopKB()
    {
        anim.SetBool("knockedBack", false);
        anim.SetBool("IsIdle", true);
        anim.SetBool("CanAttack", true);
        charMove.canMove = true;
        charMove.canAttack = true;
        isKnockedBack = false;
    }
    public void resetPerc() {
        percent = 0;
        pdisplay.resetPercentDisplay();
    }
    public void heal()
    {
        pdisplay.takeDamage(-12);
    }

}
