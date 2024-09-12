using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova.TMP;

public class UpdateScore : MonoBehaviour
{
    private TextMeshProTextBlock textMeshPro;
    public Stacks stack;
    public bool total = true; 
    public int id = 0;
    private bool printed = false;

    // Start is called before the first frame update
    void Start()
    {
        printed = false;
        textMeshPro = GetComponent<TextMeshProTextBlock>();
        if (textMeshPro == null)
        {
            textMeshPro = gameObject.AddComponent<TextMeshProTextBlock>();
        }
    }

    void Update()
    {
        if (total && stack.endedGame) 
        {
            textMeshPro.text = (GameManager.Instance.score).ToString();
        } else if (stack.endedGame) {
            Affiche afficheScript = stack.GetAffiches()[id].GetComponent<Affiche>();
            textMeshPro.text = afficheScript.score.ToString();
        }
    }
}
