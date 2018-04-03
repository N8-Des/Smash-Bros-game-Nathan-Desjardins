using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrekBase : BaseCharMove {
    
	void Update () {
        if (baseA)
        {
            anim.SetTrigger("NeutA");
            baseA = false;
        }
        if (rightA == true)
        {
            anim.SetTrigger("RightA");
            rightA = false;
        }
        if (moveRight == true || moveLeft == true)
        {
            anim.SetBool("isWalking", true);
        }

        if (isIdle == true)
        {
            anim.SetBool("isWalking", false);
        }
    }
}
