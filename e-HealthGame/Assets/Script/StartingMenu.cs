using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingMenu : MonoBehaviour
{

    public GameObject StartingSettings;
    public GameObject pausemenu;
    public GameObject pause;
    public GameObject bird;
    public GameObject tree;
    public GameObject transition;
    public GameObject endtransition;
    public GameObject privacy;

    AsyncOperation async;
    AsyncOperation asyncTutorial;
    AsyncOperation asyncfirst;

    public GameObject camera_;
    //int caricato = 0;

    public void Awake()
    {
        
    }

    public void Start()
    {
        if (PlayerPrefs.GetInt("PrivacyPolicy", 0) == 0)
        {
            privacy.SetActive(true);
        }

        if (ButtonsTutorial.checktrans == false || buttons.checktrans2 == false)
        {
            endtransition.SetActive(true);
        }

    }

    public void Update()
    { 
        /*print("update");
        if (caricato == 0)
        {
            async = SceneManager.LoadSceneAsync("SampleScene");
            async.allowSceneActivation = false;
            caricato = 1;      
        }*/
    }

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

        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            Debug.Log("First Time Opening");
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
            asyncfirst = SceneManager.LoadSceneAsync("Tutorial");
            asyncfirst.allowSceneActivation = false;
            StartCoroutine(loadFirstTutorial());


        }
        else
        {
            PlayerPrefs.SetInt("scoreLevel", 0);
            PlayerPrefs.SetInt("Level", 0);
            //Destroy(bird.gameObject);
            //StopCoroutine(bird.GetComponent<SpeakBird>().ShowText());
            bird.SetActive(false);
            tree.SetActive(false);
            //async.allowSceneActivation = true;
            async = SceneManager.LoadSceneAsync("SampleScene");
            async.allowSceneActivation = false;
            StartCoroutine(loadGame());
        }
    }

    public void RestartTutorial()
    {
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("scoreLevel", 0);
        asyncTutorial = SceneManager.LoadSceneAsync("Tutorial");
        asyncTutorial.allowSceneActivation = false;
        StartCoroutine(loadTutorial());
    }

    public void Pause()
    {
        pause.SetActive(false);
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausemenu.SetActive(false);
        pause.SetActive(true);
        Time.timeScale = 1f;
    }

    IEnumerator loadGame()
    {
        transition.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        async.allowSceneActivation = true;
        //SceneManager.LoadScene("SampleScene");
    }

    IEnumerator loadTutorial()
    {
        //bird.SetActive(false);
        //tree.SetActive(false);
        transition.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        asyncTutorial.allowSceneActivation = true;
        //SceneManager.LoadScene("Tutorial");
    }

    IEnumerator loadFirstTutorial()
    {
        //bird.SetActive(false);
        //tree.SetActive(false);
        transition.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        asyncfirst.allowSceneActivation = true;
        //SceneManager.LoadScene("Tutorial");
    }
}
