using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour {
    public Material Red;
    public Material Blue;
    public Material White;
    public GameObject RedLight;
    public GameObject BlueLight;
    public Renderer rendThis;

    public void OnTriggerEnter(Collider other)
    {
        CharacterMove playerOn = other.GetComponent<CharacterMove>();
        if (playerOn != null)
        {
            if (playerOn.A == "A")
            {
                rendThis.material = Red;
                RedLight.SetActive(true);
            }
            else if (playerOn.A == "A2")
            {
                rendThis.material = Blue;
                BlueLight.SetActive(true);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        CharacterMove playerOn = other.GetComponent<CharacterMove>();
        if (playerOn != null)
        {
            rendThis.material = White;
            RedLight.SetActive(false);
            BlueLight.SetActive(false);
        }
    }
}
