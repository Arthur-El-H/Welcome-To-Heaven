using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Holy : AbstractWaiter
{
    float minWaitTime = 8f;
    float maxWaitTime = 16f;
    float ascendAnimTime = 7f;
    [SerializeField] Animator anim;
    
    private void Awake()
    {
        isHoly = true;
        StartCoroutine(showHoliness());
    }

    private IEnumerator showHoliness()
    {
        while (true)
        {
            yield return new WaitForSeconds(GetWaitTime());
            //play Holy-anim
        }
    }

    private float GetWaitTime()
    {
        return Random.Range(minWaitTime, maxWaitTime);
    }

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

    override public async Task GoToHeaven()
    {
        await base.GoToHeaven();
        Debug.Log("going to heaven now");
        anim.Play("npcAscend");
        Vector2 target = (Vector2)transform.position + Vector2.up * 8;
        float counter = 0.0f;
        while (counter < ascendAnimTime)
        {
            if (!mainManager.isPaused)
            {
                counter += Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target, 1.2f * Time.deltaTime);
                Debug.Log("moving toward heaven");
            }
            await Task.Yield();
        }
        Destroy(this.gameObject);

    }

}
