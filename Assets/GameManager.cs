﻿using System.Collections;
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
    public GameObject Player1;
    public GameObject Player2;
    public GameObject mainCamera;
    public GameObject background;
    public GameObject winner;
    public GameObject focus;
    public GameObject selectionCanvas;
    public ProgressManager Prog1;
    public ProgressManager Prog2;
    public CameraFocus cam;
    public CharacterMove playerOne;
    public CharacterMove playerTwo;
    public int lives = 3;
    public bool ultimatesOn;
    public Stage stageSelected;
    public Animator canvas2;
    public GameObject UltimateMeters;
    

    public void startGame()
    {
        player1Respawn = stageSelected.player1Respawn;
        player2Respawn = stageSelected.player2Respawn;
        player1Spawn = stageSelected.player1Spawn;
        player2Spawn = stageSelected.player2Spawn;
        mainCamera.transform.position = stageSelected.mainCamPos.transform.position;
        player1Life.life = lives;
        player1Life.updateLifeDisplay();
        player2Life.life = lives;
        player2Life.updateLifeDisplay();
        GameObject Player1 = GameObject.Instantiate((GameObject)Resources.Load(player1Selection));
        GameObject Player2 = GameObject.Instantiate((GameObject)Resources.Load(player2Selection));
        playerOne = Player1.GetComponent<CharacterMove>();
        playerTwo = Player2.GetComponent<CharacterMove>();
        Player1.transform.position = player1Spawn.transform.position;
        Player2.transform.position = player2Spawn.transform.position;
        p1score = GameObject.Find(player1Selection + "P");
        p2score = GameObject.Find(player2Selection + "P");
        p1score.transform.position += new Vector3(0, 350, 0);
        p2score.transform.position += new Vector3(0, 350, 0);
        BaseHit bh1 = Player1.GetComponent<BaseHit>();
        BaseHit bh2 = Player2.GetComponent<BaseHit>();
        playerOne.stopMoving();
        playerTwo.stopMoving();
        canvas2.SetTrigger("StartGame");
        if (!ultimatesOn)
        {
            bh1.ultOn = false;
            playerOne.ultsOn = false;
            bh2.ultOn = false;
            playerTwo.ultsOn = false;
        }
        else
        {
            bh1.progressBar = Prog1;
            bh2.progressBar = Prog2;
            bh1.ultOn = true;
            bh2.ultOn = true;
            playerOne.ultsOn = true;
            playerOne.ultsOn = true;
        }
        //cam.Players.Add(Player1);
        //cam.Players.Add(Player2);
    }
    public void startGameInput()
    {
        playerOne.startMoving();
        playerTwo.startMoving();
    }
    public void respawnP1()
    {
        player1Life.LoseLife();
        if (player1Life.life > 0)
        {
            Invoke("spawnp1", 3);
        }
        else
        {
            CharacterMove[] players = FindObjectsOfType<CharacterMove>();
            winner = GameObject.Find(player2Selection + "W");
            UltimateMeters.SetActive(false);
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
    public void startSpawnAnimPlayer()
    {
        playerOne.Spawn();
        playerTwo.Spawn();
        playerTwo.TurnLeft();
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
            Invoke("spawnp2", 3);
        }
        else
        {
            CharacterMove[] players = FindObjectsOfType<CharacterMove>();
            winner = GameObject.Find(player1Selection + "W");
            UltimateMeters.SetActive(false);
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
        BaseHit bh1 = Player1.GetComponent<BaseHit>();
        playerOne = Player1.GetComponent<CharacterMove>();
        bh1.invulnStart();
        if (!ultimatesOn)
        {
            bh1.ultOn = false;
            playerOne.ultsOn = false;
        }
        else
        {
            bh1.progressBar = Prog1;
        }
        //cam.Players.RemoveRange(0, cam.Players.Count);
        //cam.Players.Add(focus);
        //cam.Players.Add(Player1);
        //cam.Players.Add(Player2);
    }
    void spawnp2()
    {
        GameObject Player2 = GameObject.Instantiate((GameObject)Resources.Load(player2Selection));
        Player2.transform.position = player2Respawn.transform.position;
        BaseHit bh2 = Player2.GetComponent<BaseHit>();
        bh2.invulnStart();
        if (!ultimatesOn)
        {
            bh2.ultOn = false;
            playerTwo.ultsOn = false;
        }
        bh2.progressBar = Prog2;
        //cam.Players.RemoveRange(0, cam.Players.Count);
        //cam.Players.Add(focus);
        //cam.Players.Add(Player1);
        //cam.Players.Add(Player2);
    }
    public void newGame()
    {
        p1score.transform.position -= new Vector3(0, 350, 0);
        p2score.transform.position -= new Vector3(0, 350, 0);
        winner.transform.position -= new Vector3(0, 700, 0);
        UltimateMeters.SetActive(true);
        background.SetActive(false);
        selectionCanvas.SetActive(true);
        CharSelect charSelection = selectionCanvas.GetComponent<CharSelect>();
        charSelection.isUseful = true;
        Prog1.reset();
        Prog2.reset();
    }
}
