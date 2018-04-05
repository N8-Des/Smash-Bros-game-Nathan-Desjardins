using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrekBase : BaseCharMove
{

    void Update()
    {
        if (moveRight == true || moveLeft == true)
        {
            anim.SetBool("isWalking", true);
        }

        if (isIdle == true)
        {
            anim.SetBool("isWalking", false);
        }
    }
    public override void BaseA()
    {
        canMove = true;
        canAttack = true;
    }
    public override void SideA()
    {
        canMove = true;
        canAttack = true;
    }
    public override void UpA()
    {
        canMove = true;
        canAttack = true;
    }
}