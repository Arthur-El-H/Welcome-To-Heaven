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

    //Prefabs
    public Player playerPre;
    public AbstractWaiter Unholy;
    public AbstractWaiter Holy;

    Player player;

    float entryTime; // Time before a new waiter gets in

    int amountOfPositions;
    int PosisitionsPerWay = 10;
    float xStep = .7f;
    float yStep = .1f;
    Vector2 firstPos = new Vector2(-8f, -4f);

    private void Start()
    {
        initStairway();
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
            currentPositionManager.Actualize(player.currentPositionIndex);
        }
        yield return null;
    }

    private void initStairway()             //Stairway positions must be of (3*x + .5x)
    {
        amountOfPositions = (int)(3.5 * PosisitionsPerWay);
        Vector2 newPos;
        float currentHeight = firstPos.y;

        stairway.positions = new List<Vector2>(new Vector2[amountOfPositions]);
        stairway.waiters = new List<AbstractWaiter>(new AbstractWaiter[amountOfPositions]);
        amountOfPositions--;                //to use as a counter
        CreatePlayer();

        for (int i = 1; i < PosisitionsPerWay; i++)     //First Way
        {
            currentHeight += yStep;
            newPos = new Vector2(firstPos.x + (i * xStep), currentHeight);
            stairway.positions[amountOfPositions-i] = newPos;
            stairway.waiters[amountOfPositions-i] = CreateWaiter((Vector3)newPos);
        }

        for(int i = 1; i < PosisitionsPerWay; i++)      //Second Way
        {
            currentHeight += yStep;
            newPos = new Vector2(firstPos.x + ((float)(10 - i) * xStep), currentHeight);
            int actualIndex = amountOfPositions - (i + PosisitionsPerWay);
            stairway.positions[actualIndex] = newPos;
            stairway.waiters[actualIndex] = CreateWaiter((Vector3)newPos);
        }

        for (int i = 1; i < PosisitionsPerWay; i++)     //Third Way
        {
            currentHeight += yStep;
            newPos = new Vector2(firstPos.x + (i * xStep), currentHeight);
            int actualIndex = amountOfPositions - (i + 2*PosisitionsPerWay);
            stairway.positions[actualIndex] = newPos;
            stairway.waiters[actualIndex] = CreateWaiter((Vector3)newPos); 
        }

        for (int i = 1; i < (PosisitionsPerWay/2); i++)      //Fourth and last (half) Way
        {
            currentHeight += yStep;
            newPos = new Vector2(firstPos.x + ((float)(10 - i) * xStep), currentHeight);
            int actualIndex = amountOfPositions - (i + 3 * PosisitionsPerWay);
            stairway.positions[actualIndex] = newPos;
            stairway.waiters[actualIndex] = CreateWaiter((Vector3)newPos);
        }
    }

    private void CreatePlayer()
    {
        Debug.Log("amount of Positions = " + amountOfPositions + " and Lists are so big: " + stairway.positions.Count);

        stairway.positions[amountOfPositions] = firstPos;
        player = Instantiate(playerPre, (Vector3)firstPos, Quaternion.identity);
        stairway.waiters[amountOfPositions] = player;
        player.currentPositionIndex = amountOfPositions;
        
        player.stairway = stairway;
        stairway.player = player;
    }

    private AbstractWaiter CreateWaiter(Vector3 newPos)
    {
        int RandomInt = Random.Range(1, 3);
        if (RandomInt == 1)
        {
            return Instantiate(Holy, (Vector3)newPos, Quaternion.identity);
        }
        else
        {
            return Instantiate(Unholy, newPos, Quaternion.identity);
        }
    }
}
