using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : MonoBehaviour
{
    private bool isAttached = false;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "AloneSticker";
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Affiche") && GetComponent<DragAndDrop>().isDragging)
        {
            Affiche affiche = other.GetComponent<Affiche>();
            if (affiche.GetIsMainAffiche() && !isAttached) {
                Debug.Log("Attach");
                AttachToAffiche();
                affiche.AddStickers(gameObject);
            }
        }
    }

    private void AttachToAffiche()
    {
        isAttached = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
         if (other.CompareTag("Affiche") && GetComponent<DragAndDrop>().isDragging)
        {
            Affiche affiche = other.GetComponent<Affiche>();
            if (affiche.GetIsMainAffiche())
            {
                AttachToAffiche();
            }
        }
    }
 

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Affiche") && GetComponent<DragAndDrop>().isDragging)
        {
            Affiche affiche = other.GetComponent<Affiche>();
            if (affiche.GetIsMainAffiche() && isAttached)
            {
                affiche.RemoveStickers(gameObject);
                Debug.Log("Detach");
                isAttached = false; // Permet de retirer le sticker de l'affiche
            }
     
        }
    }

}
