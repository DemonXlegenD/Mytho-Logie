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

        stringsText.Add("Hi hii!! It's your first time here, isn't it!");
        stringsText.Add("I'll tell you how everything works");
        stringsText.Add("You see, this poster is incomplete.");
        stringsText.Add("Try and make Artemis happy with it");
        stringsText.Add("The symbol on the top left will indicate what direction you have taken.");
        stringsText.Add("Be careful, sometimes making one god happy will anger the other.");
        stringsText.Add("Take one of the stickers and put it on the poster.");
        stringsText.Add("You can rotate it with right click, scale it with the scroll wheel and flip it with middle click.");
        stringsText.Add("Pay close attention to where you're sticking them.");
        stringsText.Add("Well done! But it won't always be so easy.");
        stringsText.Add("Now you can deliver the poster.");
        stringsText.Add("Now you can deliver the poster.");
        stringsText.Add("Good Job! Now, back to work!");
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
