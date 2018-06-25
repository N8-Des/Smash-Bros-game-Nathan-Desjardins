using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaxterBall : MonoBehaviour {
    public int rng;
    public void OnTriggerEnter(Collider other)
    {
        CharacterMove character = other.GetComponent<CharacterMove>();
        if (character != null)
        {
            rng = Random.Range(1, 10);
            switch (rng) {
                case 1:
                    Debug.Log("Hal");
                    break;
                case 2:
                    Debug.Log("Angie");
                    break;
                case 3:
                    Debug.Log("Learn By Doing");
                    break;
                case 4:
                    Debug.Log("Wi-Fi");
                    break;
                case 5:
                    Debug.Log("Thinkpad");
                    break;
                case 6:
                    Debug.Log("Learn By Doing");
                    break;
                case 7:
                    Debug.Log("FIRST Robotics");
                    break;
                case 8:
                    Debug.Log("Innovative & Ethical");
                    break;
                case 9:
                    Debug.Log("Minecraft Is Lit");
                    break;
                case 10:
                    Debug.Log("Moxhay");
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
