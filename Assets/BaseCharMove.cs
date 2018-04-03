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
        attackUpdate();
        moveUpdate();
    }
    void attackUpdate ()
    {
        inputBuffer();
        lastInput = getLastInput();
        if (Input.GetButtonDown("A"))
        {
            if(neutralY && neutralX)
            {
                baseA = true;
            }else
            {
                attackDir();
            }
        }
    }
    void attackDir()
    {
        if (lastInput == "Right")
        {
            rightA = true;
        }
    }
    void inputBuffer()
    {
        {

        }
        neutralX = false;
        if (Input.GetAxis("LeftJoystickX") > threshold)
        {
            inputBufferList.Add("Right");
        } else if (Input.GetAxis("LeftJoystickX") < -threshold)
        {
            inputBufferList.Add("Left");
        }
        else
        {
            inputBufferList.Add("Neutral");
            neutralX = true;
        }

        neutralY = false;
        if(Input.GetAxis("LeftJoystickY") > threshold)
        {
            inputBufferList.Add("Up");
        }
        else if (Input.GetAxis("LeftJoystickY") < -threshold)
        {
            inputBufferList.Add("Neutral");
            inputBufferList.Add("Down");
        }
        else
        {
            neutralY = true;
        }
    }
    public string getLastInput()
    {
        int amount = inputBufferList.Count;
        inputBufferList.RemoveRange(0, (amount - 1));
        return inputBufferList[0];
    }
    void moveUpdate()
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
        }   else if (isWalking && neutralX)
        {
            isWalking = false;
            moveLeft = false;
            moveRight = false;
            rb.velocity = new Vector3(0, 0, 0);
            isIdle = true;
        }
    }
}
