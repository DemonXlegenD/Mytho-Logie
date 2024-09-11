using Nova.TMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class ZeusText : MonoBehaviour
{

    [SerializeField] private TextMeshProTextBlock textMeshPro;
    [SerializeField] private Arrow arrow;

    [SerializeField] private List<string> textList = new List<string>();
    [SerializeField] private int tutoIndex = 0;

    private void Start()
    {
        textList.Add("Bonjour esclave, c'est l'heure de travailler!");
        textList.Add("J'espère que tu es prêt car c'est une tâche très difficile.");
        textList.Add("Tellement difficile qu'un misérable humain comme toi ne pourra y parvenir.");
        textList.Add("Un peu comme ma première femme.");
        textList.Add("ENFIN BREF.");
        textList.Add("Ton travail est de créer des affiches de propagande pour nos clients.");
        textList.Add("Ici tu as les affiches.");
        textList.Add("Tu devras y coller des stickers.");
        textList.Add("Sais-tu où sont les stickers?.");
        textList.Add("Incompétant !!!");
        textList.Add("Ils sont là!");
        textList.Add("Bien maintenant tu peux prendre un stickers.");
        textList.Add("Maintenant tu le places sur l'affiche à l'endroit exact qui est pointé!");
        textList.Add("Tu l'as mal collé! Que vais-je faire de toi");
        textList.Add("Bon laisse tomber. Si tu penses que ton affiche est bonne, alors tu peux passer à la suivante en cliquant ici!");
        textList.Add("Bravo tu as tout compris. Je savais que je pouvais compter sur toi");
        textList.Add("Travaille bien, nos clients sont très exigents, et surtout pas de bêtises, sinon on va encore croire que c'est moi...");
        ChangeText(textList[tutoIndex]);
    }

    public void NextText()
    {
        tutoIndex++;

        if(tutoIndex == 6)
        {
            arrow.GoToAffiche();
        }

        if(tutoIndex == 10)
        {
            arrow.GoToStickers();
        }

        if (tutoIndex == 12)
        {
            arrow.GoToPlaceStickers();
        }

        if (tutoIndex == 14)
        {
            arrow.GoToNextButton();
        }

        if (tutoIndex == textList.Count)
        {
            GameManager.Instance.ChangeScene("MainMenu");
        }
        ChangeText(textList[tutoIndex]);
    }

    public void HoldStickers()
    {
        ChangeText("Maintiens ton clique!!!");
    }

    public void UnholdStickers()
    {
        ChangeText("Reprends le stickers");
    }

    public void ChangeText(string text)
    {
        textMeshPro.text = text;
    }
}
