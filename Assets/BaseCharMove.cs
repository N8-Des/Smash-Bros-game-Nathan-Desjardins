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
    public Vector3 moveSpeed;
    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
    }
    public void FixedUpdate()
    {
        float axisLeft = Input.GetAxis("LeftJoystickX");
        if (Input.GetButtonDown("A"))
        {
            baseA = true;
        }
        if (Input.GetButtonDown("A") && axisLeft >= 0.7)
        {
            rightA = true;
        }
        if (axisLeft >= 0.9)
        {
            moveRight = true;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            rb.velocity = moveSpeed;
        }
        if (axisLeft <= -0.9)
        {
            moveLeft = true;
            transform.rotation = new Quaternion(0, 180, 0, 0);
            rb.velocity = moveSpeed * -1;
        }

        if (axisLeft == 0 && (moveRight == true || moveLeft == true)) 
        {
            Debug.Log("No Movement");
            moveLeft = false;
            moveRight = false;
            isIdle = true;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
