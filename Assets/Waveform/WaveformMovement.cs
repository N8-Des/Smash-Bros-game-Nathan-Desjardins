using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveformMovement : CharacterMove
{
    public Rigidbody me;
    public Rigidbody PresentRB;
    public AudioSource audioStrike;
    public ParticleSystem part;
    public bool isShooting = false;
    public bool hasCloud;
    public GameObject cloud;
    public Cloud ThunCloud;
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
    public override void SpecialDir4()
    {
        if (lastInput == "Down")
        {
            canJump = false;
            canAttack = false;
            canMove = false;
            anim.SetTrigger("BDown");
            attacking();
        }
        else
        {
            canAttack = true;
            canMove = true;
            anim.SetBool("IsIdle", true);
            anim.SetBool("CanAttack", true);
            if (!inAir)
            {
                canJump = true;

            }
        }
    }
    public void deactivate()
    {
        iCanMove = false;
        me.velocity = new Vector3(0, 0, 0);
        canMove = true;
        canAttack = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("CanAttack", true);
        anim.SetBool("IsIdle", true);
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
    }

    public override void bUp()
    {
        transform.position += new Vector3(0, 4, 0);
    }
    public override void baseB()
    {
        Invoke("baseStop", 3);
    }
    public override void bDown()
    {
        if (!hasCloud)
        {
            hasCloud = true;
            cloud = GameObject.Instantiate((GameObject)Resources.Load("Thundercloud"));
            cloud.transform.position = transform.position + new Vector3(0, 3, 0);
            ThunCloud = cloud.GetComponent<Cloud>();
            ThunCloud.friendly = this;
        }
        else
        {
            ThunCloud.SendThunder();
            hasCloud = false;
            //send thunder, despawn cloud, turn off has cloud. 
        }
    }
    public void ThrowSpear()
    {
        GameObject Spear = GameObject.Instantiate((GameObject)Resources.Load("ElectroSpear"));
        PresentRB = Spear.GetComponent<Rigidbody>();
        SpearTargetting sptar = Spear.GetComponent<SpearTargetting>();
        //audioStrike.Play();
        if (!isRight)
        {
            Spear.transform.rotation = new Quaternion(0, 180, 0, 0);
            Spear.transform.position = transform.position + new Vector3(0, 0.5f, -0.6f);
            PresentRB.velocity = new Vector3(0, 0, -5f);
            sptar.unhomeSpeed = new Vector3(0, 0, -5f);
            sptar.friend = this;
            //Invoke("baseStop", 0.6f);
        }
        else
        {
            Spear.transform.position = transform.position + new Vector3(0, 0.5f, 0.6f);
            PresentRB.velocity = new Vector3(0, 0, 5f);
            sptar.unhomeSpeed = new Vector3(0, 0, 5f);
            sptar.friend = this;
            //Invoke("baseStop", 0.6f);
        }
        //anim.ResetTrigger("NeutB");      
    }

}