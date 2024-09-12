using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UpdateScore : MonoBehaviour
{
    private TextMeshPro textMeshPro; 
    public Stacks stack;
    public bool total = true; 
    public int id = 0;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        if (textMeshPro == null)
        {
            textMeshPro = gameObject.AddComponent<TextMeshPro>();
        }

        if (total) 
        {
            textMeshPro.text = (stack.gameManager.score).ToString();
        } else {
            Affiche afficheScript = stack.GetAffiches()[id].GetComponent<Affiche>();
            textMeshPro.text = afficheScript.score.ToString();
            
        }
    }
}
