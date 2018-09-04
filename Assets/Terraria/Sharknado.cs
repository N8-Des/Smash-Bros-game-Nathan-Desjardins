using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharknado : MonoBehaviour {
    public GameObject friendlyPlayer;
    public CharacterMove controlledPlayer;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject != friendlyPlayer)
        {
            if (collider.tag == "Char")
            {
                GameObject Sharky = GameObject.Instantiate((GameObject)Resources.Load("Shark"));
                Sharky.transform.position = transform.position;
                Vector3 Delta = collider.transform.position - Sharky.transform.position;
                Rigidbody rb = Sharky.GetComponent<Rigidbody>();
                rb.velocity = Delta * 1.25f;
                Sharky.transform.LookAt(collider.transform.position);
                if (!controlledPlayer.isRight)
                {
                    Shark projectile = Sharky.GetComponent<Shark>();
                    projectile.isRight = false;
                }
            }
        }
    }
}
