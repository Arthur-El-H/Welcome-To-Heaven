using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPositionManager : MonoBehaviour
{
    Player player;
    int currentPosOfPlayer;
    [SerializeField] Text currentPos;

    public void Initialize(Player p)
    {
        currentPosOfPlayer = p.currentPosition.index;
        currentPosOfPlayer++;
        DisplayOnView(currentPosOfPlayer);
        player = p;
    }

    public void Actualize()
    {
        currentPosOfPlayer = player.currentPosition.index;
        currentPosOfPlayer++;
        DisplayOnView(currentPosOfPlayer);
    }

    private void DisplayOnView(int Pos)
    {
        currentPos.text = currentPosOfPlayer.ToString();
        Debug.Log(Pos + " is the current Pos of player.");
    }
}
