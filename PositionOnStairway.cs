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
    public mainManager mainManager;

    public IEnumerator waiterIsLeaving()
    {
        if (mainManager.isPaused) yield return new WaitForEndOfFrame();
        isWaiterLeaving = true;
        waiterOnPosition = null;

        float counter = 0.0f;
        while (counter < leavingTime)
        {
            if (!mainManager.isPaused) counter += Time.deltaTime;
            yield return null;
        }

        isEmpty = true;
        isWaiterLeaving = false;
        if (index == 26) yield break;  //checken ob es sich um letzten waiter handelt. Geht nicht über index, weil Menge der Waiter dynamisch ist
        if (getPredecessingPosition().isEmpty) yield break;
        if (getPredecessingPosition().waiterOnPosition.isShaking) yield break;

        PositionOnStairway predecessor = getPredecessingPosition();

        while (predecessor.isEmpty && predecessor.index < 25)
        {
            predecessor = predecessor.getPredecessingPosition();
            if (!predecessor.isEmpty )
            {
                predecessor.waiterOnPosition.catchUp();
            }
        }

        if (!predecessor.isEmpty)
        {
            predecessor.waiterOnPosition.catchUp();
        }
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
