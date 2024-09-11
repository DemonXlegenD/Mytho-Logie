using Nova;
using Nova.TMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacks : MonoBehaviour
{
    [SerializeField] private int indexActualAffiche = 0;
    [SerializeField] private GameObject[] affichesPrefabs; // Liste des préfabriqués d'affiches
    [SerializeField] private Interactable buttonNextAffiche;

    [SerializeField] private GameObject currentAffiche;
    [SerializeField] private GameObject nextAffiche;
    private List<GameObject> affichesUndone;
    private List<GameObject> affichesDone;
    [SerializeField] private TextMeshProTextBlock remainingAffiche;

    [SerializeField] private float timeBeforeEnding = 5f;
    [SerializeField] private int maxAffiche = 1;

    [SerializeField] private ConfigurationUI ConfigurationUI;
    // Start is called before the first frame update
    [SerializeField] private GameObject spawnPoint; // Point d'apparition
    [SerializeField] private GameObject StickerStack;

    // Private var
    private GameManager gameManager;
    private BoxCollider2D spawnZone; // BoxCollider définissant la zone de spawn
    private int currentLvlMinScore;

    void Start()
    {
        indexActualAffiche = 0;
        gameManager = GameManager.Instance;
        currentLvlMinScore = gameManager.scoreCapToChangeLvl[gameManager.currentLvlID];
        StartLevel();
    }

    void RestartLevel() 
    {
        gameManager.currentLvlID += 1;
        gameManager.score = 0;
        currentLvlMinScore = gameManager.scoreCapToChangeLvl[gameManager.currentLvlID];
        StartLevel();
    }
    void StartLevel()
    {
        foreach (Transform child in StickerStack.transform) {
            GameObject.Destroy(child.gameObject);
        }
        affichesUndone = new List<GameObject>(maxAffiche);
        affichesDone = new List<GameObject>(maxAffiche);

        spawnZone = spawnPoint.GetComponent<BoxCollider2D>();

        for (int i = 0; i < maxAffiche; i++)
        {
            // Sélectionner une affiche aléatoire parmi les préfabriqués
            GameObject affichePrefab = affichesPrefabs[Random.Range(0, affichesPrefabs.Length)];
            affichePrefab.GetComponent<SpriteRenderer>().sortingOrder = maxAffiche - i - 1;
            SpawnStickersForAffiche(affichePrefab);
            
            GameObject instance = Instantiate(affichePrefab, transform.position, Quaternion.identity);
            instance.transform.SetParent(transform, false);
            instance.transform.localScale = Vector3.one * 18;
            affichesUndone.Add(instance);
        }
        currentAffiche = affichesUndone[indexActualAffiche];

        nextAffiche = affichesUndone[indexActualAffiche + 1];
        Affiche current = currentAffiche.GetComponent<Affiche>();
        Affiche next = nextAffiche.GetComponent<Affiche>();
        current.SetIsMainAffiche(true);
        next.SetIsMainAffiche(false);
        current.SetMainOrder();
        ChangeRemainingAfficheText();
    }

    public void ChangeRemainingAfficheText()
    {
        remainingAffiche.text = $"{affichesDone.Count} / {maxAffiche}";
    }

    public void NextAffiche()
    {
        indexActualAffiche = (indexActualAffiche + 1) % maxAffiche;
        currentAffiche.GetComponent<Affiche>().GoToRight();
        nextAffiche.GetComponent<Affiche>().GoToLeft();

        currentAffiche = affichesUndone[indexActualAffiche];
        nextAffiche = affichesUndone[(indexActualAffiche + 1) % maxAffiche];
        currentAffiche.GetComponent<Affiche>().SetIsMainAffiche(true);
        nextAffiche.GetComponent<Affiche>().SetIsMainAffiche(false);
    }

    private IEnumerator SwitchAffiche()
    {
        currentAffiche.gameObject.SetActive(false);
        yield return null;
        currentAffiche = affichesUndone[indexActualAffiche];
        currentAffiche.gameObject.SetActive(true);
    }

    public void SubmitAffiche()
    {
        buttonNextAffiche.enabled = false;
        StartCoroutine(EndGame(timeBeforeEnding));
    }

    public List<GameObject> GetAffiches() { return affichesDone; }

    private IEnumerator EndGame(float _timer)
    {
        yield return new WaitForSeconds(_timer);
        StickerStack.SetActive(false);
        foreach (GameObject affiche in affichesDone)
        {
            Affiche afficheScript = affiche.GetComponent<Affiche>();
            if (afficheScript != null)
            {
                gameManager.score += afficheScript.score; // Ajouter le score de l'affiche au score global
            }
        }
        ConfigurationUI.StartCommentary();
    }

    private void SpawnStickersForAffiche(GameObject affiche)
    {
        Affiche afficheData = affiche.GetComponent<Affiche>();
        // Vérifier que la liste de stickers n'est pas vide
        if (afficheData.stickers.Count == 0)
        {
            Debug.LogWarning("Aucun sticker associé à cette affiche.");
            return;
        }

        int numberOfStickers = Random.Range(3, 6); // Nombre de stickers à instancier aléatoirement

        for (int i = 0; i < numberOfStickers; i++)
        {
            // Sélectionner un sticker aléatoire parmi ceux associés à l'affiche
            GameObject stickerPrefab = afficheData.stickers[Random.Range(0, afficheData.stickers.Count)];

            Vector2 colliderCenter = spawnZone.offset;
            Vector2 colliderSize = spawnZone.size;

            Vector2 randomPosition = new Vector2(
                Random.Range(-colliderSize.x / 2f, colliderSize.x / 2f),
                Random.Range(-colliderSize.y / 2f, colliderSize.y / 2f)
            );

            Vector3 spawnPosition = spawnZone.transform.position + (Vector3)(colliderCenter + randomPosition);

            // Instancier le sticker
            GameObject stickerInstance = Instantiate(stickerPrefab, spawnPosition, Quaternion.identity);
            stickerInstance.transform.SetParent(StickerStack.transform);
        }
    }


    private void SwitchAnimation()
    {

    }
}