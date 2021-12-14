using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Stairway : MonoBehaviour
{
    //References
    public Game_manager gameManager;
    public Player player;
    public PlayerInput playerInput;

    public List<AbstractWaiter> waiters;
    public List<PositionOnStairway> positionsOnStairway;

    public CurrentPositionManager currentPositionManager;
    public FreeSlotsManager freeSlotsManager;

    public void LetOneIn()
    {
        Debug.Log("About to try letting one in");
        if (positionsOnStairway[0].isEmpty)
        {
            Debug.Log("noone to let in");
            Debug.Log(positionsOnStairway[0].waiterOnPosition);
            return;
        }

        AbstractWaiter waiterEnteringHeaven = positionsOnStairway[0].waiterOnPosition;
        if (waiterEnteringHeaven == player) { gameManager.Win(); }

        StartCoroutine(positionsOnStairway[0].waiterIsLeaving());
        waiterEnteringHeaven.GoToHeaven();

        currentPositionManager.Actualize();
        freeSlotsManager.RemoveOneSlot();
    }

    public PositionOnStairway getNextPosition( PositionOnStairway currentPosition)
    {
        Debug.Log("getting position " + (currentPosition.index - 1));
        return positionsOnStairway[currentPosition.index - 1];
    }

    public PositionOnStairway getPredecessingPosition(PositionOnStairway currentPosition)
    {
        return positionsOnStairway[currentPosition.index + 1];
    }
}
