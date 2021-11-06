using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player player;
    bool isBlocked = false; 
    public void block() { isBlocked = true; } 
    public void unblock() { isBlocked = false; Debug.Log("unblocked"); }
    public bool isInputBlocked() { return isBlocked; }

    private void Start()
    {
        player = this.GetComponent<Player>();
    }

    void Update()
    {
        if (isBlocked) { return; }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.ShakeNextsHand();
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.HitNext();
        }
    }
}
