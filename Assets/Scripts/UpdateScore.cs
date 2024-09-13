using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova.TMP;

public class UpdateScore : MonoBehaviour
{
    private TextMeshProTextBlock textMeshPro;
   [SerializeField] private Jauges jauge;
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
            if (GameManager.Instance.score < 0) GameManager.Instance.score = 0;
            if (GameManager.Instance.score > 100) GameManager.Instance.score = 100;
            textMeshPro.text = (GameManager.Instance.score).ToString();
            jauge.Change(GameManager.Instance.score);
        } else if (stack.endedGame) {
            Affiche afficheScript = stack.GetAffiches()[id].GetComponent<Affiche>();
            textMeshPro.text = afficheScript.score.ToString();
        }
    }
}
