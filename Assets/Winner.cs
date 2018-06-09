using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour {
    public AudioSource announcer;

    public void voiceline()
    {
        announcer.Play();
    }
}
