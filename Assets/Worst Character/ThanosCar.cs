using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanosCar : MonoBehaviour {
    public bool isRight = true;
    public Rigidbody rb;
    public FriendlyHitbox friendControl;
    public WorstMovement spawner;
    public void moving()
    {
        if (isRight)
        {
            rb.velocity = new Vector3(0, 0, 3f);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, -3f);
        }
    }
    void Update()
    {
        if (rb.velocity.z == 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter(Collision collider)
    {
        if (collider.collider.tag == "Char")
        {
            if (collider.gameObject != friendControl.friend)
            {
                GameObject Explosion = GameObject.Instantiate((GameObject)Resources.Load("Explode"));
                Explosion.transform.position = this.transform.position;
                BasicHurtbox hurtbox = Explosion.GetComponentInChildren<BasicHurtbox>();
                hurtbox.damage = 9;
                hurtbox.KB = new Vector3(0, 0.4f, 0.4f);
                Destroy(gameObject);
            }
        }
    }
    public void Explode()
    {
        GameObject Explosion = GameObject.Instantiate((GameObject)Resources.Load("CosmeticExplosion"));
        Explosion.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }
    public void OnDestroy()
    {
        spawner.CarOut = false;
    }
}
