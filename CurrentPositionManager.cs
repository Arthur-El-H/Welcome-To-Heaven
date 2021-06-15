using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPositionManager : MonoBehaviour
{
    Player player;
    int currentPosOfPlayer;

    public void Initialize(Player p)
    {
        currentPosOfPlayer = p.currentPositionIndex;
        DisplayOnView(currentPosOfPlayer);
        player = p;
    }

    public void Actualize()
    {
        currentPosOfPlayer = player.currentPositionIndex;
        DisplayOnView(currentPosOfPlayer);
    }

    private void DisplayOnView(int Pos)
    {
        Debug.Log(Pos + " is the current Pos of player.");
    }
}
