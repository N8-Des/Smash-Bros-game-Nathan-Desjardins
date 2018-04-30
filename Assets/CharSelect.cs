using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour {
    public bool neutralX = false;
    public bool neutralY = false;
    public float threshold = 1;
    public bool canSelect = true;
    public string lastInput;
    public bool player1Selected = false;
    public bool player2Selected = false;
    public GameManager gameManager;
    public GameObject currentlySelected;
    public List<GameObject> player1Char = new List<GameObject>();
    public List<string> inputBufferList = new List<string>();
    public int onNumber = 0;
    public GameObject pressStart;
    public GameObject canvas2;
    public bool isUseful = true;

    public void Update()
    {
        if (isUseful) {
            inputBuffer();
            testForButton();
            testForMovement();
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
    public void testForButton() { 
        if (Input.GetButton("A"))
        {
            gameManager.player1Selection = currentlySelected.ToString();
            player1Selected = true;
            
        } else if (Input.GetButton("B"))
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
    public string getLastInput()
    { 
        int amount = inputBufferList.Count;
        inputBufferList.RemoveRange(0, (amount - 8));
        return inputBufferList[0];
    }
}
