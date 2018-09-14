using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouKnow : MonoBehaviour {
    public BaseHit playerHit;
    public WorstMovement PeterDinklage;
    public HadToHitbox hurtbox;
    public void HitOne()
    {
        playerHit.takeDamage(6);
    }
    public void HitTwo()
    {
        playerHit.takeDamage(9);
    }
    public void HitThree()
    {
        playerHit.takeDamage(7);

    }
    public void HitFour()
    {
        playerHit.takeDamage(10);

    }
    public void HitFive()
    {
        playerHit.takeDamage(7);

    }
    public void HitSix()
    {
        hurtbox.takeHit(playerHit);
    }
}
