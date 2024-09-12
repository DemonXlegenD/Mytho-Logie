using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : MonoBehaviour
{
    private bool isAttached = false;
  private Vector2 screenBounds;
    private float spriteWidth;
    private float spriteHeight;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "AloneSticker";

        // Obtenir les limites de la caméra
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Obtenir la taille du sprite
        spriteWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        spriteHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

  

    void LateUpdate()
    {
        // Obtenir la position actuelle du sprite
        Vector3 viewPos = transform.position;

        // Limiter les mouvements du sprite dans les bornes de la caméra
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + spriteWidth, screenBounds.x - spriteWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + spriteHeight, screenBounds.y - spriteHeight);

        // Appliquer la nouvelle position
        transform.position = viewPos;
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
