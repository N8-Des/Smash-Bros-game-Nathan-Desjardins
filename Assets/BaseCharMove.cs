using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharMove : MonoBehaviour {
    public Rigidbody rb;
    public bool baseA = false;
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
    public string lastInput;
    public List<string> inputBufferList = new List<string> ();
    public Vector3 moveSpeed;
    public Animator anim;
    public void Start()
    {
        inputBufferList.Add("ShutUpCount");
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();       

    }
    public void FixedUpdate()
    {
        inputBuffer();
        attackUpdate();
        moveUpdate();
    }
    void attackUpdate ()
    {
        lastInput = getLastInput();
        if (Input.GetButtonDown("A"))
        {
            canMove = false;
            if(neutralY && neutralX)
            {
                baseA = true;
            }
            else
            {
                attackDir();
            }
        }
        else
        {
            canMove = true;
        }
    }
    void attackDir()
    {
        if (lastInput == "Right")
        {
            if (this.transform.rotation == new Quaternion(0, 180, 0, 0))
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                rightA = true;
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
        if (lastInput == "Left")
        {
            if (this.transform.rotation == new Quaternion(0, 0, 0, 0))
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                rightA = true;
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
    }
    public void inputBuffer()
    {
        neutralX = false;
        neutralY = false;
        if (Input.GetAxis("LeftJoystickX") > threshold)
        {
            inputBufferList.Add("Right");
        }
        else if (Input.GetAxis("LeftJoystickX") < -threshold)
        {
            inputBufferList.Add("Left");
        }
        else if (Input.GetAxis("LeftJoystickY") > threshold)
        {
            inputBufferList.Add("Down");
        }
        else if (Input.GetAxis("LeftJoystickY") < -threshold)
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
