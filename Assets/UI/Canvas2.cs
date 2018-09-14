using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas2 : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject OptionsMenu;
    public bool InTraining = false;
    public bool NeedsInput = false;
    public bool InMenu = false;
    public TrainOptions currentlySelected;
    public bool neutralX = true;
    public bool neutralY = true;
    public float threshold = 0.9f;
    public bool canSelect = true;
    public int onNumber = 0;
    public string lastInput;
    public List<string> inputBufferList = new List<string>();
    public List<TrainOptions> optionList = new List<TrainOptions>();

    public void playerAnimSpawn()
    {
        gameManager.startSpawnAnimPlayer();
    }
    public void playersCanMove()
    {
        gameManager.startGameInput();
    }
    public void Update()
    {
        StartPressed();
        if (NeedsInput)
        {
            inputBuffer();
            testForButton();
            testForMovement();
        }
    }

    void StartPressed()
    {
        if ((Input.GetButton("Start") || Input.GetKey(KeyCode.Escape)) && InTraining && !InMenu)
        {
            gameManager.stopGameInput();
            currentlySelected = optionList[0];
            currentlySelected.isHovered = true;
            InMenu = true;
            NeedsInput = true;
            OptionsMenu.SetActive(true);
            inputBufferList.Add("rhee");
        }
    }
    public void testForButton()
    {
        if (Input.GetButton("A") || Input.GetKey(KeyCode.Z))
        {
            currentlySelected.activation();
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
    public void endTraining()
    {
        foreach (TrainOptions button in optionList)
        {
            button.isHovered = false;
        }
    }
    public void testForMovement()
    {
        if (canSelect)
        {
            currentlySelected = optionList[onNumber];
            lastInput = getLastInput();
            canSelect = false;
            if (lastInput == "Down")
            {
                if (onNumber + 1 >= optionList.Count)
                {
                    currentlySelected.isHovered = false;
                    onNumber = 0;
                    currentlySelected = optionList[0];
                    currentlySelected.isHovered = true;
                    Invoke("waitToSelect", 0.3f);
                }
                else
                {
                    currentlySelected.isHovered = false;
                    onNumber += 1;
                    currentlySelected = optionList[onNumber];
                    currentlySelected.isHovered = true;
                    Invoke("waitToSelect", 0.3f);
                }
            }
            else if (lastInput == "Up")
            {
                if (onNumber - 1 < 0)
                {
                    currentlySelected.isHovered = false;
                    onNumber = optionList.Count - 1;
                    currentlySelected = optionList[onNumber];
                    currentlySelected.isHovered = true;
                    Invoke("waitToSelect", 0.3f);
                }
                else
                {
                    currentlySelected.isHovered = false;
                    onNumber -= 1;
                    currentlySelected = optionList[onNumber];
                    currentlySelected.isHovered = true;
                    Invoke("waitToSelect", 0.3f);
                }
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