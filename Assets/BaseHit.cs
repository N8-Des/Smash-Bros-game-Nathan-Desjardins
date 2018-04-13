using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHit : MonoBehaviour {
    public bool isKnockedBack = false;
    public float kbResist;
    public int percent = 0;
    public Rigidbody rb;
    public virtual void TakeAttack(int damage, Vector3 knockback)
    {
        isKnockedBack = true;
        //Debug.Log((knockback.y + knockback.z) * (percent / 4) * (kbResist));
        percent += damage;
        rb.velocity = knockback * (percent / 10) * kbResist;
        Invoke("stopKB", ((knockback.y + knockback.z) * (percent / 10) * (kbResist)) / 10);
    }
    void stopKB()
    {
        isKnockedBack = false;
    }
}
