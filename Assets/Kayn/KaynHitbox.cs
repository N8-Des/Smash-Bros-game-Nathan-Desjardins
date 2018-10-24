using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaynHitbox : BasicHurtbox
{
    public KaynMovement kayn;
    public bool hasMultiplied;
    public int originalDamage;
    public void Start()
    {
        originalDamage = damage;
    }
    public override void OnTriggerEnter(Collider collider)
    {
        if (gameObject.transform.parent.tag == "Char")
        {
            CharacterMove player = gameObject.transform.parent.GetComponent<CharacterMove>();
            if (kayn.isShadowed)
            {
                if (!hasMultiplied)
                {
                    originalDamage = damage;
                    hasMultiplied = true;
                    damage = (int)(damage * 1.5f);
                }
            }
            else
            {
                damage = originalDamage;
            }
            if (!player.isRight)
            {
                if (!alreadyNegative)
                {
                    KB.z *= -1;
                }
                alreadyNegative = true;
            }
            else
            {
                KB.z = Mathf.Abs(KB.z);
                alreadyNegative = false;
            }
            GameObject other = collider.gameObject;
            BaseHit dmgCtrl = other.GetComponent<BaseHit>();
            ThanosCar thanosCar = other.GetComponent<ThanosCar>();
            if (dmgCtrl != null && dmgCtrl.transform.parent != this)
            {
                if (kayn.isRhaast)
                {
                    kayn.healRhaast();
                }
                dmgCtrl.TakeAttack(damage, KB, null);
            }
            else if (thanosCar != null)
            {
                thanosCar.Explode();
            }
        }
    }      
}
