using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class rulesManager : MonoBehaviour
{
    mainManager mainManager;

    private void Awake()
    {
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
    }

    public void backToLastScene()
    {
        SceneManager.LoadScene(mainManager.lastScene);
    }
}
