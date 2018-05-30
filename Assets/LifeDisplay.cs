using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    public int lifeStart = 1;
    public int life;
    public Text lifeDisplay;
    void Start()
    {
        life = lifeStart;
        updateLifeDisplay();
    }
    public void LoseLife()
    {
        life -= 1;
        updateLifeDisplay();
    }
    public void updateLifeDisplay()
    {
        lifeDisplay.text = life.ToString();
    }
}
