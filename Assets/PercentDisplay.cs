using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentDisplay : MonoBehaviour {
    public int percentStart = 0;
    public int percent;
    public Text percentDisplay;
    void Start()
    {
        percent = percentStart;
        updatePercentDisplay();
    }
    public int takeDamage(int damage)
    {
        percent += damage;
        updatePercentDisplay();
        return percent;
    }
    void updatePercentDisplay()
    {
        percentDisplay.text = percent.ToString();
    }
    public void resetPercentDisplay()
    {
        percent = 0;
        updatePercentDisplay();
    }
}
