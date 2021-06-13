using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_manager : MonoBehaviour
{
    //References
    FreeSlotsManager freeSlotsManager;
    CurrentPositionManager currentPositionManager;
    Stairway stairway;
    Player player;

    float entryTime; // Time before a new waiter gets in
    public void Loose() { }

    public void PlayerGotToGates()
    {
        if(freeSlotsManager.GetFreeSlots() > 0) { Win(); }
    }

    private void Win()
    {
        
    }

    IEnumerator LettingPeopleIn()
    {
        while (freeSlotsManager.GetFreeSlots() > 0)
        {
            yield return new WaitForSeconds(entryTime);
            stairway.LetOneIn();
            freeSlotsManager.RemoveOneSlot();
            currentPositionManager.Actualize(player.currentPositionIndex);
        }
        yield return null;
    }
}
