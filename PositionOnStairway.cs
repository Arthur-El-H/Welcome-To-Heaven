using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOnStairway
{
    public Stairway stairway;
    public float leavingTime = 1.5f;

    public Vector2 coordinates;
    public int index;
    public AbstractWaiter waiterOnPosition;
    public bool isEmpty = false;
    public bool isWaiterLeaving = false;

    public IEnumerator waiterIsLeaving()
    {
        isWaiterLeaving = true;
        yield return new WaitForSeconds(leavingTime);
        isEmpty = true;
        isWaiterLeaving = false;
        waiterOnPosition = null;
        if (index == 26) yield break;  //checken ob es sich um letzten waiter handelt. Geht nicht über index, weil Menge der Waiter dynamisch ist

        PositionOnStairway predecessor = getPredecessingPosition();
        if (!predecessor.isEmpty)
        {
            Debug.Log("Predecessor catching up. Predecessor: " + predecessor.waiterOnPosition.currentPosition.index);
            predecessor.waiterOnPosition.catchUp();
        }
        Debug.Log(index + "is empty now");
    }

    internal void clear()
    {
        waiterOnPosition = null;
        isEmpty = true;
    }

    public PositionOnStairway getNextPosition()
    {
        return stairway.getNextPosition(this);
    }

    public PositionOnStairway getPredecessingPosition()
    {
        return stairway.getPredecessingPosition(this);
    }
}
