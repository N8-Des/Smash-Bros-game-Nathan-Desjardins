using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BASpawn : MonoBehaviour {
    void Start()
    {
        StartCoroutine(TSpawn());
    }
    public IEnumerator TSpawn()
    {
        yield return new WaitForSeconds(22f);
        GameObject Ball = GameObject.Instantiate((GameObject)Resources.Load("BaxterBall"));
        StartCoroutine(TSpawn());
    }
}
