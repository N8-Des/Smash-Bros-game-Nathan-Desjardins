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
    public void startGame()
    {
        GameObject Player1 = GameObject.Instantiate((GameObject)Resources.Load(player1Selection));
        GameObject Player2 = GameObject.Instantiate((GameObject)Resources.Load(player2Selection));
        Player1.transform.position = player1Spawn.transform.position;
        Player2.transform.position = player2Spawn.transform.position;
        p1score = GameObject.Find(player1Selection + "P");
        p2score = GameObject.Find(player2Selection + "P");
        p1score.transform.position -= new Vector3(0, -350, 0);
        p2score.transform.position -= new Vector3(0, -350, 0);
    }
}
