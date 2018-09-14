using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : TrainOptions
{
    public GameObject menu;
    public GameManager gameManager;
    public override void activation()
    {
        HUD.NeedsInput = false;
        HUD.InMenu = false;
        gameManager.EndTraining();
        menu.SetActive(false);
        HUD.endTraining();
    }
}
