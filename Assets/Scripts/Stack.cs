using Nova.TMP;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stacks : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private int indexActualAffiche = 0;
    [SerializeField] private GameObject prefabAffiche;

    [SerializeField] private GameObject actualAffiche;
    private List<GameObject> affichesUndone = new List<GameObject>(10);
    private List<GameObject> affichesDone = new List<GameObject>(10);
    [SerializeField] private TextMeshProTextBlock remainingAffiche;

    // Start is called before the first frame update
    void Start()
    {
        gameManager= GetComponent<GameManager>();
       
        //Pool d'affiche


        for (int i = 0; i < 10; i++)
        {
            GameObject instance = Instantiate(prefabAffiche, transform.position, Quaternion.identity);

            affichesUndone.Add(instance);
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
        Debug.Log("Next First");
        actualAffiche.GetComponent<Affiche>().Next();
        affichesDone.Add(actualAffiche);
        affichesUndone.Remove(actualAffiche);
               ChangeRemainingAfficheText();
        if (affichesUndone.Count > 0) actualAffiche = affichesUndone[0];
        else gameManager.PauseGame();
    }

 


}
