using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusMelonSB : MonoBehaviour {
    public GameObject Lotus;
    public Vector3 OtherPosition;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Char" && other.gameObject != Lotus)
        {
            CharacterMove hitPlayer = other.GetComponent<CharacterMove>();
            if (hitPlayer.blocking == false)
            {
                OtherPosition = other.transform.position;
                other.transform.position = Lotus.transform.position;
                Lotus.transform.position = OtherPosition;
                Destroy(this.gameObject);
            } else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
