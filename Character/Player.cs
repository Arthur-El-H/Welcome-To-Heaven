using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractWaiter
{
    //References
    public Stairway stairway;

    float stepY;
    float stepX;

    public void FlyToAnimPos() { }
    public IEnumerator Flying()
    {
        Vector2 firstTarget = new Vector2(this.transform.position.x + stepX, this.transform.position.y + stepY);
        while ((Vector2)transform.position != firstTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, firstTarget, getSpeed() * Time.deltaTime);
        }
        yield return null;
    }
    public void HitNext() 
    {
        Debug.Log("Im Hitting");
        stairway.RegularGotHit();
    }
    public void ShakeNextsHand() 
    {
        Debug.Log("Im Shaking the hand");
        stairway.RegularGotHandShaken();
    }
}
