using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova.TMP;

public class AddComment : MonoBehaviour
{
    private TextMeshProTextBlock textMeshPro; 
    public Stacks stack;
    public int id = 0;
    private bool isWrited = false;

    // Sprites pour les émotions d'Arthémis et Apollon
    public Sprite artemisHappy;
    public Sprite artemisNeutral;
    public Sprite artemisSad;
    public Sprite apollonHappy;
    public Sprite apollonNeutral;
    public Sprite apollonSad;

    // Référence au UI block 2D de Nova
    public GameObject uiBlock2D;

    private Dictionary<string, List<string>> dictType1 = new Dictionary<string, List<string>>()
    {
        { "Arthémis", new List<string> { "You made this for me, I may send you a gift as a token of my appreciation.", "You have such good tastes, I think you would like to join my hunt session.", "Do you really think I look like that? I appreciate it, thank you." }},
        { "Apollon", new List<string> { "Wisely decided there.", "I'll talk to Father, maybe you'll get a raise.", "Sorry sunshine if I offended you before, you know, bad temper runs in the family." }}
    };

    private Dictionary<string, List<string>> dictType2 = new Dictionary<string, List<string>>()
    {
        { "Arthémis", new List<string> { "Some people are not made for winning.", "I think you missed the target.", "It perfectly reflects your miserable life." }},
        { "Apollon", new List<string> { "Meh..", "At least you tried sunshine.", "It's an interesting choice... although I'm not sure I understand it." }}
    };

    private Dictionary<string, List<string>> dictType3 = new Dictionary<string, List<string>>()
    {
        { "Arthémis", new List<string> { "Is this some sort of joke?", "Wherever you are, I'll track you down.", "I think it's time for target practice. Start running, commoner." }},
        { "Apollon", new List<string> { "Augh! Now really isn't the time for petty squabbles, but I'm afraid you leave me with no choice...", "Come on, sunshine, why this aberration? You should know that when my feelings get hurt, it never ends well.", "You know I have a reputation to uphold. Even if you are just a pathetic slave to my father." }}
    };

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProTextBlock>();
        if (textMeshPro == null)
        {
            textMeshPro = gameObject.AddComponent<TextMeshProTextBlock>();
        }
    }

    void Update()
    { 
        if (stack.endedGame && !isWrited) 
        {
            isWrited = true;
            Affiche afficheScript = stack.GetAffiches()[id].GetComponent<Affiche>();

            string selectedText = "";

            if (afficheScript.score > 66) 
            {
                selectedText = GetRandomPhrase("Apollon", "type1");
                UpdateUIBlockSprite("Apollon", "Happy");
            } 
            else if (afficheScript.score > 33) 
            {
                selectedText = GetRandomPhrase("Apollon", "type2");
                UpdateUIBlockSprite("Apollon", "Neutral");
            } 
            else 
            {
                selectedText = GetRandomPhrase("Apollon", "type3");
                UpdateUIBlockSprite("Apollon", "Sad");
            }

            textMeshPro.text = selectedText;
        }
    }

    public string GetRandomPhrase(string nom, string type)
    {
        Dictionary<string, List<string>> selectedDict = null;

        switch (type)
        {
            case "type1":
                selectedDict = dictType1;
                break;
            case "type2":
                selectedDict = dictType2;
                break;
            case "type3":
                selectedDict = dictType3;
                break;
            default:
                selectedDict = dictType1;
                break;
        }

        if (selectedDict.ContainsKey(nom))
        {
            List<string> phrases = selectedDict[nom];
            return phrases[Random.Range(0, phrases.Count)];
        }
        return "";
    }

    // Met à jour l'image du UI Block 2D en fonction du personnage et de l'émotion
    private void UpdateUIBlockSprite(string character, string emotion)
    {
        Sprite newSprite = null;

        if (character == "Arthémis")
        {
            switch (emotion)
            {
                case "type1":
                    newSprite = artemisHappy;
                    break;
                case "type2":
                    newSprite = artemisNeutral;
                    break;
                case "type3":
                    newSprite = artemisSad;
                    break;
            }
        }
        else if (character == "Apollon")
        {
            switch (emotion)
            {
                case "type1":
                    newSprite = apollonHappy;
                    break;
                case "type2":
                    newSprite = apollonNeutral;
                    break;
                case "type3":
                    newSprite = apollonSad;
                    break;
            }
        }

        if (newSprite != null)
        {
            //uiBlock2D.SetImage(newSprite);
        }
    }
}
