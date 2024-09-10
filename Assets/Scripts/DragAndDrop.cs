using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private SpriteRenderer spriteRenderer;
    private static int sortingOrder = 1;
    //private bool isDragging = false;
    public GameObject[] stickers; // Liste des stickers associés à cette affiche
    [SerializeField] private Vector2 stockPosition = new Vector2(20, 0);
    bool isNext = false;
    bool detect_sticker = true;
    string pathDefaultSticker = "Assets/Prefabs/Affiches/Zeus.prefab";
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
        if (!CompareTag("Affiche")) 
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseUp()
    {
        //isDragging = false;
        if (CompareTag("Affiche"))
        {
            //List<UnseenPoints.ClosestObjectInfo> ObjectsForTheScore = ClosePage();
            //foreach (UnseenPoints.ClosestObjectInfo obj in ObjectsForTheScore) {
            //    int test = ReadCSV(obj, spriteRenderer.name);
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

    public List<UnseenPoints.ClosestObjectInfo> ClosePage() 
    {
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

    private int ReadCSV(UnseenPoints.ClosestObjectInfo obj, string themeName)
    {
        string filePath = "Assets/DataBase/ScoreDataBase.csv";
        int results = 0;

        using (StreamReader sr = new StreamReader(filePath))
        {
            string headerLine = sr.ReadLine(); // Lire l'entête

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(',');

                string theme = values[0];
                string sticker = values[1];
                string emplacement = values[2];
                int point = int.Parse(values[3]);

                if (obj.StickerName.Contains(sticker) && obj.name == emplacement && themeName.Contains(theme))
                {
                    results = point;
                }
            }
        }
        
        Debug.Log($"Emplacement du sticker {obj.StickerName} : {obj.name} à {obj.distance}. Pour le thème : {themeName} cela donne {results}");
        return results;
    }

    void Update()
    {
        if (isNext)
        {
            if(detect_sticker)
            {
                AttachStickers();
                List<UnseenPoints.ClosestObjectInfo> ObjectsForTheScore = ClosePage();
                foreach (UnseenPoints.ClosestObjectInfo obj in ObjectsForTheScore) {
                    int nb_score = ReadCSV(obj, spriteRenderer.name);
                    GameManager.Instance.score += nb_score;
                }
                detect_sticker = false;
            }
            // Mouvement vers la position cible
            transform.position = Vector2.Lerp(transform.position, stockPosition, 5.0f * Time.deltaTime);

            // Vérification si l'objet est proche de la position finale
            if (Vector2.Distance(transform.position, stockPosition) < 0.01f) // Seuil de tolérance (peut être ajusté)
            {
                transform.position = stockPosition; // S'assurer que l'objet est exactement à la position finale
                isNext = false; // Arrêter le mouvement
            }
        }
    }

    public void Next()
    {
        isNext = true;
    }
}


