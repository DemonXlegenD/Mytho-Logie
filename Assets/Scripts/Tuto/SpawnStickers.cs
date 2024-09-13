using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStickers : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void SpawnStickersForAffiche(GameObject affiche)
    {
        Affiche afficheData = affiche.GetComponent<Affiche>();
        // Vérifier que la liste de stickers n'est pas vide
        if (afficheData.stickers.Count == 0)
        {
            Debug.LogWarning("Aucun sticker associé à cette affiche.");
            return;
        }

        int numberOfStickers = 4; // Nombre de stickers à instancier aléatoirement

        for (int i = 0; i < numberOfStickers; i++)
        {
            // Sélectionner un sticker aléatoire parmi ceux associés à l'affiche
            GameObject stickerPrefab = afficheData.stickers[i];

            Vector2 colliderCenter = boxCollider.offset;
            Vector2 colliderSize = boxCollider.size;

            Vector2 randomPosition = new Vector2(
                Random.Range(-colliderSize.x / 2f, colliderSize.x / 2f),
                Random.Range(-colliderSize.y / 3f, colliderSize.y / 2f)
            );

            Vector3 spawnPosition = boxCollider.transform.position + (Vector3)(colliderCenter + randomPosition);

            // Instancier le sticker
            GameObject stickerInstance = Instantiate(stickerPrefab, spawnPosition, Quaternion.identity);
            stickerInstance.transform.SetParent(transform);

        }
    }

    
}