using Nova.TMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusText : MonoBehaviour
{

    [SerializeField] private TextMeshProTextBlock textMeshPro;
    [SerializeField] private GameObject arrow;

    [SerializeField] private List<string> textList = new List<string>();
    [SerializeField] private int tutoIndex = 0;

    [SerializeField] private Vector3 affichePosition = new Vector3(-2.5f,0.65f,0f);
    [SerializeField] private Vector3 afficheRotation = new Vector3(0f,0f,-80f);

    [SerializeField] private Vector3 stickersPosition = new Vector3(-2.5f, 0.65f, 0f);
    [SerializeField] private Vector3 stickersRotation = new Vector3(0f, 0f, -80f);

    [SerializeField] private Vector3 placeStickersPosition = new Vector3(-2.5f, 0.65f, 0f);
    [SerializeField] private Vector3 placeStickersRotation = new Vector3(0f, 0f, -80f);

    [SerializeField] private Vector3 nextPosition = new Vector3(-2.5f, 0.65f, 0f);
    [SerializeField] private Vector3 nextRotation = new Vector3(0f, 0f, -80f);

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
        textList.Add("Bien maintenant clique sur un stickers et place le sur l'affiche!");
        textList.Add("Tu l'as mal collé! Que vais-je faire de toi");
        textList.Add("Bon laisse tomber. Si tu penses que ton affiche est bonne, alors tu peux passer à la suivante en cliquant ici!");
        ChangeText(textList[tutoIndex]);

    }

    public void NextText()
    {
        tutoIndex++;

        if(tutoIndex == 6)
        {
            arrow.SetActive(true);
            arrow.transform.position = affichePosition;
            arrow.transform.rotation = Quaternion.Euler(afficheRotation);
        }

        if(tutoIndex == 10)
        {
            arrow.transform.position = stickersPosition;
            arrow.transform.rotation = Quaternion.Euler(stickersRotation);
        }

        if (tutoIndex == 11)
        {
            arrow.transform.position = placeStickersPosition;
            arrow.transform.rotation = Quaternion.Euler(placeStickersRotation);
        }

        if (tutoIndex == 13)
        {
            arrow.transform.position = nextPosition;
            arrow.transform.rotation = Quaternion.Euler(nextRotation);
        }

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
