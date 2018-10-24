using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnTime : MonoBehaviour
{
    public float time;
    void Start()
    {
        StartCoroutine(death());
    }
    public IEnumerator death()
    {
        yield return new WaitForSeconds(time);
        GameObject Explosion = GameObject.Instantiate((GameObject)Resources.Load("ExplosionGrenade"));
        Explosion.transform.position = this.transform.position;
        Destroy(gameObject);
    }
}
