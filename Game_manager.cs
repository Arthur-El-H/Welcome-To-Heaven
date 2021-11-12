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

    float timeBetweenEntries = 8f; // Time before a new waiter gets in
    float timeToWaitBeforeFirstEntry = 3f;
    float maxTimeOfInputBlock = 2f;

    int positionCounter;
    int amountOfPositions = 26;
    int PosisitionsPerWay = 10;
    float xStep = 1.6f;
    float yStep = .38f;
    Vector2 firstPos = new Vector2(-7.3f, -4.25f);

    const bool asPlayer = true;
    const bool asWaiter = false;

    private void Start()
    {
        InitStairway();
        SetWaitersLayers();
        StartCoroutine(lettingPeopleIn());
        currentPositionManager.Initialize(player);
    }

    IEnumerator lettingPeopleIn()
    {
        yield return new WaitForSeconds(timeToWaitBeforeFirstEntry); 

        while (true)
        {
            if (playerInput.isInputBlocked())
            {
                yield return new WaitForSeconds(maxTimeOfInputBlock);
            }

            //while (true) 
            //{
            //    if (!playerInput.isInputBlocked()) //Eigentlich sollte ich hier gucken, ob alle angekommen sind.
            //    {
            //        yield return new WaitForSeconds(maxTimeOfInputBlock);
            //        break;
            //    }
            //    yield return new WaitForEndOfFrame();
            //}

            stairway.LetOneIn();
            if (freeSlotsManager.CheckLoss()) { Loose(); break; }
            yield return new WaitForSeconds(timeBetweenEntries);
        }
    }

    public void Loose() 
    {
        Debug.Log("You Loose");
    }

    public void PlayerGotToGates()
    {
        if(freeSlotsManager.GetFreeSlots() > 0) { Win(); }
    }

    public void Win()
    {
        Debug.Log("You Win");
    }

    private void SetWaitersLayers()
    {
        for (int i = 24; i > 0; i--)           
        {
            if(i > 16)
            {
                stairway.positionsOnStairway[i].waiterOnPosition.GetComponent<SpriteRenderer>().sortingOrder = 10;
            }

            else if( i > 7)
            {
                stairway.positionsOnStairway[i].waiterOnPosition.GetComponent<SpriteRenderer>().sortingOrder = 9;
            }

            else
            {
                stairway.positionsOnStairway[i].waiterOnPosition.GetComponent<SpriteRenderer>().sortingOrder = 8;
            }
        }
    }

    private void InitStairway()             
    {
        Vector2 newPos;
        float currentHeight = firstPos.y;

        stairway.positionsOnStairway = new List<PositionOnStairway>(new PositionOnStairway[amountOfPositions]);
        positionCounter = amountOfPositions - 1;
        positionCounter--;
        createPositionOnStairway(firstPos, asPlayer);

        for (int i = 1; i < PosisitionsPerWay; i++)     //First Way
        {
            currentHeight += yStep;
            newPos = new Vector2(firstPos.x + (i * xStep), currentHeight);
            createPositionOnStairway(newPos, asWaiter);
            positionCounter--;
        }

        xStep = 1.422222f;
        yStep = .2044444f;

        for(int i = 0; i <(PosisitionsPerWay-1); i++)      //Second Way To (-6/2) and from (6.8/.16)
        {
            currentHeight += yStep;
            newPos = new Vector2(firstPos.x + ((float)(9 - i) * xStep), currentHeight + .2f);
            createPositionOnStairway(newPos, asWaiter);
            positionCounter--;
        }

        xStep = 1.35f;
        yStep = .23f;

        for (int i = 2; i < (PosisitionsPerWay-1); i++)     //Third Way
        {
            currentHeight += yStep;
            newPos = new Vector2(firstPos.x + (i * xStep), currentHeight);
            createPositionOnStairway(newPos, asWaiter);
            positionCounter--;
        }
    }

    private void createPositionOnStairway(Vector2 coordinates, bool waiterSpecification)
    {
        PositionOnStairway nextPositionOnStairway = new PositionOnStairway();
        nextPositionOnStairway.stairway = stairway;
        nextPositionOnStairway.coordinates = coordinates;
        nextPositionOnStairway.index = positionCounter; //doppelt sortiert --> in positionsOnStairway und über index der Positions 
        stairway.positionsOnStairway[positionCounter] = nextPositionOnStairway;
        if (waiterSpecification == asWaiter)
        {
            AbstractWaiter waiter = CreateWaiter((Vector3)coordinates);
            nextPositionOnStairway.waiterOnPosition = waiter;
            waiter.currentPosition = nextPositionOnStairway;
        }
        if (waiterSpecification == asPlayer)
        {
            player = Instantiate(playerPre, (Vector3)firstPos, Quaternion.identity);
            nextPositionOnStairway.waiterOnPosition = player;
            player.currentPosition = nextPositionOnStairway;
            initPlayer();
        }

    }

    private void initPlayer()
    {
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
        return waiter;
    }

    public void Update()
    {
        //for (int i = 1; i < amountOfPositions-1; i++)
        //{
        //    PositionOnStairway positionToCheck = stairway.positionsOnStairway[i];
        //    PositionOnStairway predecessorPosition = stairway.positionsOnStairway[i-1];

        //    if (!positionToCheck.isEmpty) return;
        //    if (predecessorPosition.isEmpty) return;
        //    predecessorPosition.waiterOnPosition.catchUp();
        //}
    }
}
