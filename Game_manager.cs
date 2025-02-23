﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game_manager : MonoBehaviour
{
    //References
    [SerializeField] FreeSlotsManager freeSlotsManager;
    [SerializeField] CurrentPositionManager currentPositionManager;
    [SerializeField] Stairway stairway;
    [SerializeField] Waiter_Manager waiterManager;
    mainManager mainManager;

    //Prefabs
    public Player playerPre;

    Player player;
    PlayerInput playerInput;

    float timeBetweenEntries = 12f; // Time before a new waiter gets in
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
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
        InitStairway();
        SetWaitersLayers();
        StartCoroutine(lettingPeopleIn());
        currentPositionManager.Initialize(player);
        waiterManager.player = player;
    }

    IEnumerator lettingPeopleIn()
    {
        float counter = 0.0f;
        while (counter < timeToWaitBeforeFirstEntry)
        {
            if (!mainManager.isPaused) counter += Time.deltaTime;
            yield return null;
        }

        while (true)
        {
            if (playerInput.isInputBlocked())
            {
                counter = 0.0f;
                while (counter < maxTimeOfInputBlock)
                {
                    if (!mainManager.isPaused) counter += Time.deltaTime;
                    yield return null;
                }
            }
            stairway.LetOneIn();
            if (freeSlotsManager.CheckLoss()) { Loose(true); break; }
            counter = 0.0f;
            while (counter < timeBetweenEntries)
            {
                if (!mainManager.isPaused) counter += Time.deltaTime;
                yield return null;
            }            
        }
    }

    public void Loose(bool full) 
    {
        if (full)
        {
            mainManager.goToLoose();
        }

        else
        {
            mainManager.goToHell();
        }

    }
    public void Win()
    {
        mainManager.goToWin();
    }

    public void PlayerGotToGates()
    {
        if(freeSlotsManager.GetFreeSlots() > 0) { Win(); }
    }

    private void SetWaitersLayers()
    {
        for (int i = 24; i > 0; i--)           
        {
            if(i > 16)
            {
                stairway.positionsOnStairway[i].waiterOnPosition.sprite.sortingOrder = 10;
            }

            else if( i > 7)
            {
                stairway.positionsOnStairway[i].waiterOnPosition.sprite.sortingOrder = 9;
            }

            else
            {
                stairway.positionsOnStairway[i].waiterOnPosition.sprite.sortingOrder = 8;
            }
        }
    }

    private void InitStairway()
    {
        Vector2 newPos;
        float currentHeight = firstPos.y;

        stairway.positionsOnStairway = new List<PositionOnStairway>(new PositionOnStairway[amountOfPositions]);
        positionCounter = amountOfPositions - 1;
        createPositionOnStairway(firstPos, asPlayer);
        positionCounter--;

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
        nextPositionOnStairway.mainManager = mainManager;
        nextPositionOnStairway.stairway = stairway;
        nextPositionOnStairway.coordinates = coordinates;
        nextPositionOnStairway.index = positionCounter; //doppelt sortiert --> in positionsOnStairway und über index der Positions 
        stairway.positionsOnStairway[positionCounter] = nextPositionOnStairway;        
        if (waiterSpecification == asWaiter)
        {
            AbstractWaiter waiter = waiterManager.CreateWaiter((Vector3)coordinates);

            if (positionCounter > 16)
            {
                waiter.sprite.sortingOrder = 10;
                waiter.turnRight();
            }
            else if (positionCounter > 7)
            {
                waiter.sprite.sortingOrder = 9;
                waiter.turnLeft();
            }
            else 
            {
                waiter.sprite.sortingOrder = 8;
                waiter.turnRight();
            }
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

    private void initPlayer() //TODO: Möglicherweise in Waiter_manager passender
    {
        player.stairway = stairway;
        player.currentPositionManager = currentPositionManager;
        stairway.player = player;
        player.isLast = true;
        playerInput = player.GetComponent<PlayerInput>();
        stairway.playerInput = playerInput;
        playerInput.waiterManager = waiterManager;
        player.mainManager = mainManager;
        player.sprite = player.GetComponent<SpriteRenderer>();
        player.turnRight();
    }
}
