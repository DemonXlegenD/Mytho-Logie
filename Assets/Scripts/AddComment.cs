using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova.TMP;

public class AddComment : MonoBehaviour
{
    private TextMeshProTextBlock textMeshPro; 
    public Stacks stack;
    public int id = 0;

    private Dictionary<string, string> dictType1 = new Dictionary<string, string>()
    {
        { "Alice", "Phrase de type 1 pour Alice" },
        { "Bob", "Phrase de type 1 pour Bob" },
        { "Charlie", "Phrase de type 1 pour Charlie" }
    };

    private Dictionary<string, string> dictType2 = new Dictionary<string, string>()
    {
        { "Alice", "Phrase de type 2 pour Alice" },
        { "Bob", "Phrase de type 2 pour Bob" },
        { "Charlie", "Phrase de type 2 pour Charlie" }
    };

    private Dictionary<string, string> dictType3 = new Dictionary<string, string>()
    {
        { "Alice", "Phrase de type 3 pour Alice" },
        { "Bob", "Phrase de type 3 pour Bob" },
        { "Charlie", "Phrase de type 3 pour Charlie" }
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
        if (stack.endedGame) 
        {
            Affiche afficheScript = stack.GetAffiches()[id].GetComponent<Affiche>();
            
            if (afficheScript.score <33) 
            {
                textMeshPro.text = GetRandomPhrase("Bob", "type1");
            } else if (afficheScript.score <66) 
            {
                textMeshPro.text = GetRandomPhrase("Bob", "type2");
            } else {
                textMeshPro.text = GetRandomPhrase("Bob", "type3");
            }
        }
    }

    public string GetRandomPhrase(string nom, string type)
    {
        Dictionary<string, string> selectedDict = null;

        // Sélection du dictionnaire en fonction du type
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
        
        return selectedDict[nom];
    }
}
