using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RandomTitle : MonoBehaviour
{
    public Sprite[] sprites; // Tableau de sprites à assigner depuis l'inspecteur
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AssignRandomSprite();
    }

    void AssignRandomSprite()
    {
        // Vérifie qu'il y a des sprites dans le tableau
        if (sprites.Length > 0)
        {
            // Sélectionne un sprite aléatoire
            Sprite randomSprite = sprites[Random.Range(0, sprites.Length)];

            // Affecte le sprite au SpriteRenderer de l'objet
            spriteRenderer.sprite = randomSprite;
        }
        else
        {
            Debug.LogError("Aucun sprite assigné dans le tableau des sprites");
        }
    }
}
