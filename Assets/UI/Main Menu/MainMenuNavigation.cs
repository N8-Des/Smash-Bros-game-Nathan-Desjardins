using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
    public bool neutralX = false;
    public bool neutralY = false;
    public float threshold = 0.6f;
    public bool canSelect = true;
    public string lastInput;
    public bool player1Selected = false;
    public GameManager gameManager;
    public MenuButtons currentlySelected;
    public List<MenuButtons> Buttons = new List<MenuButtons>();
    public List<string> inputBufferList = new List<string>();
    public int onNumber = 0;
    public GameObject canvas2;
    public bool isUseful = true;

    public void Start()
    {
        currentlySelected = Buttons[0];
        currentlySelected.isSelected = true;
    }
    public void Update()
    {
        if (isUseful)
        {
            inputBuffer();
            testForButton();
            testForMovement();
        }
    }
    public void testForButton()
    {
        if (Input.GetButton("A") || Input.GetKey(KeyCode.Z))
        {
            currentlySelected.CorrelatedCanvas.SetActive(true);
            isUseful = false;
            gameObject.SetActive(false);
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
            currentlySelected = Buttons[onNumber];
            lastInput = getLastInput();
            canSelect = false;
            if (lastInput == "Up" && !player1Selected)
            {
                if (onNumber + 1 >= Buttons.Count)
                {
                    currentlySelected.isSelected = false;
                    onNumber = 0;
                    currentlySelected = Buttons[0];
                    currentlySelected.isSelected = true;
                    Invoke("waitToSelect", 0.3f);
                }
                else
                {
                    currentlySelected.isSelected = false;
                    onNumber += 1;
                    currentlySelected = Buttons[onNumber];
                    currentlySelected.isSelected = true;
                    Invoke("waitToSelect", 0.3f);
                }
            }
            else if (lastInput == "Down" && !player1Selected)
            {
                if (onNumber - 1 < 0)
                {
                    currentlySelected.isSelected = false;
                    onNumber = Buttons.Count - 1;
                    currentlySelected = Buttons[onNumber];
                    currentlySelected.isSelected = true;
                    Invoke("waitToSelect", 0.3f);
                }
                else
                {
                    currentlySelected.isSelected = false;
                    onNumber -= 1;
                    currentlySelected = Buttons[onNumber];
                    currentlySelected.isSelected = true;
                    Invoke("waitToSelect", 0.3f);
                }
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
