using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class hitAnimation : MonoBehaviour
{
    [SerializeField] GameObject hitter;
    [SerializeField] GameObject hitted;
    [SerializeField] Animator hit;

    Vector2 hitterPos = new Vector2(-0.9f, .46f);
    Vector2 hittedPos = new Vector2(-1.25f, -.62f);
    Vector2 off = new Vector2(50f, 50f);
    mainManager mainManager;
    int hitAnimFrames = 20;

    private void Start()
    {
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
    }

    public async void PlayHitAnimation()
    {
        mainManager.isPaused = true;

        hitter.transform.position = hitterPos;
        hitted.transform.position = hittedPos;
        hit.Play("HitAnimation");

        Debug.Log("Anim started");

        int counter = 0;
        while (counter < hitAnimFrames)
        {
            counter++;
            await Task.Yield(); ;
        }
        Debug.Log("Anim stopped");
        hitted.transform.position = off;
        hitter.transform.position = off;

        mainManager.isPaused = false;
    }


}
