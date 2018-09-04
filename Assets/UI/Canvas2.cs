using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas2 : MonoBehaviour {
    public GameManager gameManager;
    public void playerAnimSpawn()
    {
        gameManager.startSpawnAnimPlayer();
    }
    public void playersCanMove()
    {
        gameManager.startGameInput();
    }
}
