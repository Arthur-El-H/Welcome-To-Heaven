using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class options_Manager : MonoBehaviour
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

    public void toggleMusic()
    {
        mainManager.toggleMusic();
    }

}
