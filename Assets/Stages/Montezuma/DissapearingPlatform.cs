using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour {
    public List<CharacterMove> PlayerList = new List<CharacterMove>();

    public void OnCollisionEnter(Collision collider)
    {
        CharacterMove playerOn = collider.gameObject.GetComponent<CharacterMove>();
        if (playerOn != null)
        {
            PlayerList.Add(playerOn);
        }
    }

    public void LeavePlayer()
    {
        foreach (CharacterMove Player in PlayerList)
        {
            Player.LeaveGround();
            PlayerList.Remove(Player);
        }
        Reset();
    }
    public void OnCollisionExit(Collision collider)
    {
        CharacterMove playerOn = collider.gameObject.GetComponent<CharacterMove>();
        bool containsItem = false;
        containsItem = PlayerList.Contains(playerOn);
        if (containsItem)
        {
            PlayerList.Remove(playerOn);
        }
    }
    public void Reset()
    {
        PlayerList.RemoveRange(0, PlayerList.Count);
    }
}
