using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrarianMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    int timesSpawned = 0;
    public GameObject wings;
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
    public void BDown()
    {
        StartCoroutine(spawnArrow());
    }
    public IEnumerator spawnArrow()
    {
        GameObject Arrow = GameObject.Instantiate((GameObject)Resources.Load("TerrariaArrow"));
        timesSpawned += 1;
        Rigidbody arrowRB = Arrow.GetComponent<Rigidbody>();
        Vector3 offset = transform.position + new Vector3(0, 3, Random.Range(-0.5f, 0.5f));
        Arrow.transform.position = offset;
        arrowRB.velocity = Vector3.up * -5;
        FriendlyHitbox friendly = Arrow.GetComponentInChildren<FriendlyHitbox>();
        friendly.friend = gameObject;
        yield return new WaitForSeconds(0.1f);
        if (timesSpawned >= 4)
        {
            //end everything
        }
        else
        {
            StartCoroutine(spawnArrow());
        }
    }
    public void baseStop()
    {
        timesSpawned = 0;
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
    public void ShootRocket()
    {
        isShooting = true;
        GameObject Present = GameObject.Instantiate((GameObject)Resources.Load("VortexShot"));
        PresentRB = Present.GetComponent<Rigidbody>();
        if (!isRight)
        {
            BasicHurtbox vortexHitbox = Present.GetComponentInChildren<BasicHurtbox>();
            vortexHitbox.isRight = false;
            Present.transform.rotation = new Quaternion(0, 180, 0, 0);
            Present.transform.position = transform.position + new Vector3(0, 0.05f, -0.6f);
            PresentRB.velocity = new Vector3(0, 0, -5f);
        }
        else
        {
            Present.transform.position = transform.position + new Vector3(0, 0.05f, 0.6f);
            PresentRB.velocity = new Vector3(0, 0, 5f);
        }
    }

}