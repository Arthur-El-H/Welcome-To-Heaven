using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("RightArrow"))
        {
            player.ShakeNextsHand();
        }

        else if (Input.GetKeyDown("LeftArrow"))
        {
            player.HitNext();
        }
    }
}
