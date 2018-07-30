using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour {
    public int percent;
    public Image me;
    float amount = 0f;
    public CharacterMove selectedPlayer;
    public bool ultimate = false;
    public void reset()
    {
        me.fillAmount = 0;
        amount = 0;
    }
    private void start()
    {
        me = gameObject.GetComponent<Image>();
    }
    public void ChangeValue(int hurtAmount)
    {
        if (!ultimate)
        {
            if (amount <= 200)
            {
                me.fillAmount += hurtAmount * 0.005f;
                amount += hurtAmount * 0.5f;
            }
            else
            {
                ultimate = true;
                selectedPlayer.ultReady = true;
            }
        }
    }
}
