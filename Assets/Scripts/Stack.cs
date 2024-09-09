using Nova.TMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacks : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private int indexActualAffiche = 0;
    [SerializeField] private Affiche actualAffiche;
    private List<Affiche> affichesUndone = new List<Affiche>(10);
    private List<Affiche> affichesDone = new List<Affiche>(10);
    [SerializeField] private TextMeshProTextBlock remainingAffiche;

    // Start is called before the first frame update
    void Start()
    {
        gameManager= GetComponent<GameManager>();
       
        //Pool d'affiche


        for (int i = 0; i < 10; i++)
        {
            affichesUndone.Add(new Affiche());
        }
        actualAffiche = affichesUndone[0];
        ChangeRemainingAfficheText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRemainingAfficheText()
    {
        remainingAffiche.text = $"Affiches Restantes : {affichesUndone.Count}";
    }


    public void NextAffiche()
    {
        affichesDone.Add(actualAffiche);
        affichesUndone.Remove(actualAffiche);
               ChangeRemainingAfficheText();
        if (affichesUndone.Count > 0) actualAffiche = affichesUndone[0];
        else gameManager.PauseGame();
    }


}
