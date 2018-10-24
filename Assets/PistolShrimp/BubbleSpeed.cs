using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpeed : MonoBehaviour
{
    public float acceleration = 1.0f;
    public float maxSpeed = 60.0f;

    private float curSpeed = 0.0f;

    void Update()
    {
        transform.Translate(Vector3.forward * curSpeed);

        curSpeed += acceleration;
        curSpeed *= 1.08f;
        if (curSpeed > maxSpeed)
            curSpeed = maxSpeed;
    }
}