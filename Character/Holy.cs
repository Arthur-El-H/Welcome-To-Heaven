using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holy : AbstractWaiter
{
    float minWaitTime = 8f;
    float maxWaitTime = 16f;

    private void Start()
    {
        MakeHoly();
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
}
