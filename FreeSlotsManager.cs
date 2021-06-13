using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeSlotsManager : MonoBehaviour
{
    int currentFreeSlotsInHeaven;

    public int GetFreeSlots()
    {
        return currentFreeSlotsInHeaven;
    }

    public void RemoveOneSlot()
    {
        currentFreeSlotsInHeaven--;
        DisplayOnView(currentFreeSlotsInHeaven);
    }

    private void DisplayOnView(int slots)
    {
        Debug.Log(slots + " slots still available");
    }
}
