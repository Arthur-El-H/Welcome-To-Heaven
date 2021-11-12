using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOnStairway
{
    [SerializeField] private Stairway stairway;
    public float leavingTime = 1.5f;

    public Vector2 coordinates;
    public int index;
    public AbstractWaiter waiterOnPosition;
    public bool isEmpty = false;

    public IEnumerator waiterIsLeaving()
    {
        yield return new WaitForSeconds(leavingTime);
        isEmpty = true;
        waiterOnPosition = null;

        Debug.Log(index + "is empty now");
    }

    public PositionOnStairway getNextPosition()
    {
        return stairway.getNextPosition(this);
    }
}
