using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = this.GetComponent<Player>();
    }

    void Update()
    {
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
