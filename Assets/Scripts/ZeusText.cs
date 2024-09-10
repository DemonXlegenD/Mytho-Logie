using Nova.TMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusText : MonoBehaviour
{
    [SerializeField] private TextMeshProTextBlock textMeshPro;

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
        ChangeText(textList[tutoIndex]);

    }

    public void NextText()
    {
        tutoIndex++;

        if (tutoIndex == textList.Count)
        {
            GameManager.Instance.ChangeScene("MainMenu");
        }
        ChangeText(textList[tutoIndex]);
    }

    public void ChangeText(string text)
    {
        textMeshPro.text = text;
    }
}
