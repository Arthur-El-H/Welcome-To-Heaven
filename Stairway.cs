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
    public List<Vector2> positions;

    private async Task AllWaitersMovePhysciallyUpTo(int index) 
    {
        for (int i = index; i < waiters.Count; i++)
        {
            Task t = ChangePosAsync(waiters[i], i);
            await(t);
        }
    }

    public async void RegularGotHit()
    {
        int index = player.currentPositionIndex - 1;
        //Play Holy-Animation
        //Play Hit-Animation for player and Regular
        if (waiters[index].CheckHolyness()) { player.GoToHell(); gameManager.Loose(); return; }        //Check For Win

        AbstractWaiter currentWaiter = waiters[index];
        waiters.RemoveAt(index);
        currentWaiter.GoToHell();

        playerInput.block();
        Task t = AllWaitersMovePhysciallyUpTo(index);
        await (t);
        playerInput.unblock();

        //if (player.currentPositionIndex == 0) { gameManager.PlayerGotToGates(); }   //Check for Win
    }

    public async void RegularGotHandShaken()
    {
        int index = player.currentPositionIndex - 1;
        playerInput.block();
        //Play Handshake-Animation for player and Regular
        if (waiters[index].CheckHolyness())
        {
            Task t = SwapAsync(player, waiters[index]);
            await (t);
        }
        // await (anim);
        playerInput.unblock();
    }

    private async Task SwapAsync(AbstractWaiter newFirst, AbstractWaiter newSecond)
    {
        int newFirstIndex = newFirst.currentPositionIndex;
        int newSecondIndex = newSecond.currentPositionIndex;

        waiters.RemoveAt(newFirstIndex);
        waiters.Insert(newSecondIndex, newFirst);

        Task taskOne = ChangePosAsync (newFirst, newSecondIndex);
        Task taskTwo = ChangePosAsync (newSecond, newFirstIndex);

        await (taskOne);
        await (taskTwo);
    }

    private async Task ChangePosAsync(AbstractWaiter waiter, int index)
    {
        Task moving = waiter.MoveToAsync(positions[index]);  //physical Movement
        waiter.currentPositionIndex = index; //change index-reference
        await (moving);
        Debug.Log("Now I arrived");
    }

    public void LetOneIn()
    {
        AbstractWaiter waiter = waiters[0];
        waiters.RemoveAt(0);
        AllWaitersMovePhysciallyUpTo(0);
        waiter.GoToHeaven();
    }
}
