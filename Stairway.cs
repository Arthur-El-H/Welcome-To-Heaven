﻿using System.Collections;
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

    public CurrentPositionManager currentPositionManager;
    public FreeSlotsManager freeSlotsManager;

    int indexToTurnLeft = 10;
    int indexToTurnRight = 18;

    private async Task AllWaitersMovePhysciallyUpTo(int index) 
    {
        List<Task> tasks = new List<Task>();
        int posOfLast = waiters.Count - 1;

        for (int i = index; i < posOfLast; i++)
        {    
            Task t = ChangePosAsync(waiters[i], i);
            tasks.Add(t);
            if (i == 15) { waiters[i].GetComponent<SpriteRenderer>().sortingOrder = 9; }
            if (i == 6)  { waiters[i].GetComponent<SpriteRenderer>().sortingOrder = 8; }
        }
        Task t0 = ChangePosAsync(waiters[posOfLast], posOfLast);
        await Task.WhenAll(tasks.ToArray());
        //await (t0);
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
        Debug.Log("unblock");

        currentPositionManager.Actualize();

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
            currentPositionManager.Actualize();
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
    }

    public async void LetOneIn()
    {
        playerInput.block();
        AbstractWaiter waiter = waiters[0];

        if (waiter == player) { gameManager.Win(); }

        waiters.RemoveAt(0);
        Task t = AllWaitersMovePhysciallyUpTo(0);
        waiter.GoToHeaven();
        await (t);

        currentPositionManager.Actualize();
        freeSlotsManager.RemoveOneSlot();

        playerInput.unblock();
    }
}
