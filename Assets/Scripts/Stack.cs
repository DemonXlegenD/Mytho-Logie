using Nova;
using Nova.TMP;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stacks : MonoBehaviour
{
    // SerializeField
    //[SerializeField] private int indexActualAffiche = 0;
    [SerializeField] private GameObject[] affichesPrefabs; // Liste des préfabriqués d'affiches
    [SerializeField] private Interactable buttonNextAffiche;
    [SerializeField] private GameObject actualAffiche;
    [SerializeField] private TextMeshProTextBlock remainingAffiche;
    [SerializeField] private float timeBeforeEnding = 5f;
    [SerializeField] private GameObject spawnPoint; // Point d'apparition

    // Private var
    private GameManager gameManager;
    private BoxCollider2D spawnZone; // BoxCollider définissant la zone de spawn
    private List<GameObject> affichesUndone = new List<GameObject>(10);
    private List<GameObject> affichesDone = new List<GameObject>(10);

    void Start()
    {
        gameManager = GameManager.Instance;
        spawnZone = spawnPoint.GetComponent<BoxCollider2D>();

        for (int i = 0; i < 3; i++)
        {
            // Sélectionner une affiche aléatoire parmi les préfabriqués
            GameObject affichePrefab = affichesPrefabs[Random.Range(0, affichesPrefabs.Length)];
            SpawnStickersForAffiche(affichePrefab);
            GameObject instance = Instantiate(affichePrefab, transform.position, Quaternion.identity);

            affichesUndone.Add(instance);
        }

        actualAffiche = affichesUndone[0];
        actualAffiche.GetComponent<SpriteRenderer>().sortingOrder = 1;
        ChangeRemainingAfficheText();
    }

    public void ChangeRemainingAfficheText()
    {
        remainingAffiche.text = $"Affiches Restantes : {affichesUndone.Count}";
    }

    public void NextAffiche()
    {
        actualAffiche.GetComponent<Affiche>().Next();
        affichesDone.Add(actualAffiche);
        actualAffiche.GetComponent<SpriteRenderer>().sortingOrder = 1;
        affichesUndone.Remove(actualAffiche);
        ChangeRemainingAfficheText();
        
        if (affichesUndone.Count > 0)
        {
            actualAffiche = affichesUndone[0];
        }
        else
        {
            buttonNextAffiche.enabled = false;
            StartCoroutine(EndGame(timeBeforeEnding));
        }
    }

    private IEnumerator EndGame(float _timer)
    {
        yield return new WaitForSeconds(_timer);
        gameManager.ChangeScene("MainMenu");
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
