using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class shakeAnimation : MonoBehaviour
{
    [SerializeField] GameObject shaker;
    [SerializeField] GameObject shaked;
    [SerializeField] Animator playerShake;
    [SerializeField] Animator npcShake;

    Vector2 shakerPos = new Vector2(-0.75f, .207f);
    Vector2 shakedPos = new Vector2(1.89f, .26f);
    Vector2 off = new Vector2(50f, 50f);
    mainManager mainManager;
    int shakeAnimFrames = 60;

    private void Start()
    {
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
    }

    public async Task PlayShakeAnimation()
    {
        shaker.transform.position = shakerPos;
        shaked.transform.position = shakedPos;

        playerShake.Play("playerHandshake");
        npcShake.Play("npcHandshake");

        Debug.Log("Anim started");

        int counter = 0;
        while (counter < shakeAnimFrames)
        {
            counter++;
            await Task.Yield(); ;
        }
        Debug.Log("Anim stopped");
        shaked.transform.position = off;
        shaker.transform.position = off;
    }


}
