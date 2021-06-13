using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPositionManager : MonoBehaviour
{
    int currentPosOfPlayer;

    public void Actualize(int Pos)
    {
        currentPosOfPlayer = Pos;
        DisplayOnView(Pos);
    }

    private void DisplayOnView(int Pos)
    {
        Debug.Log(Pos + " is the current Pos of player.");
    }
}
