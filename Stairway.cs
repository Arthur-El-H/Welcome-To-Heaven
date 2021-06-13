using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairway : MonoBehaviour
{
    //References
    Game_manager gameManager;
    Player player;

    List<AbstractWaiter> waiters = new List<AbstractWaiter>();
    List<Vector2> positions = new List<Vector2>();

    private void MoveUpTo(int index) 
    {

    }

    public void RegularGotHit()
    {
        int index = player.currentPositionIndex - 1;
        //Play Hit-Animation for player and Regular
        if (waiters[index].CheckHolyness()) { gameManager.Loose(); return; }
        waiters[index].GoToHell();
        MoveUpTo(index);
        if (player.currentPositionIndex == 0) { gameManager.PlayerGotToGates(); }
    }

    public void RegularGotHandShaken()
    {
        int index = player.currentPositionIndex - 1;
        //Play Handshake-Animation for player and Regular
        if (waiters[index].CheckHolyness())
        {
            player.MoveToPos(positions[index]);
            waiters[index].MoveToPos(positions[player.currentPositionIndex]);
        }
    }
}
