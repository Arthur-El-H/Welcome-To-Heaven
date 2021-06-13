using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWaiter : MonoBehaviour
{
    public int currentPositionIndex;

    public void GoToHell() 
    {

    }

    public void GoToHeaven()
    {

    }

    public void MoveToPos(Vector2 Position)
    {

    }


    //For regulars

    bool IAmHoly;

    public bool CheckHolyness() { return IAmHoly; }
    public void ShakeHandsBackwards() { }
    public void getHit() { }
}
