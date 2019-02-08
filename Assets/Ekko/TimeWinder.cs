using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWinder : MonoBehaviour {
    public EkkoMovement EkkoSpawn;
    bool hitTarget = false;
    void Start()
    {
        StartCoroutine(goBack());
    }

    public IEnumerator goBack()
    {
        float DistY;
        float DistX;
        yield return new WaitForSeconds(0.7f);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        while (!hitTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, EkkoSpawn.transform.position, Time.deltaTime * 5);
            yield return new WaitForEndOfFrame();
            DistY = transform.position.y - EkkoSpawn.transform.position.y;
            DistY = Mathf.Abs(DistY);
            DistX = transform.position.x - EkkoSpawn.transform.position.x;
            DistX = Mathf.Abs(DistX);
            if (DistX + DistY < 0.01)
            {
                hitTarget = true;
            }
        }
        EkkoSpawn.canNB = true;
        Destroy(gameObject);
    }
}
