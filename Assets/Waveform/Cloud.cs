using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
    public CharacterMove friendly; //very important to set up correctly, use this when starting anim to make sure the thunder hurtbox won't damage summoner
    public FriendlyHitbox hurtbox;
    public Animator anim;
    public void Start()
    {
        hurtbox.friend = friendly.gameObject;
    }
    public void SendThunder()
    {
        anim.SetTrigger("Thunder");
    }
    public void die() //for the animation to trigger upon finishing
    {
        Destroy(gameObject);
    }
}
