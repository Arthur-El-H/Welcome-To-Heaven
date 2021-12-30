using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainManager : MonoBehaviour
{
    public string lastScene;
    int winsForOne = 0, WinsForTwo = 0;
    bool musicPlaying = true;
    public bool isPaused;
    [SerializeField] AudioSource audio;

    public void resetWins()
    {
        winsForOne = 0;
        WinsForTwo = 0;
    }

    public void win(bool p1)
    {
        if (p1) { winsForOne++; }
        else { WinsForTwo++; }
    }

    public int getWinsForOne() { return winsForOne; }
    public int getWinsForTwo() { return WinsForTwo; }

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        Screen.fullScreen = true;
        Application.targetFrameRate = 30;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        goToStart();
        audio.loop = true;
    }

    public void toggleMusic()
    {
        musicPlaying = !musicPlaying;
        if (!musicPlaying) { audio.Stop(); }
        else { audio.Play(); }
    }

    public void goToStart()
    {
        SceneManager.LoadScene("Start");
    }
    public void goToLoose()
    {
        SceneManager.LoadScene("Loose");
    }
    public void goToHell()
    {
        SceneManager.LoadScene("Hell");
    }
    public void goToWin()
    {
        SceneManager.LoadScene("Win");
    }
}
