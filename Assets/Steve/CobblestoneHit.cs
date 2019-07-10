using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobblestoneHit : BaseHit {
    int health = 20;
    public SteveMovement steve;
    void takeAttack(int damage, Vector3 knockback, CharacterMove attacker)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        steve.numBlocks -= 1;
    }
}
