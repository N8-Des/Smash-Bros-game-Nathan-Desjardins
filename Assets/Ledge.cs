using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour {
    public GameObject pullLoc;
    public bool isRight;
    public void OnTriggerEnter(Collider collider)
    {
        CharacterMove character = collider.GetComponent<CharacterMove>();
        if (character != null)
        {
            Debug.Log("Gottem");
            character.grabLedge(transform.position, isRight);
        }
    }
}
