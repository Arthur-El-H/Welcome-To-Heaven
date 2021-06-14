using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Stairway : MonoBehaviour
{
    //References
    public Game_manager gameManager;
    public Player player;

    public List<AbstractWaiter> waiters;
    public List<Vector2> positions;

    private void MovePhysciallyUpTo(int index) 
    {
        for (int i = index; i < waiters.Count; i++)
        {
            ChangePosAsync(waiters[i], i);
        }
    }

    public void RegularGotHit()
    {
        Debug.Log("Test 0");
        int index = player.currentPositionIndex - 1;
        //Play Holy-Animation
        //Play Hit-Animation for player and Regular
        if (waiters[index].CheckHolyness()) { player.GoToHell(); gameManager.Loose(); return; }        //Check For Win
        Debug.Log("Test 1");

        AbstractWaiter currentWaiter = waiters[index];
        Debug.Log("Test 2");
        waiters.RemoveAt(index);
        Debug.Log("Test 3");
        currentWaiter.GoToHell();
        Debug.Log("Test 4");
        MovePhysciallyUpTo(index);
        Debug.Log("Test 5");

        //if (player.currentPositionIndex == 0) { gameManager.PlayerGotToGates(); }   //Check for Win
        //Debug.Log("Test 6");
    }

    public void RegularGotHandShaken()
    {
        int index = player.currentPositionIndex - 1;
        //Play Handshake-Animation for player and Regular
        if (waiters[index].CheckHolyness())
        {
            SwapAsync(player, waiters[index]);
        }
    }

    private async Task ChangePosAsync(AbstractWaiter waiter, int index)
    {
        Task moving = waiter.MoveToAsync(positions[index]);  //physical Movement
        waiter.currentPositionIndex = index; //change index-reference
        await (moving);
    }
     
    private async void SwapAsync(AbstractWaiter newFirst, AbstractWaiter newSecond)
    {
        int newFirstIndex = newFirst.currentPositionIndex;
        int newSecondIndex = newSecond.currentPositionIndex;

        waiters.RemoveAt(newFirstIndex);
        waiters.Insert(newSecondIndex, newFirst);

        Task taskOne = ChangePosAsync (newFirst, newSecondIndex);
        Task taskTwo = ChangePosAsync (newSecond, newFirstIndex);

        //Task taskNewFirst = newFirst.MoveToAsync(positions[newSecondIndex]);
        //Task taskNewSecond = newSecond.MoveToAsync(positions[newSecondIndex]);

        await (taskOne);
        await (taskTwo);
        Debug.Log("Now I arrived");
    }

    public void LetOneIn()
    {
        AbstractWaiter waiter = waiters[0];
        waiters.RemoveAt(0);
        MovePhysciallyUpTo(0);
        waiter.GoToHeaven();
    }
}
