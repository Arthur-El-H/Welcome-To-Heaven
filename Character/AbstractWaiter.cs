using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWaiter : MonoBehaviour
{
    public int currentPositionIndex;
    private float speed = 1f;   public float getSpeed() { return speed; }
    float flyHeight;

    public void GoToHell() 
    {
        Debug.Log("Going to hell");
        Destroy(this);
    }

    public void GoToHeaven()
    {
        Debug.Log("Going to Heaven");
        Destroy(this);
    }

    public void MoveToPos(Vector2 Pos)
    {
        StartCoroutine(MovingToPos(Pos));
    }

    public IEnumerator MovingToPos(Vector2 Position)
    {
        Vector2 firstTarget = new Vector2(this.transform.position.x, this.transform.position.y + flyHeight);
        while ((Vector2)transform.position != firstTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, firstTarget, speed * Time.deltaTime); 
        }

        Vector2 secondTarget = new Vector2(Position.x, Position.y + flyHeight);
        while ((Vector2)transform.position != secondTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, secondTarget, speed * Time.deltaTime);
        }

        while ((Vector2)transform.position != Position)
        {
            transform.position = Vector2.MoveTowards(transform.position, Position, speed * Time.deltaTime);
        }
        yield return null;
    }

    //For regulars

    bool IAmHoly; public void MakeHoly() { IAmHoly = true; }

    public bool CheckHolyness() { return IAmHoly; }
    public void ShakeHandsBackwards() 
    {
        //play anim
    }
    public void getHit() 
    {
        //play anim
    }
}
