using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingMenu : MonoBehaviour
{

    public GameObject StartingSettings;



    public void levelC()
    {
        PlayerPrefs.SetString("LetteraLivello", "C");
    }

    public void levelG()
    {
        PlayerPrefs.SetString("LetteraLivello", "G");
    }

    public void levelSC()
    {
        PlayerPrefs.SetString("LetteraLivello", "SC");

    }

    public void easy()
    {
        PlayerPrefs.SetString("difficolta", "Easy");
    }

    public void medium()
    {
        PlayerPrefs.SetString("difficolta", "Medium");
    }

    public void hard()
    {
        PlayerPrefs.SetString("difficolta", "Hard");
    }

    public void options()
    {
        StartingSettings.SetActive(true);
    }

    public void back()
    {

        StartingSettings.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void RestartTutorial()
    {
        PlayerPrefs.SetInt("Level", 0);
        SceneManager.LoadScene("Tutorial");
    }
}
