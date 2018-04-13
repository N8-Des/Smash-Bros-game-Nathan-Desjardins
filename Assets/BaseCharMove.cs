using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharMove : MonoBehaviour
{
    public string A;
    public string B;
    public string X;
    public string LeftJoystickX;
    public string LeftJoystickY;
    public Rigidbody rb;
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool isIdle = false;
    public bool isWalking = false;
    public float threshold = 0.7f;
    public bool neutralX = false;
    public bool neutralY = false;
    public bool canMove = true;
    public bool canAttack = true;
    public string lastInput;
    public List<string> inputBufferList = new List<string>();
    public Vector3 moveSpeed;
    public Vector3 moveSpeedAirR;
    public Vector3 moveSpeedAirL;
    public Animator anim;
    public float BADelay;
    public float SADelay;
    public float UADelay;
    public float JDelay;
    public float FADelay;
    public float DADelay;
    public float NADelay;
    public float BSDelay;
    public float BUDelay;
    public float BBDelay;
    public float BDDelay;
    public float BARDelay;
    public float UARDelay;
    public bool onGround;
    public bool canJump;
    public bool canBUp = true;
    public bool iCanMove = false;
    public bool inAir = false;
    public BaseHit damageControl;
    public void Start()
    {
        inputBufferList.Add("ShutUpCount");
        damageControl = gameObject.GetComponent<BaseHit>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
    public void OnCollisionStay(Collision other)
    {
        if (other.collider.tag == "Ground")
        {
            canBUp = true;
            if (!onGround)
            {
                onGround = true;
            }
            if (inAir && onGround)
            {
                inAir = false;
            }


            if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleAir") || anim.GetCurrentAnimatorStateInfo(0).IsName("Dair") || anim.GetCurrentAnimatorStateInfo(0).IsName("BDown") || anim.GetCurrentAnimatorStateInfo(0).IsName("Nair"))
            {
                anim.SetTrigger("Landed");
                canJump = true;
                if (!iCanMove)
                {
                    rb.velocity = new Vector3(0, 0, 0);

                }
            }

        }
    }
    public void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Ground")
        {
            inAir = true;
            onGround = false;
        }
    }
    public virtual void BaseA() { }
    public virtual void SideA() { }
    public virtual void UpA() { }
    public virtual void DownA() { }
    public virtual void jump() { }
    public virtual void fair() { }
    public virtual void dair() { }
    public virtual void nair() { }
    public virtual void bRight() { }
    public virtual void bLeft() { }
    public virtual void bUp() { }
    public virtual void baseB() { }
    public virtual void bDown() { }
    public virtual void uAir() { }
    public virtual void bAir() { }
    public void FixedUpdate()
    {
        inputBuffer();
        attackUpdate();
        moveUpdate();
        specialUpdate();
        jumpUpdate();
    }
    void specialUpdate()
    {
        if ((Input.GetButton(B) || Input.GetKey(KeyCode.A)) && canAttack && !damageControl.isKnockedBack)
        {
            lastInput = getLastInput();
            canJump = false;
            canAttack = false;
            canMove = false;
            if (neutralX && neutralY)
            {
                anim.SetTrigger("NeutB");
                Invoke("baseB", BBDelay);
            }
            else
            {
                SpecialDir1();
            }
        }
    }
    void SpecialDir1()
    {
        if (lastInput == "Right")
        {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                anim.SetTrigger("BSide");
                Invoke("bRight", BSDelay);
        }
        else
        {
            SpecialDir2();
        }
    }
    void SpecialDir2()
    {
        if (lastInput == "Left")
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            anim.SetTrigger("BSide");
            Invoke("bLeft", BSDelay);
        }
        else
        {
            SpecialDir3();
        }
    }
    void SpecialDir3()
    {
        if (lastInput == "Up" && canBUp)
        {
            iCanMove = true;
            canBUp = false;
            anim.SetBool("BUp", true);
            Invoke("bUp", BUDelay);
        }
        else
        {
            SpecialDir4();
        }
    }
    void SpecialDir4()
    {
        if (lastInput == "Down")
        {
            anim.SetTrigger("BDown");
            Invoke("bDown", BDDelay);
        }
        else
        {
            canAttack = true;
            canMove = true;
            canJump = true;
        }
    }
    void jumpUpdate()
    {
        if ((Input.GetButton(X) || Input.GetKey(KeyCode.X)) && canJump && !damageControl.isKnockedBack)
        {
            anim.ResetTrigger("Landed");
            anim.ResetTrigger("Nair");
            anim.ResetTrigger("Fair");
            anim.ResetTrigger("Dair");
            canJump = false;
            canMove = false;
            anim.ResetTrigger("Jump");
            anim.SetTrigger("Jump");
            Invoke("jump", JDelay);
            inAir = true;
        }
    }
    void attackUpdate()
    {
        lastInput = getLastInput();
        if ((Input.GetButton(A) || Input.GetKey(KeyCode.Z)) && canAttack && onGround && !damageControl.isKnockedBack)
        {
            canJump = false;
            canAttack = false;
            canMove = false;
            if (neutralY && neutralX)
            {
                anim.SetTrigger("NeutA");
                Invoke("BaseA", BADelay);
            }
            else
            {
                attackDir1();
            }
        }
        else if ((Input.GetButton("A") || Input.GetKey(KeyCode.Z)) && canAttack && inAir)
        {
            lastInput = getLastInput();
            canAttack = false;
            canMove = false;
            if (neutralY && neutralX)
            {
                anim.SetTrigger("Nair");
                Invoke("nair", NADelay);
            }
            else
            {
                attackAirDir1();
            }
        }       
    }
    void attackAirDir1()
    {
        if (lastInput == "Right")
        {
            anim.SetTrigger("Fair");
            Invoke("fair", FADelay);
        }
        else
        {
            attackAirDir2();
        }
    }
    void attackAirDir2()
    {
        if (lastInput == "Down") {
            anim.SetTrigger("Dair");
            Invoke("dair", DADelay);
        }
        else
        {
            attackAirDir3();
        }
    }
    void attackAirDir3()
    {
        if (lastInput == "Up")
        {
            anim.SetTrigger("Uair");
            Invoke("uAir", UARDelay);
        }
        else
        {
            attackAirDir4();
        }
    }
    void attackAirDir4()
    {
        if (lastInput == "Left")
        {
            anim.SetTrigger("Bair");
            Invoke("bAir", UARDelay);
        }
        else
        {
            canAttack = true;
            canMove = true;
        }
    }
    void attackDir1()
    {
        if (lastInput == "Right")
        {
            if (transform.rotation == new Quaternion(0, -180, 0, 0))
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                anim.SetTrigger("RightA");
                Invoke("SideA", SADelay);
            }
            else
            {
                anim.SetTrigger("RightA");
                Invoke("SideA", SADelay);
            }
        }else
        {
            attackDir2();
        }
    }

    void attackDir2()
    {
        if (lastInput == "Left")
        {
            if (transform.rotation == new Quaternion(0, 0, 0, 0))
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                anim.SetTrigger("RightA");
                Invoke("SideA", SADelay);
            }
            else
            {
                anim.SetTrigger("RightA");
                Invoke("SideA", SADelay);
            }
        }
        else
        {
            attackDir3();
        }
    }
    void attackDir3()
    {
        if (lastInput == "Up")
        {
            anim.SetTrigger("UpA");
            Invoke("UpA", UADelay);
        }
        else
        {
            attackDir4();
        }
    }
    void attackDir4()
    {
        if (lastInput == "Down")
        {
            anim.SetTrigger("DownA");
            Invoke("DownA", UADelay);
        }
        else
        {
            canAttack = true;
            canMove = true;
        }
    }
    public void inputBuffer()
    {
        neutralX = false;
        neutralY = false;
        if (Input.GetAxis(LeftJoystickX) > threshold || Input.GetKey(KeyCode.RightArrow))
        {
            inputBufferList.Add("Right");
        }
        else if (Input.GetAxis(LeftJoystickX) < -threshold || Input.GetKey(KeyCode.LeftArrow))
        {
            inputBufferList.Add("Left");
        }
        else if (Input.GetAxis(LeftJoystickY) > threshold || Input.GetKey(KeyCode.DownArrow))
        {
            inputBufferList.Add("Down");
        }
        else if (Input.GetAxis(LeftJoystickY) < -threshold || Input.GetKey(KeyCode.UpArrow))
        {
            inputBufferList.Add("Up");
        }
        else
        {
            inputBufferList.Add("Neutral");
            neutralY = true;
            neutralX = true;
        }
    }
    public string getLastInput()
    {
        int amount = inputBufferList.Count;
        inputBufferList.RemoveRange(0, (amount - 8));
        return inputBufferList[0];
    }
    void moveUpdate()
    {
        if (!iCanMove && !damageControl.isKnockedBack)
        {
            if (isWalking && inAir && neutralX)
            {
                isWalking = false;
                moveLeft = false;
                moveRight = false;
            }
            if (onGround)
            {
                if ((isWalking && neutralX || (!canMove)) && !inAir)
                {
                    isWalking = false;
                    moveLeft = false;
                    moveRight = false;
                    isIdle = true;
                    //rb.velocity = new Vector3(0, 0, 0);
                }
                if (canMove)
                {
                    canJump = true;
                    if (lastInput == "Right")
                    {
                        moveLeft = false;
                        moveRight = true;
                        transform.rotation = new Quaternion(0, 0, 0, 0);
                        rb.velocity = moveSpeed;
                        isWalking = true;
                        isIdle = false;
                    }
                    else if (lastInput == "Left")
                    {
                        moveRight = false;
                        moveLeft = true;
                        transform.rotation = new Quaternion(0, 180, 0, 0);
                        rb.velocity = -moveSpeed;
                        isWalking = true;
                        isIdle = false;
                    }
                }
            }
            else if (canMove)
            {
                if (lastInput == "Right")
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    rb.velocity = moveSpeedAirR;
                    isWalking = true;
                }
                if (lastInput == "Left")
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                    rb.velocity = moveSpeedAirL;
                    isWalking = true;
                }
            }
        }
    }
}
