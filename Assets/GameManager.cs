using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public string player1Selection;
    public string player2Selection;
    public GameObject player1Spawn;
    public GameObject player2Spawn;
    public GameObject p1score;
    public GameObject p2score;
    public GameObject player1Respawn;
    public GameObject player2Respawn;
    public LifeDisplay player1Life;
    public LifeDisplay player2Life;
    public GameObject mainCamera;
    public GameObject background;
    public GameObject winner;
    public GameObject selectionCanvas;

    public void startGame()
    {
        player1Life.life = 3;
        player1Life.updateLifeDisplay();
        player2Life.life = 3;
        player2Life.updateLifeDisplay();
        GameObject Player1 = GameObject.Instantiate((GameObject)Resources.Load(player1Selection));
        GameObject Player2 = GameObject.Instantiate((GameObject)Resources.Load(player2Selection));
        Player1.transform.position = player1Spawn.transform.position;
        Player2.transform.position = player2Spawn.transform.position;
        p1score = GameObject.Find(player1Selection + "P");
        p2score = GameObject.Find(player2Selection + "P");
        p1score.transform.position += new Vector3(0, 350, 0);
        p2score.transform.position += new Vector3(0, 350, 0);
    }
    public void respawnP1()
    {
        player1Life.LoseLife();
        if (player1Life.life > 0)
        {
            Invoke("spawnp1", 5);
        }
        else
        {
            CharacterMove[] players = FindObjectsOfType<CharacterMove>();
            winner = GameObject.Find(player2Selection + "W");
            Invoke("moveWinner", 1);
            background.SetActive(true);
            foreach (CharacterMove target in players)
            {
                Destroy(target);
            }
            GameObject winningPlayer = GameObject.Find(player2Selection + "(Clone)");
            CharacterMove basic = winningPlayer.GetComponent<CharacterMove>();
            basic.death(false);
            Invoke("newGame", 3);
        }
    }
    void moveWinner()
    {
        winner.transform.position += new Vector3(0, -700, 0);
    }
    public void respawnP2()
    {
        player2Life.LoseLife();
        if (player2Life.life > 0)
        {
            Invoke("spawnp2", 5);
        }
        else
        {
            CharacterMove[] players = FindObjectsOfType<CharacterMove>();
            winner = GameObject.Find(player1Selection + "W");
            Invoke("moveWinner", 1);
            background.SetActive(true);
            foreach (CharacterMove target in players)
            {
                Destroy(target);
            }
            GameObject winningPlayer = GameObject.Find(player1Selection + "(Clone)");
            CharacterMove basic = winningPlayer.GetComponent<CharacterMove>();
            basic.death(false);
            Invoke("newGame", 3);
        }
    }
    void spawnp1()
    {
        GameObject Player1 = GameObject.Instantiate((GameObject)Resources.Load(player1Selection));
        Player1.transform.position = player1Respawn.transform.position;
    }
    void spawnp2()
    {
        GameObject Player2 = GameObject.Instantiate((GameObject)Resources.Load(player2Selection));
        Player2.transform.position = player2Respawn.transform.position;
    }
    public void newGame()
    {
        p1score.transform.position -= new Vector3(0, 350, 0);
        p2score.transform.position -= new Vector3(0, 350, 0);
        winner.transform.position -= new Vector3(0, 700, 0);
        background.SetActive(false);
        selectionCanvas.SetActive(true);
        CharSelect charSelection = selectionCanvas.GetComponent<CharSelect>();
        charSelection.isUseful = true;
    }
}
