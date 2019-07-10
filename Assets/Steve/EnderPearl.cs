using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnderPearl : MonoBehaviour {
    public GameObject steve;
    Vector3 offset = new Vector3(0, 0.3f, 0);

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Ground")
        {
            steve.transform.position = this.transform.position + offset;
            steve.gameObject.GetComponent<BaseHit>().takeDamage(2);
            Destroy(gameObject);
        }
    }
}
