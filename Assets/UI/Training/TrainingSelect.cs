using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingSelect : MonoBehaviour
{
    public bool neutralX = false;
    public bool neutralY = false;
    public bool neutralX2 = false;
    public bool neutralY2 = false;
    public float threshold = 0.6f;
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
    public List<string> inputBufferList = new List<string>();
    public List<string> inputBufferList2 = new List<string>();
    public int onNumber = 0;
    public int onNumber2 = 0;
    public GameObject pressStart;
    public GameObject canvas2;
    public bool isUseful = true;
    public GameObject optionsCanvas;
    public GameObject selectionCanvas;
    public GameObject SelectedP1Indicator;
    public GameObject MainMenu;
    public void OnEnable()
    {
        isUseful = false;
        Invoke("usefulness", 0.3f);
    }

    public void Update()
    {
        if (isUseful)
        {
            inputBuffer();
            testForButton();
            testForMovement();
            testForStart();
        }
    }
    public void testForStart()
    {
        if (player1Selected)
        {
            pressStart.SetActive(true);
            if (Input.GetButton("Start") || Input.GetKey(KeyCode.Return))
            {
                gameManager.StartTraining();
                gameObject.SetActive(false);
            }
        }
        else
        {
            pressStart.SetActive(false);
        }
    }  
    public void testForButton()
    {
        if (Input.GetButton("A") || Input.GetKey(KeyCode.Z))
        {
            gameManager.player1Selection = currentlySelected.ToString();
            player1Selected = true;
            SelectedP1Indicator.SetActive(true);

        }
        else if (Input.GetButton("B") || Input.GetKey(KeyCode.X))
        {
            if (player1Selected)
            {
                player1Selected = false;
                SelectedP1Indicator.SetActive(false);
            }
            else
            {
                MainMenu.SetActive(true);
                MainMenuNavigation mm = MainMenu.GetComponent<MainMenuNavigation>();
                mm.isUseful = true;
                isUseful = false;
                gameObject.SetActive(false);
            }

        }
      
    }
    void usefulness()
    {
        isUseful = true;
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
            currentlySelected = player1Char[onNumber];
            lastInput = getLastInput();
            canSelect = false;
            if (lastInput == "Right" && !player1Selected)
            {
                if (onNumber + 1 >= player1Char.Count)
                {
                    currentlySelected.SetActive(false);
                    onNumber = 0;
                    currentlySelected = player1Char[0];
                    currentlySelected.SetActive(true);
                    Invoke("waitToSelect", 0.2f);
                }
                else
                {
                    currentlySelected.SetActive(false);
                    onNumber += 1;
                    currentlySelected = player1Char[onNumber];
                    currentlySelected.SetActive(true);
                    Invoke("waitToSelect", 0.2f);
                }
            }
            else if (lastInput == "Left" && !player1Selected)
            {
                if (onNumber - 1 < 0)
                {
                    currentlySelected.SetActive(false);
                    onNumber = player1Char.Count - 1;
                    currentlySelected = player1Char[onNumber];
                    currentlySelected.SetActive(true);
                    Invoke("waitToSelect", 0.2f);
                }
                else
                {
                    currentlySelected.SetActive(false);
                    onNumber -= 1;
                    currentlySelected = player1Char[onNumber];
                    currentlySelected.SetActive(true);
                    Invoke("waitToSelect", 0.2f);
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
