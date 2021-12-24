using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Waiter_Manager : MonoBehaviour
{
    [SerializeField] AbstractWaiter Unholy;
    [SerializeField] AbstractWaiter Holy;
    [SerializeField] GameObject WaiterParent;
    [SerializeField] Game_manager gameManager;
    [SerializeField] hitAnimation hitAnimation; 
    [SerializeField] shakeAnimation shakeAnimation; 

    private mainManager mainManager;
    public List<AbstractWaiter> waiters;
    public Player player;
    float waiterLeavingTime = 1.5f;

    private void Start()
    {
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
    }

    public AbstractWaiter CreateWaiter(Vector3 newPos)
    {
        AbstractWaiter waiter;
        int RandomInt = Random.Range(1, 3);
        if (RandomInt == 1)
        {
            waiter = Instantiate(Holy, newPos, Quaternion.identity, WaiterParent.transform);
            waiter.isHoly = true;
            waiter.sprite = waiter.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        else
        {
            waiter = Instantiate(Unholy, newPos, Quaternion.identity, WaiterParent.transform);
            waiter.sprite = waiter.GetComponent<SpriteRenderer>();
        }
        waiter.mainManager = mainManager;
        return waiter;
    }

    public void playerTriesHittingNext()
    {
        PositionOnStairway positionOfWaiterToHit = player.currentPosition.getNextPosition();
        if (positionOfWaiterToHit.isEmpty || positionOfWaiterToHit.isWaiterLeaving )
        {
            player.catchUp();
            return;
        }

        AbstractWaiter waiterToHit = positionOfWaiterToHit.waiterOnPosition;

        if (waiterToHit.isHoly)
        {
            Debug.Log("should loose now");
            gameManager.Loose(false);
        }

        //Play Holy-Animation
        //Play Hit-Animation for player and Regular
        hitAnimation.PlayHitAnimation();
        waiterToHit.GoToHell();
        player.catchUp();
    }

    public async void playerTriesShakingNextsHands()
    {
        PositionOnStairway positionOfWaiterToShakeHandsTo = player.currentPosition.getNextPosition();
        if (positionOfWaiterToShakeHandsTo.isEmpty || positionOfWaiterToShakeHandsTo.isWaiterLeaving)
        {
            player.catchUp();
            return;
        }

        AbstractWaiter waiterToShakeHandsTo = positionOfWaiterToShakeHandsTo.waiterOnPosition;

        waiterToShakeHandsTo.isShaking = true;
        player.isShaking = true;

        await shakeAnimation.PlayShakeAnimation();

        if (waiterToShakeHandsTo.isHoly)
        {
            waiterToShakeHandsTo.MoveTo(player.currentPosition);
            player.MoveTo(waiterToShakeHandsTo.currentPosition);
            if (player.isLast)
            {
                player.isLast = false;
                waiterToShakeHandsTo.isLast = true;
            }            
        }
        float counter = 0.0f;
        while (counter < waiterLeavingTime)
        {
            if (!mainManager.isPaused) counter += Time.deltaTime;
            await Task.Yield();
        }
        waiterToShakeHandsTo.isShaking = false;
        player.isShaking = false;
    }
}
