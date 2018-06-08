using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillRad : MonoBehaviour {
    public GameManager manager;

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Char")
        {
            BaseCharMove standard = other.GetComponent<BaseCharMove>();
            if (other.name == (manager.player1Selection + "(Clone)"))
            {
                standard.death(true);
                manager.respawnP1();
            } else if (other.name == (manager.player2Selection + "(Clone)"))
            {
                standard.death(true);
                manager.respawnP2();
            }
        }
    }
}
