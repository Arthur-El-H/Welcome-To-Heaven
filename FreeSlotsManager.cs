using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeSlotsManager : MonoBehaviour
{
    int currentFreeSlotsInHeaven = 12;
    [SerializeField] Text freeSlotsNumber;
    private const bool loss = true;
    private const bool win  = false;


    private void Start()
    {
        DisplayOnView(currentFreeSlotsInHeaven);
    }
    public bool CheckLoss()
    {
        if(currentFreeSlotsInHeaven == 0) { return loss; }
        else { return win; }
    }

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
        freeSlotsNumber.text = currentFreeSlotsInHeaven.ToString();
        Debug.Log(slots + " slots still available");
    }
}
