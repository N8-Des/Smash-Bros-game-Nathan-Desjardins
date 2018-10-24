using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    public GameObject bubbleSpot;
    public GameObject bubble;
    private int numBubbles = 0;
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
        anim.SetBool("Jumping", false);
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
    public IEnumerator BubbleShot()
    {
        if (numBubbles < 4)
        {
            GameObject bubble = GameObject.Instantiate((GameObject)Resources.Load("ShrimpBubbleShot"));
            float randScale = Random.Range(0.08f, 0.23f);
            FriendlyHitbox friendHB = bubble.GetComponentInChildren<FriendlyHitbox>();
            friendHB.friend = gameObject;
            if (!isRight)
            {
                bubble.transform.rotation = new Quaternion(0, 180, 0, 0);
                bubble.transform.localScale = new Vector3(0.0003506864f, randScale, randScale);
                bubble.transform.position = transform.position + new Vector3(0, Random.Range(0.10f, 0.30f), Random.Range(-0.70f, -0.50f));
            }
            else
            {
                bubble.transform.position = transform.position + new Vector3(0, Random.Range(0.10f, 0.30f), Random.Range(0.50f, 0.70f));
                bubble.transform.localScale = new Vector3(0.0003506864f, randScale, randScale);
            }
            yield return new WaitForSeconds(0.1f);
            numBubbles += 1;
            StartCoroutine(BubbleShot());
        }
        else
        {
            numBubbles = 0;
        }
    }
    public void spawnBubbleUB()
    {
        GameObject BubbleUB = Instantiate(bubble);
        BubbleUB.transform.position = bubbleSpot.transform.position;
        FriendlyHitbox fhb = BubbleUB.GetComponent<FriendlyHitbox>();
        fhb.friend = this.gameObject;
    }
}