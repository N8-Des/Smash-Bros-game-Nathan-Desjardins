using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhaastUlt : MonoBehaviour {
    public GameObject hitPlayer;
    public GameObject Kayn;
    public Vector3 originalPos;
    Vector3 offset = new Vector3(0, 1.4f, 0);
    public void Start()
    {
        originalPos = Kayn.transform.position;
        Invoke("SendKayn", 3f);
    }
    public void Update()
    {
        if (hitPlayer != null)
        {
            transform.position = hitPlayer.transform.position + offset;
        }
        else
        {
            Kayn.transform.position = originalPos;
            KaynMovement kaynMovement = Kayn.GetComponent<KaynMovement>();
            kaynMovement.endUltNoAnim();
        }
    }
    public void SendKayn()
    {
        Kayn.transform.position = hitPlayer.transform.position;
        KaynMovement kaynMovement = Kayn.GetComponent<KaynMovement>();
        kaynMovement.endUlt();
        Destroy(gameObject);
    }
}
