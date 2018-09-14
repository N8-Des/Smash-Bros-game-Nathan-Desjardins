using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltToggleScript : TrainOptions
{
    public GameManager gameManager;
    public GameObject menu;
    public override void activation()
    {
        BaseHit bh = gameManager.bh1;
        ProgressManager prog = bh.progressBar;
        Debug.Log(prog.name);
        prog.ChangeValue(200);
    }
}
