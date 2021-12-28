using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Unholy : AbstractWaiter
{
    [SerializeField] Animator anim;

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

    override public async void GoToHell()
    {
        while (mainManager.isPaused)
        {
            await Task.Yield();
        }

        currentPosition.clear();

        anim.Play("npcDescend");
        List<Vector2> parabel = getParabel();
        grow(parabel.Count);
        await flyParabel(parabel);

        Destroy(this.gameObject);
    }

    private List<Vector2> getParabel()
    {
        List<Vector2> parabel = new List<Vector2>();

        float xStartPoint = -.4f;
        float step = .08f;
        float x;
        float y;
        float a = Random.Range(1.5f, 2.3f);
        Vector2 parabelPoint;

        x = xStartPoint - step;
        for (int i = 0; i < 1200; i++)
        {
            x += step;
            y = -a * (x * x);
            parabelPoint.x = x;
            parabelPoint.y = y;
            parabel.Add(parabelPoint);
        }

        return parabel;
    }

    private async Task flyParabel(List<Vector2> parabel)
    {
        Vector2 start = transform.position;
        if(directionTurnedTo = right)
        {
            for (int i = 0; i < parabel.Count; i++)
            {
                transform.position = start + parabel[i];
                await Task.Yield();
            }
        }
        else
        {
            for (int i = 0; i < parabel.Count; i++)
            {
                Vector2 nextPos = new Vector2(-parabel[i].x, parabel[i].y);
                transform.position = start + nextPos;
                await Task.Yield();
            }
        }

    }
    private async Task grow(int frames)
    {
        sprite.sortingOrder = 14;
        float minGrowth = 0;
        float maxGrowth = .1f;
        float growthParam = Random.Range(minGrowth, maxGrowth);
        Vector3 growth = new Vector3(growthParam, growthParam, 1f);
        for (int i = 0; i < frames; i++)
        {
            transform.localScale += growth;
            await Task.Yield();
        }
    }

    override public async Task GoToHeaven()
    {
        GoToHell();
    }

}
