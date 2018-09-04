using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour {
    public int percent;
    public Image me;
    public float amount = 0f;
    public CharacterMove selectedPlayer;
    public bool ultimate = false;
    public GameObject ultIndication;

    public void reset()
    {
        me.fillAmount = 0;
        amount = 0;
        ultIndication.SetActive(false);
    }
    private void start()
    {
        me = gameObject.GetComponent<Image>();
    }
    public void ChangeValue(int hurtAmount)
    {
        if (!ultimate)
        {
            if (amount <= 100)
            {
                me.fillAmount += hurtAmount * 0.005f;
                amount += hurtAmount * 0.5f;
            }
            else
            {
                ultimate = true;
                ultIndication.SetActive(true);
                selectedPlayer.ultReady = true;
            }
        }
    }
}
