using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EkkoTime : MonoBehaviour
{
    public float time;
    public EkkoMovement EkkoDad;
    void Start()
    {
        EkkoDad = GameObject.FindObjectOfType<EkkoMovement>();
        StartCoroutine(death());
    }
    public IEnumerator death()
    {
        yield return new WaitForSeconds(time);
        EkkoDad.changeUB();
        Destroy(gameObject);
    }
}
