using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private SpriteRenderer spriteRenderer;
    private static int sortingOrder = 1;
    //private bool isDragging = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (CompareTag("Affiche")) 
        {
            spriteRenderer.sortingOrder = 0;
        } else 
        {
            spriteRenderer.sortingOrder = sortingOrder;

        }
    }

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseWorldPos();

        if (!CompareTag("Affiche")) 
        {
            spriteRenderer.sortingOrder = sortingOrder++;
        }
        else
        {
            //isDragging = true;
            AttachStickers(); // Appelle la fonction qui attache les stickers existants sur l'affiche
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        //isDragging = false;
        if (CompareTag("Affiche"))
        {
            //List<UnseenPoints.ClosestObjectInfo> ObjectsForTheScore = ClosePage();
            //foreach (UnseenPoints.ClosestObjectInfo obj in ObjectsForTheScore) {
            //    Debug.Log($"Emplacement du sticker {obj.StickerName} : {obj.name} à {obj.distance}");
            //}
            DetachStickers(); // Détache les stickers une fois que l'affiche est relâchée
        }
    }

    // Attache les stickers à l'affiche quand on clique dessus
    void AttachStickers()
    {
        // On vérifie tous les colliders qui touchent l'affiche
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<Collider2D>().bounds.size, 0f);

        foreach (Collider2D hitCollider in hitColliders)
        {
            SpriteRenderer stickerRenderer = hitCollider.GetComponent<SpriteRenderer>();

            if (hitCollider.CompareTag("Sticker"))
            {
                if (spriteRenderer.sortingOrder <= stickerRenderer.sortingOrder) 
                {
                    // Si c'est un sticker, on l'attache à l'affiche
                    hitCollider.transform.SetParent(transform);
                }
            }
        }
    }

    // Détache tous les stickers lorsque l'affiche est relâchée
    void DetachStickers()
    {
        // Liste pour stocker les enfants à détacher
        List<Transform> stickersToDetach = new List<Transform>();

        UnseenPoints detectedZone = GetComponent<UnseenPoints>();
        // Ajouter les stickers à détacher à la liste
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Sticker"))
            {
                stickersToDetach.Add(child);
            }
        }

        // Détacher les stickers de l'affiche
        foreach (Transform sticker in stickersToDetach)
        {
            var closestObject = detectedZone.GetClosestGameObject(sticker.transform);
            sticker.SetParent(null);
        }
    }

    public List<UnseenPoints.ClosestObjectInfo> ClosePage() {
        List<Transform> stickersList = new List<Transform>();
        UnseenPoints detectedZone = GetComponent<UnseenPoints>();
        List<GameObject> objectsToRemove = new List<GameObject>();  // Liste temporaire pour stocker les objets à supprimer
        List<UnseenPoints.ClosestObjectInfo> ClosestObjectInfoForTheScore = new List<UnseenPoints.ClosestObjectInfo>();
        // Ajouter les stickers à détacher à la liste
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Sticker"))
            {
                stickersList.Add(child);
            }
        }

        // Parcourir la liste des stickers et trouver les objets les plus proches
        foreach (Transform sticker in stickersList)
        {
            var closestObject = detectedZone.GetClosestGameObject(sticker.transform);

            if (closestObject.HasValue)  // Vérifier si closestObject n'est pas null
            {
                // Créer une copie de closestObject pour la modifier
                UnseenPoints.ClosestObjectInfo updatedClosestObject = closestObject.Value;

                // Modifier la propriété StickerName
                updatedClosestObject.SetClosestObjectInfo(sticker.name);

                // Afficher les informations mises à jour
                ClosestObjectInfoForTheScore.Add(updatedClosestObject);
                // Trouver le GameObject correspondant au nom et l'ajouter à la liste temporaire
                foreach (GameObject obj in detectedZone.gameObjects)
                {
                    if (obj.name == updatedClosestObject.name)
                    {
                        objectsToRemove.Add(obj);  // Ajouter cet objet à la liste de suppression
                    }
                }
            }
            else
            {
                Debug.Log("Aucun objet proche trouvé pour ce sticker.");
            }
        }

        // Supprimer les objets après avoir parcouru tous les stickers
        foreach (GameObject objToRemove in objectsToRemove)
        {
            detectedZone.RemoveGameObject(objToRemove);
        }

        return ClosestObjectInfoForTheScore;
    }



    // Optionnel : Dessiner un cadre autour de l'affiche dans la scène pour voir la zone de détection
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider2D>().bounds.size);
    }
}
