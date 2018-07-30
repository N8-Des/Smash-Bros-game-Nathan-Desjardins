using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    public List<GameObject> hitList = new List<GameObject>();
    public BaseHit dmgControl;
    public AudioSource boom;
    public void Start()
    {
        StartCoroutine(send());
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Char")
        {
            GameObject collided = other.gameObject;
            hitList.Add(collided);
            collided.SetActive(false);
        }
    }
    public IEnumerator send()
    {
        yield return new WaitForSeconds(3.5f);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(1);
        boom.Play();
        foreach (GameObject target in hitList)
        {
            dmgControl = target.GetComponent<BaseHit>();
            target.transform.position = this.transform.position;
            target.SetActive(true);
            dmgControl.takeUlt(65, new Vector3(0, 2, 0));
        }
        Destroy(gameObject);
    }
}
