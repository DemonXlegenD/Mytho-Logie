using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Affiche : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private SpriteRenderer spriteRenderer;
    public List<GameObject> stickers = new List<GameObject>(); // Liste des stickers associés à cette affiche

    public List<GameObject> stickersIn = new List<GameObject>();
    [SerializeField] private Vector2 stockPosition = new Vector2(20, 0);
    private int sortOrder = 1;

    private bool isMainAffiche = false;
    bool isNext = false;
    bool detect_sticker = true;
    public int score;

    private float animationDuration = 0.5f;

    private bool isFirstAnimation = true;
    private Vector3 basePosition = Vector3.zero;
    private bool isGoingLeft = false;
    private float elapsedTimeLeft = 0f;
    private bool isGoingRight = false;
    private float elapsedTimeRight = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        Vector3 position = transform.position;
        position.z = 0f;
        transform.position = position;
        spriteRenderer.sortingOrder = sortOrder;
        basePosition = position;
    }

    public void SetMainOrder()
    {
        spriteRenderer.sortingLayerName = "MainAffiche";
        foreach (GameObject sticker in stickersIn)
        {
            sticker.GetComponent<SpriteRenderer>().sortingLayerName = "MainAffiche";
        }
    }

    public void SetOtherOrder()
    {
        spriteRenderer.sortingLayerName = "OtherAffiche";
        foreach (GameObject sticker in stickersIn)
        {
            sticker.GetComponent<SpriteRenderer>().sortingLayerName = "OtherAffiche";
        }
    }

    public void AddStickers(GameObject sticker)
    {
        stickersIn.Add(sticker);
        sortOrder++;
        SpriteRenderer spriteSticker = sticker.GetComponent<SpriteRenderer>();
        spriteSticker.sortingOrder = sortOrder;
        spriteSticker.sortingLayerName = "MainAffiche";
    }

    public void RemoveStickers(GameObject sticker)
    {
        stickersIn.Remove(sticker);
        SpriteRenderer spriteSticker = sticker.GetComponent<SpriteRenderer>();
        spriteSticker.sortingOrder = 0;
        spriteSticker.sortingLayerName = "AloneSticker";
        ResetOrder();
    }

    public void ChangeOrder(int newOrder)
    {

        foreach (GameObject sticker in stickersIn)
        {
            sticker.GetComponent<SpriteRenderer>().sortingOrder = sortOrder;
        }
    }
    public void ResetOrder()
    {
        sortOrder = 1;
        foreach (GameObject sticker in stickersIn)
        {
            sortOrder++;
            sticker.GetComponent<SpriteRenderer>().sortingOrder = sortOrder;
        }
    }

    public void AttachStickers()
    {
        foreach (GameObject sticker in stickersIn)
        {
            sticker.transform.SetParent(transform);
        }
    }

    public void UnattachStickers()
    {
        foreach (GameObject sticker in stickersIn)
        {
            sticker.transform.SetParent(null);
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
        score += results;
        return results;
    }

    void Update()
    {
        if (isNext)
        {
            if (detect_sticker)
            {
                //AttachStickers();
                List<UnseenPoints.ClosestObjectInfo> ObjectsForTheScore = ClosePage();
                foreach (UnseenPoints.ClosestObjectInfo obj in ObjectsForTheScore)
                {
                    int nb_score = ReadCSV(obj, name);
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

        //Animation Left
        if (isGoingLeft)
        {
            // Calculer le temps écoulé
            elapsedTimeLeft += Time.deltaTime;

            // Calculer la nouvelle position
            transform.position = Vector3.Lerp(basePosition, basePosition + new Vector3(-2f, 0f), elapsedTimeLeft / animationDuration);

            // Vérifier si la durée est dépassée
            if (elapsedTimeLeft >= animationDuration)
            {
                // S'assurer que l'objet atteint exactement la position finale
                basePosition += new Vector3(-2f, 0f);
                transform.position = basePosition;

                if (isFirstAnimation)
                {
                    isFirstAnimation = false;
                    StartCoroutine(WaitAnimationNextRight());
   
                }
                else
                {
                    isFirstAnimation = true;
                    if (isMainAffiche) UnattachStickers();
                }
                isGoingLeft = false;
                elapsedTimeLeft = 0f;
            }
        }
        //Animation Right
        if (isGoingRight)
        {
            // Calculer le temps écoulé
            elapsedTimeRight += Time.deltaTime;

            // Calculer la nouvelle position
            transform.position = Vector3.Lerp(basePosition, basePosition + new Vector3(2f, 0f), elapsedTimeRight / animationDuration);

            // Vérifier si la durée est dépassée
            if (elapsedTimeRight >= animationDuration)
            {
                // S'assurer que l'objet atteint exactement la position finale
                basePosition += new Vector3(2f, 0f);
                transform.position = basePosition;

                if (isFirstAnimation)
                {
                    isFirstAnimation = false;
                    StartCoroutine(WaitAnimationNextLeft());
                }
                else
                {
                    isFirstAnimation = true;
                    if (isMainAffiche) UnattachStickers();

                }
                isGoingRight = false;
                elapsedTimeRight = 0f;
            }
        }
    }

    public void Next()
    {
        isNext = true;
    }

    #region Animaton & Active

    public void ShowAffiche()
    {
        gameObject.SetActive(true);
    }
    public void HideAffiche()
    {
        gameObject.SetActive(false);
    }


    public void GoToLeft()
    {
        AttachStickers();
        Debug.Log("Left call " + name);
        isGoingLeft = true;
    }

    public void GoToRight()
    {
        AttachStickers();
        Debug.Log("Right call " + name);
        isGoingRight = true;
    }

    private IEnumerator WaitAnimationNextLeft()
    {
        yield return new WaitForSeconds(0.2f);
        SetOtherOrder();
        GoToLeft();
    }

    private IEnumerator WaitAnimationNextRight()
    {
        yield return new WaitForSeconds(0.2f);
        SetMainOrder();
        GoToRight();
    }

    #endregion


    private IEnumerator EndGame()
    {
        yield return null;
        gameObject.SetActive(false);
    }

    public void SetIsMainAffiche(bool _value)
    {
        isMainAffiche = _value;
    }

    public bool GetIsMainAffiche()
    {
        return isMainAffiche;
    }
}
