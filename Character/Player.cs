using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractWaiter
{
    //References
    public Stairway stairway;
    public CurrentPositionManager currentPositionManager;

    public bool isMoving;


    override public async void MoveTo(PositionOnStairway nextPosition) //TODO prüfen ob hier void geht
    {
        isMoving = true;
        base.MoveTo(nextPosition);
        currentPositionManager.Actualize();
        isMoving = false;
    }
}
