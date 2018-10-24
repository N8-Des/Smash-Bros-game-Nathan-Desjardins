using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStunHitbox : MonoBehaviour
{
    public float stunDuration;

    public void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        CharacterMove Basic = other.GetComponent<CharacterMove>();
        if (Basic != null && Basic.transform.parent != this)
        {
            Basic.takeStun(stunDuration);
        }
    }
}
