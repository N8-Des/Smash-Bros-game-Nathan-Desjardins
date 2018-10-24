using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHit : MonoBehaviour
{
    public Vector3 KB;
    public int damage;
    public float AudioHitNumber = 1;

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (gameObject.transform.parent.tag == "Char")
        {
            CharacterMove player = gameObject.transform.parent.GetComponent<CharacterMove>();
            if (!player.isRight)
            {
                KB.z *= -1;
            }
            GameObject other = collider.gameObject;
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                GameObject MoneyFall = GameObject.Instantiate((GameObject)Resources.Load("YoutubeMoney"));
                MoneyFall.transform.position = this.transform.position;
                dmgCtrl.TakeAttack(damage, KB, player);
                GameObject Audio = GameObject.Instantiate((GameObject)Resources.Load("Audh" + AudioHitNumber));

            }
        }
        else if (gameObject.transform.parent.transform.rotation.eulerAngles.y == 180)
        {
            KB.z *= -1;
            GameObject other = collider.gameObject;
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                dmgCtrl.TakeAttack(damage, KB, null);
                GameObject Audio = GameObject.Instantiate((GameObject)Resources.Load("Audh" + AudioHitNumber));
            }
        }
        else
        {
            GameObject other = collider.gameObject;
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                dmgCtrl.TakeAttack(damage, KB, null);
                GameObject Audio = GameObject.Instantiate((GameObject)Resources.Load("Audh" + AudioHitNumber));
            }
        }
    }
}
