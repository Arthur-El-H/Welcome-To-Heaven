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
}
