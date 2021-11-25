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

    //private async Task AllWaitersMovePhysciallyUpTo(int index) 
    //{
    //    List<Task> allTasksOfMocingWaiters = new List<Task>();
    //    int posOfLast = waiters.Count - 1;

    //    for (int i = index; i < posOfLast; i++)
    //    {    
    //        Task playerMovingToNextPos = createMoveTask(waiters[i], i);
    //        allTasksOfMocingWaiters.Add(playerMovingToNextPos);

    //        // waiter which is nearer to camera must be in according layer
    //        if (i == 15) { waiters[i].GetComponent<SpriteRenderer>().sortingOrder = 9; }
    //        if (i == 6)  { waiters[i].GetComponent<SpriteRenderer>().sortingOrder = 8; }

    //        await (playerMovingToNextPos);
    //    }
    //    Task t0 = createMoveTask(waiters[posOfLast], posOfLast);
    //    await Task.WhenAll(allTasksOfMocingWaiters.ToArray());
    //    //await (t0);
    //}


    private async Task SwapAsync(AbstractWaiter newFirst, AbstractWaiter newSecond)
    {
        int newFirstIndex = newFirst.currentPosition.index;
        int newSecondIndex = newSecond.currentPosition.index;

        waiters.RemoveAt(newFirstIndex);
        waiters.Insert(newSecondIndex, newFirst);

       // Task taskOne = createMoveTask(newFirst, newSecondIndex);
       // Task taskTwo = createMoveTask(newSecond, newFirstIndex);

        //await (taskOne);
        //await (taskTwo);
    }

    //private async Task createMoveTask (AbstractWaiter waiter, int index)
    //{
    //    //Task moving = waiter.MoveToAsync(positions[index]);  //physical Movement
    //    waiter.currentPosition.index = index; //change index-reference
    //    //await (moving);
    //}

    public void LetOneIn()
    {
        if (positionsOnStairway[0].isEmpty)
        {
            Debug.Log("noone to let in");
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
        return positionsOnStairway[currentPosition.index - 1];
    }

    public PositionOnStairway getPredecessingPosition(PositionOnStairway currentPosition)
    {
        return positionsOnStairway[currentPosition.index + 1];
    }
}
