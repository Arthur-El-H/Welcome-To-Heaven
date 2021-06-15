using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game_manager : MonoBehaviour
{
    //References
    public FreeSlotsManager freeSlotsManager;
    public CurrentPositionManager currentPositionManager;
    public Stairway stairway;
    public GameObject WaiterParent;

    //Prefabs
    public Player playerPre;
    public AbstractWaiter Unholy;
    public AbstractWaiter Holy;

    Player player;
    PlayerInput playerInput;

    float entryTime = 5; // Time before a new waiter gets in

    int positionCounter;
    int amountOfPositions = 26;
    int PosisitionsPerWay = 10;
    float xStep = 1.6f;
    float yStep = .38f;
    Vector2 firstPos = new Vector2(-7.3f, -4.25f);

    private void Start()
    {
        initStairway();
        StartCoroutine(lettingPeopleIn());
        currentPositionManager.Initialize(player);
    }

    IEnumerator lettingPeopleIn()
    {
        while (true)
        {
            while (true) 
            {
                if (!playerInput.getBlockStatus())
                {
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
            stairway.LetOneIn();
            yield return new WaitForSeconds(entryTime);
        }
    }

    public void Loose() { }

    public void PlayerGotToGates()
    {
        if(freeSlotsManager.GetFreeSlots() > 0) { Win(); }
    }

    private void Win()
    {
        
    }

    IEnumerator LettingPeopleIn()
    {
        while (freeSlotsManager.GetFreeSlots() > 0)
        {
            yield return new WaitForSeconds(entryTime);
            stairway.LetOneIn();
            freeSlotsManager.RemoveOneSlot();
            currentPositionManager.Actualize();
            if (freeSlotsManager.CheckLoss()) { Loose(); }
        }
        yield return null;
    }

    private void initStairway()             
    {
        Vector2 newPos;
        float currentHeight = firstPos.y;

        stairway.positions = new List<Vector2>(new Vector2[amountOfPositions]);
        stairway.waiters = new List<AbstractWaiter>(new AbstractWaiter[amountOfPositions]);
        positionCounter = amountOfPositions;
        positionCounter--;                
        CreatePlayer();

        for (int i = 1; i < PosisitionsPerWay; i++)     //First Way
        {
            currentHeight += yStep;
            newPos = new Vector2(firstPos.x + (i * xStep), currentHeight);
            stairway.positions[positionCounter] = newPos;
            stairway.waiters[positionCounter] = CreateWaiter((Vector3)newPos);
            positionCounter--;
        }

        xStep = 1.422222f;
        yStep = .2044444f;

        for(int i = 0; i <(PosisitionsPerWay-1); i++)      //Second Way To (-6/2) and from (6.8/.16)
        {
            currentHeight += yStep;
            newPos = new Vector2(firstPos.x + ((float)(9 - i) * xStep), currentHeight + .2f);
            stairway.positions[positionCounter] = newPos;
            stairway.waiters[positionCounter] = CreateWaiter((Vector3)newPos);
            positionCounter--;
        }

        xStep = 1.35f;
        yStep = .23f;

        for (int i = 2; i < (PosisitionsPerWay-1); i++)     //Third Way
        {
            currentHeight += yStep;
            newPos = new Vector2(firstPos.x + (i * xStep), currentHeight);
            stairway.positions[positionCounter] = newPos;
            stairway.waiters[positionCounter] = CreateWaiter((Vector3)newPos);
            positionCounter--;
        }
    }

    private void CreatePlayer()
    {
        Debug.Log("amount of Positions = " + amountOfPositions + " and Lists are so big: " + stairway.positions.Count);

        stairway.positions[positionCounter] = firstPos;
        player = Instantiate(playerPre, (Vector3)firstPos, Quaternion.identity);
        stairway.waiters[positionCounter] = player;
        player.currentPositionIndex = positionCounter;
        positionCounter--;
        
        player.stairway = stairway;
        stairway.player = player;
        playerInput = player.GetComponent<PlayerInput>();
        stairway.playerInput = playerInput;
    }

    private AbstractWaiter CreateWaiter(Vector3 newPos)
    {
        AbstractWaiter waiter;
        int RandomInt = Random.Range(1, 3);
        if (RandomInt == 1)
        {
            waiter = Instantiate(Holy, newPos, Quaternion.identity, WaiterParent.transform);
        }
        else
        {            
            waiter = Instantiate(Unholy, newPos, Quaternion.identity, WaiterParent.transform);
        }
        waiter.currentPositionIndex = positionCounter;
        return waiter;
    }
}
