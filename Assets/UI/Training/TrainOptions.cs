using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainOptions : MonoBehaviour {
    public bool isHovered = false;
    public GameObject selectIndicator;
    public Canvas2 HUD;
    public void Update()
    {
        if (isHovered)
        {
            selectIndicator.SetActive(true);
        }
        else
        {
            selectIndicator.SetActive(false);
        }
    }
    public virtual void activation()
    {
        //oof
    }
}
