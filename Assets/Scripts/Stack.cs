using Nova;
using Nova.TMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacks : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private int indexActualAffiche = 0;
    [SerializeField] private GameObject prefabAffiche;

    [SerializeField] private Interactable buttonNextAffiche;

    [SerializeField] private GameObject currentAffiche;
    private List<GameObject> affichesUndone;
    private List<GameObject> affichesDone;
    [SerializeField] private TextMeshProTextBlock remainingAffiche;

    [SerializeField] private float timeBeforeEnding = 5f;
    [SerializeField] private int maxAffiche = 1;

    [SerializeField] private ConfigurationUI ConfigurationUI;
    // Start is called before the first frame update
    void Start()
    {
        affichesUndone = new List<GameObject>(maxAffiche);
        affichesDone = new List<GameObject>(maxAffiche);
        gameManager = GameManager.Instance;

        for (int i = 0; i < maxAffiche; i++)
        {
            GameObject instance = Instantiate(prefabAffiche, transform.position, Quaternion.identity);

            affichesUndone.Add(instance);
        }
        currentAffiche = affichesUndone[0];
        currentAffiche.GetComponent<SpriteRenderer>().sortingOrder = 1;
        ChangeRemainingAfficheText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeRemainingAfficheText()
    {
        remainingAffiche.text = $"{affichesDone.Count} / {maxAffiche}";
    }


    public void NextAffiche()
    {
        Debug.Log(currentAffiche);
        currentAffiche.GetComponent<DragAndDrop>().Next();
        affichesDone.Add(currentAffiche);
        currentAffiche.GetComponent<SpriteRenderer>().sortingOrder = 1;
        affichesUndone.Remove(currentAffiche);
        ChangeRemainingAfficheText();
        if (affichesUndone.Count > 0) currentAffiche = affichesUndone[0];
        else
        {
            buttonNextAffiche.enabled = false;
            StartCoroutine(EndGame(timeBeforeEnding));

        }
    }


    private IEnumerator EndGame(float _timer)
    {
        yield return new WaitForSeconds(_timer);

        ConfigurationUI.StartCommentary();
    }

    public List<GameObject> GetAffiches() { return affichesDone; }
}