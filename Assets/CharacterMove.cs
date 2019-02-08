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
    public string RightJoystickX = "RightJoystickX";
    public string RightJoystickY = "RightJoystickY";
    public string LeftBumper = "LB";
    public string RightBumper = "RB";
    public string RightJoystickDown = "RightPress";
    public string DPadX = "DPadX";
    public int JumpHeight = 6500;
    public float MaxSpeedGround;
    public int AccelerationGround;
    public float MaxSpeedAir;
    public int AccelerationAir;
    public bool isJumping = false;
    public Rigidbody rb;
    protected int jumpsLeft = 2;
    public GameObject grabPosition;
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool isIdle = false;
    public bool isWalking = false;
    public float threshold = 0.7f;
    public int maxJumps = 2;
    public bool neutralX = false;
    public bool neutralY = false;
    public bool neutralXRight = false;
    public bool neutralYRight = false;
    public bool canMove = true;
    public bool canAttack = true;
    public string lastInput;
    public string lastInputRightStick;
    public List<string> inputBufferList = new List<string>();
    public List<string> inputBufferListRight = new List<string>();
    public Vector3 moveSpeed;
    public Vector3 moveSpeedAirR;
    public Vector3 moveSpeedAirL;
    public Animator anim;
    public float JDelay = 0.01f;
    public bool onGround;
    public bool canJump;
    public bool canBUp = true;
    public bool usingGravity = false;
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
    public int numSmash;
    public bool isWet = false;
    public bool ultsOn = true;
    public bool canAirdodge = true;
    public BasicHurtbox FSmashHitbox;
    public BasicHurtbox USmashHitbox;
    public BasicHurtbox DSmashHitbox;
    public GameObject ledgePlacement;
    public bool isGrabbing;
    int layerMask = 1 << 8;
    public bool canSmash = true;
    public Vector3 pullLocation;
    bool maxVelocity = false;
    GameObject enemyGrabLocation;
    public CharacterMove grabbedPlayer;
    bool isGrabbed;
    int throwDirection;
    bool canPummel = true;
    bool negativeThrow;
    ThrowConstants throwConstants;
    bool isThrowing;
    bool grabberRight;
    bool isStunned;
    bool isDashing;
    int baseDamage;
    public Vector3 GrabOffset = new Vector3(0, 0, 0);

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
    public virtual void jump()
    {
        if (jumpsLeft >= 1 && canJump)
        {
            if (inAir)
            {
                rb.velocity = Vector3.zero;
            }
            rb.useGravity = false;
            if (jumpsLeft == 2)
            {
                canMove = true;
                //canAttack = false;
                anim.SetBool("Jumping", true);
                anim.SetTrigger("Jump");
                rb.AddForce(0, JumpHeight, 0);
                jumpsLeft -= 1;
                canJump = false;
                Invoke("jDelay", 0.4f);
                Invoke("stopJump", 0.4f);
            }
            else if (jumpsLeft == 1)
            {
                canMove = true;
                isJumping = true;
                onGround = false;
                anim.SetBool("Jumping", true);
                anim.SetTrigger("Jump2");
                jumpsLeft -= 1;
                rb.AddForce(0, JumpHeight, 0);
                Invoke("stopJump", 0.4f);
            }
        }
    }
    void jDelay()
    {
        canJump = true;
    }
    public void stopJump()
    {
        rb.useGravity = true;
        isJumping = false;
        iCanMove = false;
        //rb.velocity = new Vector3(0, 0, 0);
        anim.SetBool("Jumping", false);
    }
    public void OnEnable()
    {
        inputBufferList.Add("ShutUpCount");
        damageControl = gameObject.GetComponent<BaseHit>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        throwConstants = gameObject.GetComponent<ThrowConstants>();
        threshold = 0.1f;
        jumpsLeft = maxJumps;
    }
    public void death(bool edgeDeath)
    {
        BaseHit pdis = gameObject.GetComponent<BaseHit>();
        pdis.resetPerc();
        if (edgeDeath)
        {
            GameObject sound = GameObject.Instantiate((GameObject)Resources.Load(deathNoise));
        }
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
            //Hey, uh, don't worry about this part, code reviewer. I was but a young lad. And if it ain't broke, don't fix it, or you'll just make more bugs.
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleAir") || anim.GetCurrentAnimatorStateInfo(0).IsName("Dair") || anim.GetCurrentAnimatorStateInfo(0).IsName("BDown") || 
                anim.GetCurrentAnimatorStateInfo(0).IsName("Nair") || anim.GetCurrentAnimatorStateInfo(0).IsName("Fair") || anim.GetCurrentAnimatorStateInfo(0).IsName("Bair") || 
                anim.GetCurrentAnimatorStateInfo(0).IsName("Uair") || anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") || anim.GetCurrentAnimatorStateInfo(0).IsName("Jump2"))
            {
                isJumping = false;
                canJump = true;
                stopJump();
                jumpsLeft = maxJumps;
                anim.SetBool("isAttacking", false);
                anim.SetBool("IsIdle", true);
                canAttack = true;
                canMove = true;
                if (!iCanMove)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                }
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Airdodge"))
            {
                endAirdodge();
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
        if (other.collider.tag == "Ground")
        {
            anim.SetBool("isWalking", false);
        }
        if (!canBUp && other.collider.tag == "Ground")
        {
            canBUp = true;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("UpB"))
        {
            canAttack = true;
            canMove = true;
            iCanMove = false;
            anim.SetBool("CanAttack", true);
            anim.SetBool("isAttacking", false);
            anim.SetBool("IsIdle", true);
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
    public void LeaveGround() //only used by Montezuma's dissapearing platforms as of now
    {
        inAir = true;
        anim.SetBool("InAir", true);
        onGround = false;
    }
    public virtual void EndSpawnAnim()
    {
        anim.SetBool("isAttacking", false);
    }
    public void Spawn()
    {
        anim.SetBool("isAttacking", true);
        anim.SetTrigger("Spawn");
    }
    public virtual void bRight() { }
    public virtual void bLeft() { }
    public virtual void bUp() { }
    public virtual void baseB() { }
    public virtual void bDown() { }
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
        tauntInput();
        smashUpdate();
        grabCheck();
        if (ultsOn)
        {
            ultCheck();
        }
    }
    public virtual void TurnLeft()
    {
        isRight = false;
        transform.rotation = new Quaternion(0, 180, 0, 0);
    }
    public virtual void TurnRight()
    {
        isRight = true;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    public void SendToIdle()
    {
        anim.SetBool("IsIdle", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("knockedBack", true);
    }
    public virtual void takeStun(float stunTime)
    {
        if (stunTime > 0 && !isStunned)
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("CanAttack", true);
            canMove = false;
            canAttack = false;
            canJump = false;
            isStunned = true;
            canBlock = false;
            anim.SetBool("CanAttack", false);
            anim.SetBool("isStunned", true);
            Invoke("endStun", stunTime);
        }
    }
    public virtual void tauntInput()
    {
        if ((Input.GetAxis(DPadX) > threshold || Input.GetAxis(DPadX) < -threshold) && canAttack && !damageControl.isKnockedBack && !charging)
        {
            //taunts go here. :(
        }
    }
    void endStun()
    {
        canMove = true;
        canAttack = true;
        canJump = true;
        isStunned = false;
        canBlock = true;
        anim.SetBool("IsIdle", true);
        anim.SetBool("CanAttack", true);
        anim.SetBool("isStunned", false);
    }
    void gravCheck()
    {
        if (rb.velocity.y <= -2.5 )
        {
            rb.AddForce(0, 2500, 0);
        }
    }
    public virtual void stopMoving() //used by GameManager to stop moving before the round starts
    {
        canAttack = false;
        canMove = false;
        canJump = false;
        canBlock = false;
    }
    public virtual void gotCountered()
    {
        canAttack = false;
        canMove = false;
        canJump = false;
        canBlock = false;
        anim.SetBool("isAttacking", false);
        anim.SetBool("isIdle", true);
        anim.SetBool("canAttack", false);
    }
    public virtual void startMoving() //used by GameManager to start moving at start of match
    {
        canAttack = true;
        canMove = true;
        canJump = true;
        canBlock = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("isIdle", true);

    }
    public void ultCheck()
    {
        if (ultReady && (Input.GetButton(RightJoystickDown) || Input.GetKey(KeyCode.Q)) && canAttack)
        {
            anim.SetTrigger("ult");
            canAttack = false;
            canMove = false;
            canJump = false;
            attacking();
            ultReady = false;
            progMan.reset();
        }
    }
    public void endDash()
    {
        canAttack = true;
        canMove = true;
        isDashing = false;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
        anim.SetBool("Jumping", false);

    }
    public void StopEveryThing()
    {
        rb.velocity = new Vector3(0, 0, 0);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isIdle", true);
        anim.SetBool("knockedBack", true);
    }
    public void stopVelocity()
    {
        rb.velocity = new Vector3(0, 0, 0);
    }
    public void endAirdodge()
    {
        canAirdodge = true;
        canAttack = true;
        canMove = true;
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
        anim.SetBool("block", false);
    }
    public void grabCheck()
    {
        if (Input.GetButton(RightBumper) && canAttack && !isGrabbing && !inAir)
        {
            rb.velocity = Vector3.zero;
            attacking();
            anim.SetTrigger("Grab");
            canAttack = false;
            canMove = false;
            canJump = false;
        }
        if (isGrabbed)
        {
            transform.position = enemyGrabLocation.transform.position + GrabOffset;
            transform.rotation = enemyGrabLocation.transform.rotation;
        }else if (isGrabbing)
        {
            if (Input.GetButton(RightBumper) && canPummel && !isThrowing)
            {
                anim.SetTrigger("Pummel");
                anim.SetBool("pummeling", true);
                anim.SetBool("isAttacking", true);
                canPummel = false;
            } else if (canPummel && !isThrowing)
            {
                if (!isRight && !negativeThrow)
                {
                    negativeThrow = true;
                    throwConstants.uThrowLaunch.z *= -1;
                    throwConstants.dThrowLaunch.z *= -1;
                    throwConstants.fThrowLaunch.z *= -1;
                    throwConstants.bThrowLaunch.z *= -1;
                } else if (isRight && negativeThrow)
                {
                    negativeThrow = false;
                    throwConstants.uThrowLaunch.z *= -1;
                    throwConstants.dThrowLaunch.z *= -1;
                    throwConstants.fThrowLaunch.z *= -1;
                    throwConstants.bThrowLaunch.z *= -1;
                }
                if (lastInput == "Up")
                {
                    anim.SetBool("isAttacking", true);
                    throwDirection = 0;
                    anim.SetTrigger("UpThrow");
                    anim.SetBool("pummeling", true);
                    isThrowing = true;
                }
                else if (lastInput == "Down")
                {
                    anim.SetBool("isAttacking", true);
                    throwDirection = 1;
                    anim.SetTrigger("DownThrow");
                    anim.SetBool("pummeling", true);
                    isThrowing = true;
                }
                else if (lastInput == "Right")
                {
                    if (isRight)
                    {
                        anim.SetBool("isAttacking", true);
                        throwDirection = 2;
                        anim.SetTrigger("ForwardThrow");
                        anim.SetBool("pummeling", true);
                        isThrowing = true;
                    }
                    else
                    {
                        anim.SetBool("isAttacking", true);
                        throwDirection = 3;
                        anim.SetTrigger("BackThrow");
                        anim.SetBool("pummeling", true);
                        isThrowing = true;
                    }
                } else if (lastInput == "Left")
                {
                    if (!isRight)
                    {
                        anim.SetBool("isAttacking", true);
                        throwDirection = 2;
                        anim.SetTrigger("ForwardThrow");
                        anim.SetBool("pummeling", true);
                        isThrowing = true;
                    }
                    else
                    {
                        anim.SetBool("isAttacking", true);
                        throwDirection = 3;
                        anim.SetTrigger("BackThrow");
                        anim.SetBool("pummeling", true);
                        isThrowing = true;
                    }
                }
            }
        }
    }
    public void grabDamage(int damage)
    {
        grabbedPlayer.GetComponent<BaseHit>().takeDamage(damage);
    }
    public void endThrow()
    {
        anim.SetBool("pummeling", false);    
        grabbedPlayer.GetComponent<Rigidbody>().useGravity = true;
        grabbedPlayer.isGrabbed = false;
        grabbedPlayer.GetComponent<Collider>().isTrigger = true;

        switch (throwDirection)
        {
            case 0:
                grabbedPlayer.GetComponent<BaseHit>().TakeAttack(throwConstants.uThrowDamage, throwConstants.uThrowLaunch, this);
                break;
            case 1:
                grabbedPlayer.GetComponent<BaseHit>().TakeAttack(throwConstants.dThrowDamage, throwConstants.dThrowLaunch, this);
                break;
            case 2:
                grabbedPlayer.GetComponent<BaseHit>().TakeAttack(throwConstants.fThrowDamage, throwConstants.fThrowLaunch, this);
                break;
            case 3:
                grabbedPlayer.GetComponent<BaseHit>().TakeAttack(throwConstants.bThrowDamage, throwConstants.bThrowLaunch, this);
                break;           
        }
        if (isRight)
        {
            if (throwDirection != 3)
            {
                grabbedPlayer.TurnLeft();
            }
            else
            {
                grabbedPlayer.TurnRight();
            } 
        }
        else
        {
            if (throwDirection != 3)
            {
                grabbedPlayer.TurnRight();
            }
            else
            {
                grabbedPlayer.TurnLeft();
            }
        }
        grabbedPlayer.anim.SetBool("IsGrabbed", false);
        isGrabbing = false;
        anim.SetBool("GrabIdle", false);
        isThrowing = false;
        grabbedPlayer.GetComponent<Collider>().isTrigger = false;
    }
    public void damageNoise1()
    {
        GameObject sound = GameObject.Instantiate((GameObject)Resources.Load("Audh1"));
    }
    public void damageNoise2()
    {
        GameObject sound = GameObject.Instantiate((GameObject)Resources.Load("Audh2"));
    }
    public void damageNoise3()
    {
        GameObject sound = GameObject.Instantiate((GameObject)Resources.Load("Audh3"));
    }
    public void endPummel()
    {
        canPummel = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("pummeling", true);
        grabbedPlayer.GetComponent<BaseHit>().takeDamage(2);
    }
    public void grabbed(CharacterMove attackingPlayer, bool right)
    {
        grabberRight = right;
        canMove = false;
        canAttack = false;
        canJump = false;
        isGrabbed = true;
        rb.useGravity = false;
        enemyGrabLocation = attackingPlayer.grabPosition;
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsGrabbed", true);
    }
    public void shieldCheck()
    {
        if (!inAir)
        {
            if (!damageControl.isKnockedBack)
            {
                if (Input.GetButton(LeftBumper) && canBlock && !anim.GetBool("isAttacking"))
                {
                    blocking = true;
                    damageControl.isBlocking = true;
                    anim.SetBool("block", true);
                    canJump = false;
                    canAttack = false;
                    canMove = false;

                }
                else if (blocking)
                {
                    blocking = false;
                    anim.SetBool("block", false);
                    damageControl.isBlocking = false;
                    canJump = true;
                    canAttack = true;
                    canMove = true;
                }
            }
        }else if (!damageControl.isKnockedBack)
        {
            if (Input.GetButton(LeftBumper) && !anim.GetBool("isAttacking") && canAirdodge)
            {
                anim.SetBool("block", true);
                anim.SetBool("IsIdle", false);
                anim.SetTrigger("airdodge");
                canAirdodge = false;
                canJump = false;
                canAttack = false;
                canMove = false;
            }
        }
    }
    void smashUpdate()
    {
        inputBufferRight();
        if ((!neutralXRight || !neutralYRight) && canSmash && !inAir)
        {
            lastInputRightStick = getLastInputRight();
            canJump = false;
            canAttack = false;
            canMove = false;
            canSmash = false;
            attacking();
            smashDirection1();
        }
    }
    public virtual void smashDirection1()
    {
        if (lastInputRightStick == "Right")
        {
            rb.velocity = Vector3.zero;
            if (!isRight)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            anim.SetTrigger("FSmashCharge");
            numSmash = 0;
        }
        else
        {
            smashDirection2();    
        }
    }
    public virtual void smashDirection2()
    {
        if (lastInputRightStick == "Left")
        {
            rb.velocity = Vector3.zero;
            if (isRight)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            anim.SetTrigger("FSmashCharge");
            numSmash = 0;
        }
        else
        {
            smashDirection3();
        }
    }
    public virtual void smashDirection3()
    {
        if (lastInputRightStick == "Up")
        {
            rb.velocity = Vector3.zero;
            anim.SetTrigger("USmashCharge");
            numSmash = 1;
        }
        else
        {
            smashDirection4();
        }
    }
    public virtual void smashDirection4()
    {
        if (lastInputRightStick == "Down")
        {
            rb.velocity = Vector3.zero;
            anim.SetTrigger("DSmashCharge");
            numSmash = 2;
        }
        else
        {
            canAttack = true;
            canMove = true;
            anim.SetBool("IsIdle", true);
            anim.SetBool("CanAttack", true);
            canSmash = true;
            if (!inAir)
            {
                canJump = true;

            }
        }
    }
    public void startSmashCharge()
    {
        StartCoroutine(smashCharge());
    }
    public IEnumerator smashCharge()
    {
        int framesCharged = 0;
        float extraDamage = 0;
        while (!neutralXRight && !neutralYRight && framesCharged <= 200)
        {
            extraDamage += 0.1f;
            framesCharged += 1;
            yield return new WaitForEndOfFrame();
        }
        anim.ResetTrigger("FSmashCharge");
        anim.ResetTrigger("DSmashCharge");
        anim.ResetTrigger("USmashCharge");

        framesCharged = 0;
        switch (numSmash)
        {
            case 0:
                baseDamage = FSmashHitbox.damage;
                FSmashHitbox.damage += (int)(extraDamage);
                anim.SetTrigger("FSmash");
                break;
            case 1:
                baseDamage = USmashHitbox.damage;
                USmashHitbox.damage += (int)(extraDamage);
                anim.SetTrigger("USmash");
                break;
            case 2:
                baseDamage = DSmashHitbox.damage;
                DSmashHitbox.damage += (int)(extraDamage);
                anim.SetTrigger("DSmash");
                break;
        }
        extraDamage = 0;
    }
    public void smashStop()
    {
        canSmash = true;
        canAttack = true;
        canMove = true;
        switch (numSmash)
        {
            case 0:
                FSmashHitbox.damage = baseDamage;
                break;
            case 1:
                USmashHitbox.damage = baseDamage;
                break;
            case 2:
                DSmashHitbox.damage = baseDamage;
                break;
        }
        anim.SetBool("CanAttack", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("IsIdle", true);
        anim.SetBool("Jumping", false);

    }
    public void grabLedge(Vector3 ledgePos, bool isRightSide)
    {
        RaycastHit hit;
        if (inAir)
        {
            if (Physics.Raycast(transform.position - new Vector3(0, 1, 0), transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity, layerMask))
            {

            }
            else
            {
                if (isRightSide)
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                    isRight = true;
                }
                else
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    isRight = false;
                }
                Vector3 offset = ledgePos - ledgePlacement.transform.position;
                transform.localPosition += offset;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                isLedged = true;
                jumpsLeft = maxJumps;
                CancelInvoke();
                anim.SetBool("LedgeGrab", true);
                anim.SetBool("KnockedBack", false);
                canJump = false;
                canAttack = false;
                Invoke("jDelay", 0.2f);
            }
        }
    }
    void specialUpdate()
    {
        if ((Input.GetButton(B) || Input.GetKey(KeyCode.A)) && canAttack && !damageControl.isKnockedBack && !charging)
        {
            if (!inAir && !isJumping)
            {
                stopVelocity();
            }
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
            isRight = true;
            canJump = false;
            canAttack = false;
            canMove = false;
            transform.rotation = new Quaternion(0, 0, 0, 0);
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
            isRight = false;
            canJump = false;
            canAttack = false;
            canMove = false;
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
            anim.SetBool("IsIdle", true);
            anim.SetBool("CanAttack", true);
            if (!inAir)
            {
                canJump = true;

            }
        }
    }
    void jumpUpdate()
    {
        if ((Input.GetButton(X) || Input.GetKeyDown(KeyCode.X)) && canJump && !damageControl.isKnockedBack)
        {
            jump();
            isJumping = true;
            //canMove = false;
            anim.SetBool("IsIdle", false);
            anim.SetBool("LedgeGrab", false);
            isLedged = false;
            inAir = true;
            onGround = false;
        }
    }
    public void endVelocityEdge()
    {
        rb.velocity = Vector3.zero;
        canMove = false;
        canAttack = false;
        Invoke("canMoveAgain", 0.15f);
    }
    void canMoveAgain()
    {
        canMove = true;
        canAttack = true;
    }
    void endMovementWalk()
    {
        if (!isWalking && !isJumping && !iCanMove && !damageControl.isKnockedBack)
        {
            rb.velocity = Vector3.zero;
        }
    }
    public void damageNoiseAny(int numDam)
    {
        GameObject sound = GameObject.Instantiate((GameObject)Resources.Load("Audh" + numDam));
    }
    #region attack update
    void attackUpdate()
    {
        lastInput = getLastInput();
        if ((Input.GetButton(A) || Input.GetKey(KeyCode.Z)) && canAttack && onGround && !damageControl.isKnockedBack && !isGrabbing)
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
        else if ((Input.GetButton(A) || Input.GetKey(KeyCode.Z)) && canAttack && inAir && !damageControl.isKnockedBack)
        {
            lastInput = getLastInput();
            canAttack = false;
            canMove = false;
            anim.SetBool("IsIdle", false);
            if (neutralY && neutralX)
            {
                anim.ResetTrigger("Jump");
                anim.SetTrigger("Nair");
                anim.SetBool("Jumping", false);
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
            if (!isRight)
            {
                isRight = true;
                transform.rotation = new Quaternion(0, 0, 0, 0);
                anim.ResetTrigger("Jump");
                anim.SetTrigger("Bair");
                anim.SetBool("Jumping", false);
                attacking();
            }
            else
            {
                anim.ResetTrigger("Jump");
                anim.SetTrigger("Fair");
                anim.SetBool("Jumping", false);
                attacking();
            }

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
            anim.ResetTrigger("Jump");
            anim.SetTrigger("Dair");
            anim.SetBool("Jumping", false);
            canAttack = false;
            canMove = false;
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
            anim.ResetTrigger("Jump");
            anim.SetTrigger("Uair");
            anim.SetBool("Jumping", false);
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
            if (isRight)
            {
                isRight = false;
                anim.ResetTrigger("Jump");
                anim.SetTrigger("Bair");
                anim.SetBool("Jumping", false);
                transform.rotation = new Quaternion(0, 180, 0, 0);
                attacking();
            }
            else
            {
                isRight = false;
                anim.ResetTrigger("Jump");
                anim.SetTrigger("Fair");
                anim.SetBool("Jumping", false);
                attacking();
            }
        }
        else
        {
            canAttack = true;
            canMove = true;
            anim.SetBool("CanAttack", true);
        }
    }
    void attackDir1() //this part looks, and probably is, very poorly optimized. 
    {
        if (lastInput == "Right")
        {
            //parameters for dash attack vs side tilt.
            if (isWalking)
            {
                if (maxVelocity)
                {
                    if (isRight)
                    {
                        anim.SetTrigger("dashAttack");
                        rb.AddForce(0, 0, 1000);
                        isDashing = true;
                        attacking();
                    } else
                    {
                        isRight = true;
                        transform.rotation = new Quaternion(0, 0, 0, 0);
                        anim.SetTrigger("RightA");
                        attacking();
                    }

                }
                else //yes I do realize that I could just create a method to prevent this level of copy-pasting. It's not worth my time right now. 
                {
                    stopVelocity();
                    if (!isRight)
                    {
                        isRight = true;
                        transform.rotation = new Quaternion(0, 0, 0, 0);
                        anim.SetTrigger("RightA");
                        attacking();
                    }
                    else
                    {
                        isRight = true;
                        anim.SetTrigger("RightA");
                        attacking();
                    }
                }
            }
            else
            {
                stopVelocity();
                if (!isRight)
                {
                    isRight = true;
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    anim.SetTrigger("RightA");
                    attacking();
                }
                else
                {
                    isRight = true;
                    anim.SetTrigger("RightA");
                    attacking();
                }
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
            if (isWalking)
            {
                if (maxVelocity)
                {
                    if (!isRight)
                    {
                        anim.SetTrigger("dashAttack");
                        rb.AddForce(0, 0, -1000);
                        isDashing = true;
                        attacking();
                    }
                    else
                    {
                        isRight = false;
                        transform.rotation = new Quaternion(0, 180, 0, 0);
                        anim.SetTrigger("RightA");
                        attacking();
                    }
                }
                else
                {
                    stopVelocity();
                    if (isRight)
                    {
                        isRight = false;
                        transform.rotation = new Quaternion(0, 180, 0, 0);
                        anim.SetTrigger("RightA");
                        attacking();
                    }
                    else
                    {
                        isRight = false;
                        anim.SetTrigger("RightA");
                        attacking();
                    }
                }
            }
            else
            {
                stopVelocity();
                if (isRight)
                {
                    isRight = false;
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                    anim.SetTrigger("RightA");
                    attacking();
                }
                else
                {
                    isRight = false;
                    anim.SetTrigger("RightA");
                    attacking();
                }
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
            if (maxVelocity)
            {
                stopVelocity();
            }            
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
            if (maxVelocity)
            {
                stopVelocity();
            }
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
    #endregion
    #region input buffers
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
    public void inputBufferRight()
    {
        neutralXRight = false;
        neutralYRight = false;
        if (Input.GetAxis(RightJoystickX) > 0.7 || Input.GetKey(KeyCode.RightArrow))
        {
            inputBufferListRight.Add("Right");
        }
        else if (Input.GetAxis(RightJoystickX) < -0.7 || Input.GetKey(KeyCode.LeftArrow))
        {
            inputBufferListRight.Add("Left");
        }
        else if (Input.GetAxis(RightJoystickY) > 0.7 || Input.GetKey(KeyCode.DownArrow))
        {
            inputBufferListRight.Add("Down");
        }
        else if (Input.GetAxis(RightJoystickY) < -0.7 || Input.GetKey(KeyCode.UpArrow))
        {
            inputBufferListRight.Add("Up");
        }
        else
        {
            inputBufferListRight.Add("Neutral");
            neutralYRight = true;
            neutralXRight = true;
        }
    }
    public string getLastInput()
    {
        int amount = inputBufferList.Count;
        inputBufferList.RemoveRange(0, (amount - 8));
        return inputBufferList[0];
    }
    public string getLastInputRight()
    {
        int amount = inputBufferListRight.Count;
        inputBufferListRight.RemoveRange(0, (amount - 8));
        return inputBufferListRight[0];
    }
#endregion
    void moveUpdate()
    {
        if (!iCanMove && !damageControl.isKnockedBack && !isLedged && !isGrabbing)
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
                    if (!isJumping && !isDashing)
                    {
                        Invoke("endMovementWalk", 0.1f);
                    }
                }
                if (canMove)
                {
                    float stickSens = Input.GetAxis(LeftJoystickX);
                    canJump = true;
                    if (lastInput == "Right")
                    {
                        moveLeft = false;
                        moveRight = true;
                        transform.rotation = new Quaternion(0, 0, 0, 0);
                        if (rb.velocity.z <= MaxSpeedGround)
                        {
                            Mathf.Abs(stickSens);
                            rb.AddForce(0, 0, AccelerationGround * stickSens);
                            maxVelocity = false;
                        }
                        else
                        {
                            maxVelocity = true;
                        }
                        isRight = true;
                        isWalking = true;
                        isIdle = false;
                    }
                    else if (lastInput == "Left")
                    {
                        moveRight = false;
                        moveLeft = true;
                        transform.rotation = new Quaternion(0, 180, 0, 0);
                        if (rb.velocity.z >= -MaxSpeedGround)
                        {
                            stickSens = Mathf.Abs(stickSens);
                            rb.AddForce(0, 0, -AccelerationGround * stickSens);
                            maxVelocity = false;
                        }
                        else
                        {
                            maxVelocity = true;
                        }
                        isWalking = true;
                        isRight = false;
                        isIdle = false;
                    }
                }
            }
            else if (canMove)
            {
                moveInAir();
            }
        }
        else if (isLedged)
        {
            if (lastInput == "Down")
            {
                rb.useGravity = true;
                isLedged = false;
                anim.SetBool("LedgeGrab", false);
            }
        }
    }

    public virtual void moveInAir()
    {
        if (lastInput == "Right")
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            if (rb.velocity.z <= MaxSpeedAir)
            {
                rb.AddForce(0, 0, AccelerationAir);
            }
            isWalking = true;
            isRight = true;
        }
        if (lastInput == "Left")
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            if (rb.velocity.z >= -MaxSpeedAir)
            {
                rb.AddForce(0, 0, -AccelerationAir);
            }
            isWalking = true;
            isRight = false;
        }
    }
}
