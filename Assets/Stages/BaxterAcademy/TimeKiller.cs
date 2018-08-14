using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKiller : MonoBehaviour {
    public void startTimer()
    {
        Time.timeScale = 0.4f;
        Invoke("stopTimer", 1f);
    }
    public void stopTimer()
    {
        Time.timeScale = 1;
    }
}
