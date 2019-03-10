using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaxterBall : MonoBehaviour {
    public int rng;
    public Animator anim;
    public BaxterStage stage;
    public void OnTriggerEnter(Collider other)
    {
        CharacterMove character = other.GetComponent<CharacterMove>();
        if (character != null)
        {
            rng = Random.Range(1, 5);
            anim.SetTrigger("BaxterOut");
            switch (rng) {
                case 1:
                    stage.switchFlood();
                    stage.Invoke("despawnUI", 2);
                    stage.Invoke("switchFlood", 6);
                    break;
                case 2:
                    stage.switchLag();
                    stage.Invoke("despawnUI", 0.2f);
                    stage.Invoke("switchLag", 1);
                    break;
                case 3:
                    stage.switchVape();
                    stage.Invoke("switchVape", 6);
                    break;
                case 4:
                    stage.switchHole();
                    stage.Invoke("despawnUI", 2);
                    stage.Invoke("switchHole", 8);
                    break;
                case 5:
                    Debug.Log("YEET");
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
