using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoutubeMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    public int timesSpawned;
    public BaseHit damageTaker;
    public bool needToTurn = false;
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
        rb.useGravity = true;
        iCanMove = false;
        me.velocity = new Vector3(0, 0, 0);
        canMove = true;
        canAttack = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("CanAttack", true);
        anim.SetBool("IsIdle", true);
    }
    public void startUlt()
    {
        damageControl.isInvuln = true;
        StartCoroutine(SpawnMeteor());
    }

    public override void TurnLeft()
    {
        needToTurn = true;
    }
    public void turnIfNeeded()
    {
        if (needToTurn)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            isRight = false;
        }
    }
    public IEnumerator SpawnMeteor()
    {
        timesSpawned += 1;
        GameObject Meteor = GameObject.Instantiate((GameObject)Resources.Load("YoutubeMeteor"));
        Rigidbody rbmeteor = Meteor.GetComponent<Rigidbody>();
        Meteor.transform.position = this.transform.position + new Vector3(0, 6, Random.Range(-2, 2));
        rbmeteor.velocity = new Vector3(0, -4.2f, Random.Range(-2, 2));
        yield return new WaitForSeconds(0.25f);
        if (timesSpawned >= 25)
        {
            EndUltimate();
        } else
        {
            StartCoroutine(SpawnMeteor());
        }
    }
    public void EndUltimate()
    {
        timesSpawned = 0;
        damageControl.isInvuln = false;
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
    }
    public void bSide()
    {
        if (isRight)
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * 2.4f;
            //Invoke("deactivate", 0.56f);
        }
        else
        {
            me = gameObject.GetComponent<Rigidbody>();
            iCanMove = true;
            me.velocity = moveSpeed * -2.4f;
            //Invoke("deactivate", 0.56f);
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
    }

    public override void bUp()
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        rb.AddForce(0, 6000, 0);
        Invoke("deactivate", 0.3f);
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
    public void DownB2(BaseHit HitPlayer)
    {
        damageTaker = HitPlayer;
        anim.SetTrigger("DownB2");
    }
    public void HitDB2()
    {
        if (isRight)
        {
            damageTaker.TakeAttack(23, new Vector3(0, 0.6f, 0.4f), this);
        } else
        {
            damageTaker.TakeAttack(23, new Vector3(0, 0.6f, -0.4f), this);
        }
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