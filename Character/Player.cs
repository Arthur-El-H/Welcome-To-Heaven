using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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
        while (currentPosition != nextPosition)
        {
            await Task.Yield();  
        }
        currentPositionManager.Actualize();
        isMoving = false;
    }
}
