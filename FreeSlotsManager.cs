using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeSlotsManager : MonoBehaviour
{
    int currentFreeSlotsInHeaven = 15;
    [SerializeField] Text freeSlotsNumber;

    private void Start()
    {
        DisplayOnView(currentFreeSlotsInHeaven);
    }
    public bool CheckLoss()
    {
        if(currentFreeSlotsInHeaven == 0) { return true; }
        else { return false; }
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
