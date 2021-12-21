using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Unholy : AbstractWaiter
{
    override public async void MoveTo(PositionOnStairway nextPosition) 
    {
        base.MoveTo(nextPosition);
        while (currentPosition != nextPosition)
        {
            // waiting for moveto to finish
            await Task.Yield();
        }
        if (currentPosition.index == 0) return;
        if (currentPosition.getNextPosition().isEmpty)
        {
            catchUp();
        }
    }

    override public async Task GoToHeaven()
    {
        await base.GoToHeaven();
        Destroy(this.gameObject);
    }

}
