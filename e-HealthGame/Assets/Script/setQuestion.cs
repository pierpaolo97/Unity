using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class setQuestion : MonoBehaviour
{
    public Question[] questionsC;
    public Question[] questionsG;
    public Question[] questionsSC;
    public static List<Question> unansweredQuestions;
    public Question currentQuestion;
    public GameObject domanda;

    private Renderer rend;
    private TextMeshPro textParola;
    public GameObject risposte;
    public GameObject carta;
    public GameObject cartasonoro;

    private TextMeshPro r1;
    private TextMeshPro r2;
    private Text textCarta;
    private RawImage photoCarta;

    public GameObject audioObject;
    public AudioSource audioSource;
    public Texture2D audioTexture;
    public GameObject transition;

    public List<GameObject> mondi;
    public GameObject currentWorld;
    public GameObject pioggia;
    public List<int> validChoices;

    public GameObject risposta1;
    public GameObject risposta2;
    Vector3 posizioneRisp1;
    Vector3 posizioneRisp2;

    void Start()
    {

        if (PlayerPrefs.GetInt("Level", 0)== 0)
        {
            transition.SetActive(true);
            caricaDomande();
        }
       
        //Debug.Log(PlayerPrefs.GetString("LetteraLivello"));
        rend = domanda.transform.Find("Immagine").GetComponent<MeshRenderer>();
        textParola = domanda.transform.Find("Parola").GetComponent<TextMeshPro>();
        textCarta = carta.transform.Find("TextImg").GetComponent<Text>();
        photoCarta = carta.transform.Find("PhotoImg").GetComponent<RawImage>();

        int estrazione = Random.Range(0, 2);

        if (estrazione == 0)
        {
            posizioneRisp1 = new Vector3(-41f, 0.48f, -20f);
        }
        else
        {
            posizioneRisp1 = new Vector3(40f, 0.48f, -24f);
        }

        int estrazione2 = Random.Range(0, 2);

        if (estrazione2 == 0)
        {
            posizioneRisp2 = new Vector3(-41f, 0.48f, 18.25f);
        }
        else
        {
            posizioneRisp2 = new Vector3(39.1f, 0.48f, 19.9f);
        }


        GameObject risp1 = Instantiate(risposta1, posizioneRisp1, transform.rotation) as GameObject;
        if (risp1.transform.position.x > 20f)
        {
            risp1.transform.rotation = Quaternion.Euler(0, 110, 0);
        }
        risp1.transform.parent = risposte.transform;
        risp1.name = risp1.name.Replace("(Clone)", "");

        GameObject risp2 = Instantiate(risposta2, posizioneRisp2, transform.rotation) as GameObject;
        if (risp2.transform.position.x < -20f)
        {
            risp2.transform.rotation = Quaternion.Euler(0, -110, 0);
        }
        risp2.transform.parent = risposte.transform;
        risp2.name = risp2.name.Replace("(Clone)", "");


        r1 = risposte.transform.Find("R1").transform.Find("textR1").GetComponent<TextMeshPro>();
        r2 = risposte.transform.Find("R2").transform.Find("textR2").GetComponent<TextMeshPro>();

       
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            caricaDomande();
        }
        
        SetCurrentQuestion();
    }

    void Awake()
    {
        setCurrentWorld();
    }

    public void caricaDomande()
    {
        switch (PlayerPrefs.GetString("LetteraLivello", "C"))
        {
            case "C":
                unansweredQuestions = questionsC.ToList<Question>();
                break;
            case "G":
                unansweredQuestions = questionsG.ToList<Question>();
                break;
            case "SC":
                unansweredQuestions = questionsSC.ToList<Question>();
                break;
        }
        //Debug.Log("dall'altro codice");
    }

    void setCurrentWorld()
    {
        //Debug.Log("Mondo precedente" + PlayerPrefs.GetInt("CurrentWorld", -1).ToString());
        for (int i = 0; i < mondi.Count; i++)
        {
            if (i != PlayerPrefs.GetInt("CurrentWorld", -1))
            {
                validChoices.Add(i);
                //Debug.Log("ciclo" + i.ToString());
            }
        }

        int z = validChoices[Random.Range(0, validChoices.Count)];
        //Debug.Log("Mondo scelto" + z.ToString());

        PlayerPrefs.SetInt("CurrentWorld", z);
        currentWorld = mondi[z];
        Instantiate(currentWorld, new Vector3(0f, 0f, 0f), transform.rotation);

        if (currentWorld.name == "Mondo" || currentWorld.name == "Giostre")
        {
            int A = Random.Range(0, 10);
            Debug.Log(A);
            if (A > 8)
            {
                Instantiate(pioggia, new Vector3(3.6f, 113f, 4f), transform.rotation);
            }
        }

        //currentWorld.SetActive(true);
    }

    void SetCurrentQuestion()
    {
        int x = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[x];
        unansweredQuestions.RemoveAt(x);

        int y = Random.Range(0, 2);
        switch (y)
        {
            case 0:
                r1.text = currentQuestion.risposte[0];
                r2.text = currentQuestion.risposte[1];
                break;
            case 1:
                r1.text = currentQuestion.risposte[1];
                r2.text = currentQuestion.risposte[0];
                break;
        }

        if (string.Equals(r1.text, currentQuestion.rispostaGiusta))
        {
            risposte.transform.Find("R1").transform.Find("Risposta").tag = "True";
        }
        if (string.Equals(r2.text, currentQuestion.rispostaGiusta))
        {
            risposte.transform.Find("R2").transform.Find("Risposta").tag = "True";
        }

        string diff = PlayerPrefs.GetString("difficolta", "Easy");
        int p = Random.Range(0, 100);

        switch (diff)
        {
            case "Easy":
                //Debug.LogWarning("Easy");
                if (p >= 10)
                {
                    parola();
                }
                else
                {
                    audioParola();
                }
                break;

            case "Medium":
                //Debug.LogWarning("Medium");
                if (p >= 30)
                {
                    parola();
                }
                else
                {
                    audioParola();
                }
                break;

            case "Hard":
                //Debug.LogWarning("Hard");
                if (p >= 50)
                {
                    parola();
                }
                else
                {
                    audioParola();
                }
                break;
        }
    }

    IEnumerator playAudio()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(currentQuestion.audioWord);
    }

    private void parola()
    {
        carta.SetActive(true);
        rend.material.mainTexture = currentQuestion.texture;
        textParola.text = currentQuestion.word;
        textCarta.text = currentQuestion.word;
        photoCarta.texture = currentQuestion.texture;
    }

    private void audioParola()
    {
        cartasonoro.SetActive(true);
        rend.material.mainTexture = audioTexture;
        textParola.text = "";
        audioObject.SetActive(true);
        StartCoroutine(playAudio());
    }

}
