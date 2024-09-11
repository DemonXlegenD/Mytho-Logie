using Nova;
using Nova.TMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacks : MonoBehaviour
{
    // SerializeField
    //[SerializeField] private int indexActualAffiche = 0;
    [SerializeField] private GameObject[] affichesPrefabs; // Liste des préfabriqués d'affiches
    [SerializeField] private Interactable buttonNextAffiche;

    [SerializeField] private GameObject currentAffiche;
    private List<GameObject> affichesUndone;
    private List<GameObject> affichesDone;
    [SerializeField] private TextMeshProTextBlock remainingAffiche;

    [SerializeField] private float timeBeforeEnding = 5f;
    [SerializeField] private int maxAffiche = 1;

    [SerializeField] private ConfigurationUI ConfigurationUI;
    // Start is called before the first frame update
    [SerializeField] private GameObject spawnPoint; // Point d'apparition

    // Private var
    private GameManager gameManager;
    private BoxCollider2D spawnZone; // BoxCollider définissant la zone de spawn

    void Start()
    {
        affichesUndone = new List<GameObject>(maxAffiche);
        affichesDone = new List<GameObject>(maxAffiche);
        gameManager = GameManager.Instance;

        spawnZone = spawnPoint.GetComponent<BoxCollider2D>();

        for (int i = 0; i < maxAffiche; i++)
        {
            // Sélectionner une affiche aléatoire parmi les préfabriqués
            GameObject affichePrefab = affichesPrefabs[Random.Range(0, affichesPrefabs.Length)];
            affichePrefab.GetComponent<SpriteRenderer>().sortingOrder = maxAffiche - i -    1;
            SpawnStickersForAffiche(affichePrefab);
            
            GameObject instance = Instantiate(affichePrefab, transform.position, Quaternion.identity);

            affichesUndone.Add(instance);
        }
        currentAffiche = affichesUndone[0];
        //currentAffiche.GetComponent<SpriteRenderer>().sortingOrder = 1;
        ChangeRemainingAfficheText();
    }

    public void ChangeRemainingAfficheText()
    {
        remainingAffiche.text = $"{affichesDone.Count} / {maxAffiche}";
    }

    public void NextAffiche()
    {
        Debug.Log(currentAffiche);
        currentAffiche.GetComponent<Affiche>().Next();

        affichesDone.Add(currentAffiche);
        affichesUndone.Remove(currentAffiche);
        
        ChangeRemainingAfficheText();
        if (affichesUndone.Count > 0) 
        {
            currentAffiche = affichesUndone[0];
            //currentAffiche.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else
        {
            buttonNextAffiche.enabled = false;
            StartCoroutine(EndGame(timeBeforeEnding));
        }
    }

    public List<GameObject> GetAffiches() { return affichesDone; }

    private IEnumerator EndGame(float _timer)
    {
        yield return new WaitForSeconds(_timer);
        ConfigurationUI.StartCommentary();
    }

    private void SpawnStickersForAffiche(GameObject affiche)
    {
        Affiche afficheData = affiche.GetComponent<Affiche>();
        // Vérifier que la liste de stickers n'est pas vide
        if (afficheData.stickers.Length == 0)
        {
            Debug.LogWarning("Aucun sticker associé à cette affiche.");
            return;
        }

        int numberOfStickers = Random.Range(3, 6); // Nombre de stickers à instancier aléatoirement

        for (int i = 0; i < numberOfStickers; i++)
        {
            // Sélectionner un sticker aléatoire parmi ceux associés à l'affiche
            GameObject stickerPrefab = afficheData.stickers[Random.Range(0, afficheData.stickers.Length)];

            Vector2 colliderCenter = spawnZone.offset;
            Vector2 colliderSize = spawnZone.size;

            Vector2 randomPosition = new Vector2(
                Random.Range(-colliderSize.x / 2f, colliderSize.x / 2f),
                Random.Range(-colliderSize.y / 2f, colliderSize.y / 2f)
            );

            Vector3 spawnPosition = spawnZone.transform.position + (Vector3)(colliderCenter + randomPosition);

            // Instancier le sticker
            GameObject stickerInstance = Instantiate(stickerPrefab, spawnPosition, Quaternion.identity);
        }
    }
}