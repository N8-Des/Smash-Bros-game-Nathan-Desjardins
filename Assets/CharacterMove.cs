using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public string A;
    public string B;
    public string X;
    public string LeftJoystickX;
    public string LeftJoystickY;
    public string LeftBumper = "LB";
    public string RightJoystickDown = "RightPress";
    public bool isJumping = false;
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
    public float JDelay = 0.01f;
    public bool onGround;
    public bool canJump;
    public bool canBUp = true;
    public bool iCanMove = false;
    public bool inAir = false;
    public BaseHit damageControl;
    public bool isRight = true;
    public string deathNoise;
    public bool isLedged = false;
    public Vector3 ledgeOffset;
    public bool isCountering = false;
    public bool blocking = false;
    public bool canBlock = true;
    public bool ultReady = false;
    public ProgressManager progMan;
    public bool charging;
    public bool isWet = false;
    public void getWet()
    {
        isWet = true;
        CancelInvoke("dryOff");
        Invoke("dryOff", 6);
    }
    public void dryOff()
    {
        isWet = false;
    }
    public void OnEnable()
    {
        inputBufferList.Add("ShutUpCount");
        damageControl = gameObject.GetComponent<BaseHit>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
    public void death(bool edgeDeath)
    {
        BaseHit pdis = gameObject.GetComponent<BaseHit>();
        pdis.resetPerc();
        GameObject sound = GameObject.Instantiate((GameObject)Resources.Load(deathNoise));
        Destroy(gameObject);
    }
    public virtual void OnCollisionStay(Collision other)
    {
        if (other.collider.tag == "Ground")
        {
            if (!onGround)
            {
                onGround = true;
                anim.SetBool("InAir", false);
            }
            if (inAir && onGround)
            {
                inAir = false;
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleAir") || anim.GetCurrentAnimatorStateInfo(0).IsName("Dair") || anim.GetCurrentAnimatorStateInfo(0).IsName("BDown") || anim.GetCurrentAnimatorStateInfo(0).IsName("Nair") || anim.GetCurrentAnimatorStateInfo(0).IsName("Fair") || anim.GetCurrentAnimatorStateInfo(0).IsName("Bair") || anim.GetCurrentAnimatorStateInfo(0).IsName("Uair"))
            {
                canJump = true;
                anim.SetBool("isAttacking", false);
                anim.SetBool("IsIdle", true);
                canAttack = true;
                canMove = true;
                if (!iCanMove)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                }
            }
        }
        else if (other.collider.tag == "EdgeGrab")
        {
            isLedged = true;
            anim.SetBool("Ledge", true);
            transform.position = other.transform.position + ledgeOffset;
            rb.velocity = new Vector3(0, 0, 0);
            rb.useGravity = false;
        }
    }
    public virtual void OnCollisionEnter(Collision other)
    {
        if (!canBUp && other.collider.tag == "Ground")
        {
            canBUp = true;
        }
    }
    public void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Ground")
        {
            inAir = true;
            anim.SetBool("InAir", true);
            onGround = false;
        }
    }
    public virtual void bRight() { }
    public virtual void bLeft() { }
    public virtual void bUp() { }
    public virtual void baseB() { }
    public virtual void bDown() { }
    public virtual void jump() { }
    public virtual void attacking() { }
    public virtual void counter(int damage) { }
    public virtual void bSideGrab(GameObject playerHit) { }
    public void FixedUpdate()
    {
        inputBuffer();
        jumpUpdate();
        attackUpdate();
        moveUpdate();
        specialUpdate();
        shieldCheck();
        ultCheck();
    }
    public virtual void takeStun(float stunTime)
    {
        canMove = false;
        canAttack = false;
        canJump = false;
        anim.SetBool("CanAttack", false);
        Invoke("endStun", stunTime);
    }
    void endStun()
    {
        canMove = true;
        canAttack = true;
        canJump = true;
        anim.SetBool("IsIdle", true);
        anim.SetBool("CanAttack", true);

    }
    public void ultCheck()
    {
        if (ultReady && Input.GetButton(RightJoystickDown) && canAttack)
        {
            anim.SetTrigger("ult");
            canAttack = false;
            canMove = false;
            canJump = false;
            attacking();
            ultReady = false;
            //ULTIMATE ATTACK!
            progMan.reset();
        }
    }
    public void StopEveryThing()
    {
        rb.velocity = new Vector3(0, 0, 0);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isIdle", true);
        anim.SetBool("knockedBack", true);

    }

    public void shieldCheck()
    {
        if (!inAir && !damageControl.isKnockedBack) {
            if (Input.GetButton(LeftBumper) && canBlock && !anim.GetBool("isAttacking"))
            {
                blocking = true;
                damageControl.isBlocking = true;
                anim.SetBool("block", true);
                canJump = false;
                canAttack = false;
                canMove = false;

            } else if (blocking)
            {
                blocking = false;
                anim.SetBool("block", false);
                damageControl.isBlocking = false;
                canJump = true;
                canAttack = true;
                canMove = true;
            }
        }
    }
    void specialUpdate()
    {
        if ((Input.GetButton(B) || Input.GetKey(KeyCode.A)) && canAttack && !damageControl.isKnockedBack && !charging)
        {
            lastInput = getLastInput();
            if (neutralX && neutralY && !inAir)
            {
                canJump = false;
                canAttack = false;
                canMove = false;
                anim.SetTrigger("NeutB");
                attacking();
            }
            else
            {
                SpecialDir1();
            }
        }
    }
    public virtual void SpecialDir1()
    {
        if (lastInput == "Right" && !inAir)
        {
            canJump = false;
            canAttack = false;
            canMove = false;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            isRight = true;
            anim.SetTrigger("BSide");
            attacking();

        }
        else
        {
            SpecialDir2();
        }
    }
    public virtual void SpecialDir2()
    {
        if (lastInput == "Left" && !inAir)
        {
            canJump = false;
            canAttack = false;
            canMove = false;
            isRight = false;
            transform.rotation = new Quaternion(0, 180, 0, 0);
            anim.SetTrigger("BSide");
            attacking();
        }
        else
        {
            SpecialDir3();
        }
    }
    public virtual void SpecialDir3()
    {
        if (lastInput == "Up" && canBUp)
        {
            canJump = false;
            canAttack = false;
            canMove = false;
            iCanMove = true;
            canBUp = false;
            anim.SetTrigger("BUp");
            attacking();

        }
        else
        {
            SpecialDir4();
        }
    }
    public virtual void SpecialDir4()
    {
        if (lastInput == "Down" && !inAir)
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
            canJump = true;
            anim.SetBool("IsIdle", true);
            anim.SetBool("CanAttack", true);
        }
    }
    void jumpUpdate()
    {
        if ((Input.GetButton(X) || Input.GetKey(KeyCode.X)) && canJump && !damageControl.isKnockedBack && !isJumping)
        {
            isJumping = true;
            iCanMove = true;
            canJump = false;
            canMove = false;
            anim.SetBool("IsIdle", false);
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
            anim.SetBool("IsIdle", false);
            if (neutralY && neutralX)
            {
                anim.SetTrigger("NeutA");
                attacking();
            }
            else
            {
                attackDir1();
            }
        }
        else if ((Input.GetButton(A) || Input.GetKey(KeyCode.Z)) && canAttack && inAir)
        {
            lastInput = getLastInput();
            canAttack = false;
            canMove = false;
            anim.SetBool("IsIdle", false);
            if (neutralY && neutralX)
            {
                anim.SetTrigger("Nair");
                attacking();
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
            attacking();
        }
        else
        {
            attackAirDir2();
        }
    }
    void attackAirDir2()
    {
        if (lastInput == "Down")
        {
            anim.SetTrigger("Dair");
            attacking();
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
            attacking();
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
            attacking();
        }
        else
        {
            canAttack = true;
            canMove = true;
            anim.SetBool("CanAttack", true);
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
            }
            else
            {
                anim.SetTrigger("RightA");
                attacking();
            }
        }
        else
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
                attacking();
            }
            else
            {
                anim.SetTrigger("RightA");
                attacking();
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
            attacking();
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
            attacking();
        }
        else
        {
            canAttack = true;
            canMove = true;
            anim.SetBool("CanAttack", true);
            anim.SetBool("IsIdle", true);
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
        if (!iCanMove && !damageControl.isKnockedBack && !isJumping && !isLedged)
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
                    rb.velocity = new Vector3(0, 0, 0);
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
                        isRight = true;
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
                        isRight = false;
                        isIdle = false;
                    }
                }
            }
            else if (canMove)
            {
                if (lastInput == "Right")
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    if (rb.velocity.z <= 1)
                    {
                        rb.AddForce(0, 0, 5000);
                    }
                    isWalking = true;
                }
                if (lastInput == "Left")
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                    if (rb.velocity.z >= -1)
                    {
                        rb.AddForce(0, 0, -5000);
                    }
                    isWalking = true;
                }
            }
        }
        else if (isLedged)
        {
            if (lastInput == "Left")
            {
                transform.position -= new Vector3(0, 0.4f, 0.2f);
                anim.SetBool("Ledge", false);
                transform.rotation = new Quaternion(0, 0, 0, 0);
                rb.useGravity = true;

            }
            else if (lastInput == "Right")
            {
                transform.position += new Vector3(0, 0.4f, 0.2f);
                anim.SetBool("Ledge", false);
                transform.rotation = new Quaternion(0, 180, 0, 0);
                rb.useGravity = true;
            }
        }
    }
}
