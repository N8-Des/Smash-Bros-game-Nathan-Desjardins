using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrekBase : BaseCharMove {
    public Animator anim;
    public void OnEnable()
    {
        anim = gameObject.GetComponent<Animator>();  
    }
	void Update () {
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
