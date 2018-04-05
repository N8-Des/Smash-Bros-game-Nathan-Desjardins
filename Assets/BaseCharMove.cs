using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharMove : MonoBehaviour
{
    public Rigidbody rb;
    public bool leftA = false;
    public bool rightA = false;
    public bool downA = false;
    public bool baseB = false;
    public bool downB = false;
    public bool upB = false;
    public bool rightB = false;
    public bool leftB = false;
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
    public Animator anim;
    public float BADelay;
    public float SADelay;
    public float UADelay;
    public void Start()
    {
        inputBufferList.Add("ShutUpCount");
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public virtual void BaseA()
    {
    }
    public virtual void SideA()
    {
    }
    public virtual void UpA()
    {
    }
    public void FixedUpdate()
    {
        inputBuffer();
        attackUpdate();
        moveUpdate();
    }
    void attackUpdate()
    {
        lastInput = getLastInput();
        if ((Input.GetButton("A") || Input.GetKey(KeyCode.Z)) && canAttack)
        {
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
    }
    void attackDir1()
    {
        if (lastInput == "Right")
        {
            if (transform.rotation == new Quaternion(0, 180, 0, 0))
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
        if (Input.GetAxis("LeftJoystickX") > threshold || Input.GetKey(KeyCode.RightArrow))
        {
            inputBufferList.Add("Right");
        }
        else if (Input.GetAxis("LeftJoystickX") < -threshold || Input.GetKey(KeyCode.LeftArrow))
        {
            inputBufferList.Add("Left");
        }
        else if (Input.GetAxis("LeftJoystickY") > threshold || Input.GetKey(KeyCode.DownArrow))
        {
            inputBufferList.Add("Down");
        }
        else if (Input.GetAxis("LeftJoystickY") < -threshold || Input.GetKey(KeyCode.UpArrow))
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
        if (isWalking && neutralX || (!canMove))
        {
            isWalking = false;
            moveLeft = false;
            moveRight = false;
            rb.velocity = new Vector3(0, 0, 0);
            isIdle = true;
        }
        if (canMove)
        {
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
}
