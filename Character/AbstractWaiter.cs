using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbstractWaiter : MonoBehaviour
{
    public int currentPositionIndex;
    private float speed = .5f;   public float getSpeed() { return speed; }
    float flyHeight = .3f;

    public void GoToHell() 
    {
        Debug.Log("Going to hell");
        Destroy(this.gameObject);
    }

    public void GoToHeaven()
    {
        Debug.Log("Going to Heaven");
        Destroy(this.gameObject);
    }

    public async Task MoveToAsync(Vector2 Pos)
    {

        Vector2 firstTarget = new Vector2(this.transform.position.x, this.transform.position.y + flyHeight);
        while ((Vector2)transform.position != firstTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, firstTarget, speed * Time.deltaTime);
            await Task.Yield();
        }

        Vector2 secondTarget = new Vector2(Pos.x, Pos.y + flyHeight);
        while ((Vector2)transform.position != secondTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, secondTarget, speed * Time.deltaTime);
            await Task.Yield();
        }

        while ((Vector2)transform.position != Pos)
        {
            transform.position = Vector2.MoveTowards(transform.position, Pos, speed * Time.deltaTime);
            await Task.Yield();
        }
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
