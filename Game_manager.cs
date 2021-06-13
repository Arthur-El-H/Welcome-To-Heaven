using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_manager : MonoBehaviour
{
    //References
    FreeSlotsManager freeSlotsManager;

    public void Loose() { }

    public void PlayerGotToGates()
    {
        if(freeSlotsManager.GetFreeSlots() > 0) { Win(); }
    }

    private void Win()
    {
        
    }
}
