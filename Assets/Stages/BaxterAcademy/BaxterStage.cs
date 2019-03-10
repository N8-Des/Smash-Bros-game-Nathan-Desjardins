using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaxterStage : MonoBehaviour {
    public List<BaxterWaterGround> ground = new List<BaxterWaterGround>();
    public List<GameObject> uiNotices = new List<GameObject>(); 
    public bool isFlooding = false;
    public bool isLagging = false;
    public bool isVaping = false;
    public bool isHole = false;
    public GameObject FloodParticle;
    public GameObject VapeParticle;
    public GameObject WallHole;
    public void switchFlood()
    {
        if (!isFlooding)
        {
            isFlooding = true;
            uiNotices[0].SetActive(true);
            FloodParticle.SetActive(true);
            foreach (BaxterWaterGround grounds in ground)
            {
                grounds.isFlood = true;
            }
        } else
        {
            isFlooding = false;
            FloodParticle.SetActive(false);
            foreach (BaxterWaterGround grounds in ground)
            {
                grounds.isFlood = false;
            }
        }
    }
    public void switchLag()
    {
        if (!isLagging)
        {
            uiNotices[1].SetActive(true);
            isLagging = true;
            Time.timeScale = 0.3f;
        } else
        {
            isLagging = false;
            Time.timeScale = 1;
        }
    }
    public void switchVape()
    {
        if (!isVaping)
        {
            uiNotices[2].SetActive(true);
            isVaping = true;
            VapeParticle.SetActive(true);
        } else
        {
            uiNotices[2].SetActive(false);
            isVaping = false;
            VapeParticle.SetActive(false);
        }
    }
    public void switchHole()
    {
        if (!isHole)
        {
            isHole = true;
            WallHole.SetActive(false);
            uiNotices[3].SetActive(true);
        } else
        {
            isHole = false;
            WallHole.SetActive(true);
        }
    }

    public void despawnUI()
    {
        foreach (GameObject ui in uiNotices)
        {
            ui.SetActive(false);
        }
    }
}
