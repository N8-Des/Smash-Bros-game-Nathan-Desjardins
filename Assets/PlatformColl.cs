using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColl : MonoBehaviour
{
    public Collider disabledBoy;
    public Collider characterColl;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Char")
        {
            characterColl = other.GetComponent<Collider>();
            Physics.IgnoreCollision(characterColl, disabledBoy, true);
            //disabledBoy.isTrigger = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Char")
        {
            Physics.IgnoreCollision(characterColl, disabledBoy, false);
            //disabledBoy.isTrigger = false;
        }
    }
}