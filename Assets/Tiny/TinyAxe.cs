using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyAxe : UltHurtbox {
    public GameObject Tiny;
    public bool returning;
    public Rigidbody rb;
    public Vector3 speed;
    public Vector3 tinyPos;
    public void Start()
    {
        StartCoroutine(turnAround());
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Char" && other.gameObject != Tiny)
        {
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                dmgCtrl.takeUlt(damage, KB);
            }
        }
    }
    public void Update()
    {
        tinyPos = Tiny.transform.position;
        Vector3 position = transform.position;
        if (!returning)
        {
            rb.velocity = speed;
        } else
        {
            Vector3 Delta = tinyPos - position;
            float fast = 6 * Time.deltaTime;
            Vector3 zMove = Vector3.zero;
            zMove.z = Mathf.Lerp(position.z, tinyPos.z, fast);
            zMove.y = Mathf.Lerp(position.y, tinyPos.y, fast);
            zMove.x = position.x;
            gameObject.transform.localPosition = zMove;
            float threshold = Mathf.Abs(Delta.z);
            if (threshold <= 0.7f)
            {
                TinyMovement friend = Tiny.GetComponent<TinyMovement>();
                friend.grabbed();
                Destroy(this.gameObject); 
            }
        }
    }
    public void OnDestroy()
    {
        TinyMovement friend = Tiny.GetComponent<TinyMovement>();
        friend.canThrow = true;
    }
    public IEnumerator turnAround()
    {
        yield return new WaitForSeconds(1);
        returning = true;
    }
}
