using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour {
    public bool neutralX = false;
    public bool neutralY = false;
    public bool neutralX2 = false;
    public bool neutralY2 = false;
    public float threshold = 1;
    public bool canSelect = true;
    public bool canSelect2 = true;
    public string lastInput;
    public string lastInput2;
    public bool player1Selected = false;
    public bool player2Selected = false;
    public GameManager gameManager;
    public GameObject currentlySelected;
    public GameObject currentlySelected2;
    public List<GameObject> player1Char = new List<GameObject>();
    public List<GameObject> player2Char = new List<GameObject>();
    public List<string> inputBufferList = new List<string>();
    public List<string> inputBufferList2 = new List<string>();
    public int onNumber = 0;
    public int onNumber2 = 0;
    public GameObject pressStart;
    public GameObject canvas2;
    public bool isUseful = true;

    public void Update()
    {
        if (isUseful) {
            inputBuffer();
            inputBuffer2();
            testForButton();
            testForButton2();
            testForMovement();
            testForMovement2();
            testForStart();
        }
    }
    public void testForStart()
    {
        if (player1Selected && player2Selected)
        {
            pressStart.SetActive(true);
            if (Input.GetButton("Start"))
            {
                gameManager.startGame();
                gameObject.SetActive(false);
            }
        }
}
    public void testForButton2() { 
        if (Input.GetButton("A2"))
        {
            gameManager.player2Selection = currentlySelected2.ToString();
            player2Selected = true;
            
        } else if (Input.GetButton("B2"))
        {
            player2Selected = false;
        }
    }
    public void testForButton()
    {
        if (Input.GetButton("A"))
        {
            gameManager.player1Selection = currentlySelected.ToString();
            player1Selected = true;

        }
        else if (Input.GetButton("B"))
        {
            player1Selected = false;
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
    public void inputBuffer2()
    {
        neutralX = false;
        neutralY = false;
        if (Input.GetAxis("LeftJoystickX2") > threshold || Input.GetKey(KeyCode.RightArrow))
        {
            inputBufferList2.Add("Right");
        }
        else if (Input.GetAxis("LeftJoystickX2") < -threshold || Input.GetKey(KeyCode.LeftArrow))
        {
            inputBufferList2.Add("Left");
        }
        else if (Input.GetAxis("LeftJoystickY2") > threshold || Input.GetKey(KeyCode.DownArrow))
        {
            inputBufferList2.Add("Down");
        }
        else if (Input.GetAxis("LeftJoystickY2") < -threshold || Input.GetKey(KeyCode.UpArrow))
        {
            inputBufferList2.Add("Up");
        }
        else
        {
            inputBufferList2.Add("Neutral");
            neutralY2 = true;
            neutralX2 = true;
        }
    }
    public void testForMovement2()
    {
        if (canSelect2)
        {
            currentlySelected2 = player2Char[onNumber2];
            lastInput2 = getLastInput2();
            canSelect2 = false;
            if (lastInput2 == "Right" && !player2Selected)
            {
                currentlySelected2.SetActive(false);
                onNumber2 += 1;
                currentlySelected2 = player2Char[onNumber2];
                currentlySelected2.SetActive(true);
                Invoke("waitToSelect2", 0.3f);
            }
            else if (lastInput2 == "Left" && !player2Selected)
            {
                currentlySelected2.SetActive(false);
                onNumber2 -= 1;
                currentlySelected2 = player2Char[onNumber2];
                currentlySelected2.SetActive(true);
                Invoke("waitToSelect2", 0.3f);
            }
            else
            {
                canSelect2 = true;
            }
        }
    }
    public void testForMovement()
    {
        if (canSelect) {
            currentlySelected = player1Char[onNumber];
            lastInput = getLastInput();
            canSelect = false;
            if (lastInput == "Right" && !player1Selected)
            {
                currentlySelected.SetActive(false);
                onNumber += 1;
                currentlySelected = player1Char[onNumber];
                currentlySelected.SetActive(true);
                Invoke("waitToSelect", 0.3f);
            } else if (lastInput == "Left" && !player1Selected)
            {
                currentlySelected.SetActive(false);
                onNumber -= 1;
                currentlySelected = player1Char[onNumber];
                currentlySelected.SetActive(true);
                Invoke("waitToSelect", 0.3f);
            }
            else
            {
                canSelect = true;
            }
        }
    }
    public void waitToSelect()
    {
        canSelect = true;
    }
    public void waitToSelect2()
    {
        canSelect2 = true;
    }
    public string getLastInput()
    { 
        int amount = inputBufferList.Count;
        inputBufferList.RemoveRange(0, (amount - 8));
        return inputBufferList[0];
    }
    public string getLastInput2()
    {
        int amount2 = inputBufferList2.Count;
        inputBufferList2.RemoveRange(0, (amount2 - 8));
        return inputBufferList2[0];
    }
}
