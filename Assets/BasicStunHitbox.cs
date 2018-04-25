using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStunHitbox : MonoBehaviour
{
    public float stunDuration;

    public void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        BaseCharMove Basic = other.GetComponent<BaseCharMove>();
        if (Basic != null && Basic.transform.parent != this)
        {
            Basic.takeStun(stunDuration);
        }
    }
}
