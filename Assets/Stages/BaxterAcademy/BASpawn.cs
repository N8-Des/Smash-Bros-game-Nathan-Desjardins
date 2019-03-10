using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BASpawn : MonoBehaviour {
    public BaxterStage mainStage;
    void Start()
    {
        StartCoroutine(TSpawn());
    }
    public IEnumerator TSpawn()
    {
        yield return new WaitForSeconds(22f);
        GameObject Ball = GameObject.Instantiate((GameObject)Resources.Load("BaxterBall"));
        Ball.transform.position = transform.position;
        Ball.GetComponent<BaxterBall>().stage = mainStage;
        StartCoroutine(TSpawn());
    }
}
