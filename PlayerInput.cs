using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player player;
    public Waiter_Manager waiterManager;

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
        if (player.isMoving) { return; }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            waiterManager.playerTriesShakingNextsHands();
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            waiterManager.playerTriesHittingNext();
        }
    }
}
