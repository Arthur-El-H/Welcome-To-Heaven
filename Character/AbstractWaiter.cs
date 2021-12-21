using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbstractWaiter : MonoBehaviour
{
    public PositionOnStairway currentPosition;
    public bool isLast;
    public mainManager mainManager;
    private float speed = 1.5f;   public float getSpeed() { return speed; }
    float flyHeight = .3f;


    public async void GoToHell() 
    {
        if (mainManager.isPaused)
        {
            await Task.Yield();
        }

        Debug.Log("Going to hell");
        currentPosition.clear();
        Destroy(this.gameObject);
    }

    public async void GoToHeaven()
    {
        if (mainManager.isPaused) await Task.Yield();

        currentPosition.isEmpty = true;
        currentPosition.waiterOnPosition = null;
        PositionOnStairway predecessor = currentPosition.getPredecessingPosition();

        if (predecessor.isEmpty) return;
        Debug.Log("Going to Heaven");
        Destroy(this.gameObject);
    }

    public async void catchUp()
    {
        if (mainManager.isPaused)
        {
            await Task.Yield();
        }

        PositionOnStairway nextPosition = currentPosition.getNextPosition();
        MoveTo(nextPosition);
        if(isLast)
        {
            currentPosition.isEmpty = true;
            currentPosition.waiterOnPosition = null;
            return;
        }
        StartCoroutine(currentPosition.waiterIsLeaving());
    }

    virtual public async void MoveTo(PositionOnStairway nextPosition) //TODO prüfen ob hier void geht
    {
        Vector2 Pos = nextPosition.coordinates;
        Vector2 firstTarget = new Vector2(this.transform.position.x, this.transform.position.y + flyHeight);
        while ((Vector2)transform.position != firstTarget)
        {
            if (mainManager.isPaused)
            {
                await Task.Yield();
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, firstTarget, speed * Time.deltaTime);
                await Task.Yield();
            }            
        }

        Vector2 secondTarget = new Vector2(Pos.x, Pos.y + flyHeight);
        while ((Vector2)transform.position != secondTarget)
        {
            if (mainManager.isPaused)
            {
                await Task.Yield();
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, secondTarget, speed * Time.deltaTime);
                await Task.Yield();
            }
            
        }

        while ((Vector2)transform.position != Pos)
        {
            if (mainManager.isPaused)
            {
                await Task.Yield();
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Pos, speed * Time.deltaTime);
                await Task.Yield();
            }
        }
        
        Debug.Log("a waiter succesfully catched up");

        currentPosition = nextPosition;
        currentPosition.waiterOnPosition = this;
        currentPosition.isEmpty = false;

        if (nextPosition.index == 7)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 8;
        }
        if (nextPosition.index == 16)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 9;
        }


        if (currentPosition.index == 0) return;
        if (currentPosition.getNextPosition().isEmpty)
        {
            catchUp();
        }
    }

    public void turnLeft()
    {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    public void turnRight()
    {
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    //For regulars TODO: In Regular klasse!

    public bool isHoly;

    public void ShakeHandsBackwards() 
    {
        //play anim
    }
    public void getHit() 
    {
        //play anim
    }
}
