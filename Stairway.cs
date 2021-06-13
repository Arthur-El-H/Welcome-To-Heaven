using System.Collections;
using System.Collections.Generic;
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
            ChangePos(waiters[i], i);
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
            Swap(player, waiters[index]);
        }
    }

    private void ChangePos(AbstractWaiter waiter, int index)
    {
        waiter.MoveToPos(positions[index]);  //physical Movement
        waiter.currentPositionIndex = index; //change index-reference
    }

    private void Swap(AbstractWaiter newFirst, AbstractWaiter newSecond)
    {
        int newFirstIndex = newFirst.currentPositionIndex;
        int newSecondIndex = newSecond.currentPositionIndex;

        waiters.RemoveAt(newFirstIndex);
        waiters.Insert(newSecondIndex, newFirst);

        ChangePos(newFirst, newSecondIndex);
        ChangePos(newSecond, newFirstIndex);
    }

    public void LetOneIn()
    {
        AbstractWaiter waiter = waiters[0];
        waiters.RemoveAt(0);
        MovePhysciallyUpTo(0);
        waiter.GoToHeaven();
    }
}
