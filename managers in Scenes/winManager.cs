using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winManager : MonoBehaviour
{

    mainManager mainManager;

    void Start()
    {
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
    }

    public void goToStart()
    {
        mainManager.goToStart();
    }

    public void closeGame()
    {
        Application.Quit();
    }

}
