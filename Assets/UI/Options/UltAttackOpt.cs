using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltAttackOpt : OptBase {
    public GameObject turnOn;
    public bool neutralX = true;
    public bool neutralY = true;
    public bool OptionOn = true;
    public float threshold = 1f;
    public List<string> inputBufferList = new List<string>();
    public Text OnOff;
    public bool canSelect = true;
    public string lastInput;
    public GameManager gmanage;

    public void Update()
    {
        if (isSelected)
        {
            turnOn.SetActive(true);
            inputBuffer();
            testForMovement();
        }
        else
        {
            turnOn.SetActive(false);
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
    public void testForMovement()
    {
        if (canSelect)
        {
            lastInput = getLastInput();
            canSelect = false;
            if (lastInput == "Right" || lastInput == "Left")
            {
                if (OptionOn)
                {
                    OptionOn = false;
                    OnOff.text = "OFF";
                    gmanage.ultimatesOn = false;
                } else
                {
                    OptionOn = true;
                    OnOff.text = "ON";
                    gmanage.ultimatesOn = true;
                }
                Invoke("waitToSelect", 0.3f);
            }
            else
            {
                canSelect = true;
            }
        }
    }
    public string getLastInput()
    {
        int amount = inputBufferList.Count;
        inputBufferList.RemoveRange(0, (amount - 8));
        return inputBufferList[0];
    }
    public void waitToSelect()
    {
        canSelect = true;
    }
}
