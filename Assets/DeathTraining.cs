using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTraining : MonoBehaviour
{
    public GameManager manager;

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Char")
        {
            CharacterMove standard = other.GetComponent<CharacterMove>();
            if (other.name == (manager.player1Selection + "(Clone)"))
            {
                standard.death(true);
                manager.spawnP1Training();
            }
            else
            {
                SandbagHit sandbag = other.GetComponent<SandbagHit>();
                if (sandbag != null)
                {
                    sandbag.resetPerc();
                    Destroy(sandbag.gameObject);
                    manager.respawnSandbag();
                }
            }
        }
    }
}
