using Nova.TMP;
using System.Collections.Generic;

using UnityEngine;

public class Tuto : MonoBehaviour
{

    [SerializeField] private List<string> stringsText = new List<string>();
    private int stringIndex = 0;

    [SerializeField] private GameObject submitButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private TextMeshProTextBlock textMeshPro;
    [SerializeField] private SpawnAffiche spawnAffiche;
    [SerializeField] private Chibi chibi;
    [SerializeField] private SpawnStickers spawnStickers;
    private delegate void Step();

    private Step step;

    private void Start()
    {
        submitButton.SetActive(false);
        BlockInteractions();
        InitialiseTexts();
        step = StepOne;
        NextStep();
    }
    private void InitialiseTexts()
    {
        stringsText.Clear();

        stringsText.Add("Ohhhh, hello, hello!! It is your first time, isn't it?");
        stringsText.Add("I'll tell you how it works here");
        stringsText.Add("You see this poster is unfinished.");
        stringsText.Add("For this one you will have to make Artemis happy");
        stringsText.Add("You must look at this simbol at each poster to know what is the direction you must take.");
        stringsText.Add("Be careful, sometimes you will have to make one god sad by making another god kind.");
        stringsText.Add("Grab a sticker and put it on the poster.");
        stringsText.Add("And you can rotate it with right mouse button, zoom in/out with the wheel or flip it with the wheel click.");
        stringsText.Add("Pay attention to their location.");
        stringsText.Add("Well done! But carefull it won't always be so easy.");
        stringsText.Add("Now you can validate the poster.");
        stringsText.Add("Now you can validate the poster.");
        stringsText.Add("Good Job! Now, get back to work!");
    }
    public void BlockInteractions()
    {

    }

    public void NextStep()
    {
        step();
    }

    private void StepOne()
    {
        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;

        step = StepTwo;
    }

    private void StepTwo()
    {
        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;

        step = StepThree;
    }

    private void StepThree()
    {
        spawnAffiche.ShowChild();
        spawnAffiche.ActiveAnime();
        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;

        step = StepFour;
    }


    private void StepFour()
    {

        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;
        step = StepFive;
    }

    private void StepFive()
    {
        spawnAffiche.UnactiveAnime();
        chibi.ShowChibi();
        chibi.ActiveAnime();
        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;
        step = StepSix;
    }

    private void StepSix()
    {
        chibi.UnactiveAnime();
        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;
        step = StepSeven;
    }

    private void StepSeven()
    {
        spawnStickers.SpawnStickersForAffiche(spawnAffiche.GetAffiche());
        nextButton.SetActive(false);
        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;
        step = StepEight;
    }

    private void StepEight()
    {

        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;
        step = StepNine;
    }

    private void StepNine()
    {

        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;
        Debug.Log("9 : " + textMeshPro.text);
        step = StepTen;
    }

    private void StepTen()
    {
      nextButton.SetActive(true);
        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;
        step = StepEleven;
    }
    private void StepEleven()
    {
       nextButton.SetActive(false);
        textMeshPro.text = stringsText[stringIndex];
        submitButton.SetActive(true);
        stringIndex++;
        step = StepTwelve;
    }
    private void StepTwelve()
    {
        nextButton.SetActive(false);
        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;
        step = StepThirteen;
    }

    private void StepThirteen()
    {
        nextButton.SetActive(true);
        submitButton.SetActive(false);
        textMeshPro.text = stringsText[stringIndex];
        stringIndex++;
        step = StepFourteen;
    }

    private void StepFourteen()
    {
        GameManager.Instance.StartGame();
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
