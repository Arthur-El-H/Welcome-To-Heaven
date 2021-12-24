using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbstractWaiter : MonoBehaviour
{
    public PositionOnStairway currentPosition;
    public bool directionTurnedTo;
    public const bool right = true;
    public const bool left = false;
    public bool isHoly;
    public bool isLast;
    public bool isShaking;
    public mainManager mainManager;
    public SpriteRenderer sprite;
    private Vector2 ascendingPoint;
    private float speed = 1.5f;   public float getSpeed() { return speed; }
    float flyHeight = .3f;

    private void Start()
    {
        ascendingPoint = new Vector2(5.7f, 3f);
    }

    virtual public async void GoToHell() 
    {
    }
        
    virtual public async Task GoToHeaven()
    {
        if (mainManager.isPaused) await Task.Yield();

        currentPosition.clear();

        while ((Vector2)transform.position != ascendingPoint)
        {
            if (mainManager.isPaused)
            {
                await Task.Yield();
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, ascendingPoint, speed * Time.deltaTime);
                await Task.Yield();
            }
        }
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
            sprite.sortingOrder = 8;
            turnRight();
        }
        if (nextPosition.index == 16)
        {
            sprite.sortingOrder = 9;
            turnLeft();
        }


        if (currentPosition.index == 0) return;
    }

    public void turnLeft()
    {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        directionTurnedTo = left;
    }

    public void turnRight()
    {
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
        directionTurnedTo = right;
    }
}
