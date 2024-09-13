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
    public bool can_score = false;
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
            sticker.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void UnattachStickers()
    {
        foreach (GameObject sticker in stickersIn)
        {
            sticker.transform.SetParent(null);
            sticker.GetComponent<BoxCollider2D>().enabled = true;
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
        string theme_de_affiche = "";

        string selectedThemeName_ = transform.parent.GetComponent<Stacks>().selectedThemeName;
        bool neg = (selectedThemeName_ == "ArtemisGood" && themeName.Contains("Apollon")) // Demande d'apo : Flatter apollon            -> negatif
                || (selectedThemeName_ == "AppollonGood" && themeName.Contains("Arthemis")) // Demande d'apo : Cracher sur Arthemis      -> negatif
                || (selectedThemeName_ == "AppollonBad" && themeName.Contains("Apollon")) // Demande d'Arthemis : Cracher sur apollon   -> negatif
                || (selectedThemeName_ == "ArtemisBad" && themeName.Contains("Arthemis")); // Demande d'apo : Cracher sur arthemis      -> negatif

        bool pos = (selectedThemeName_ == "AppollonBad" && themeName.Contains("Arthemis")) // Demande d'arthemis : flatter arthemis     -> positif
                || (selectedThemeName_ == "ArtemisBad" && themeName.Contains("Apollon")) // Demande d'apollon : flatter apollon         -> positif
                || (selectedThemeName_ == "ArtemisGood" && themeName.Contains("Arthemis")) // Demande d'arthemis : flatter arthemis     -> positif
                || (selectedThemeName_ == "AppollonGood" && themeName.Contains("Apollon")); // Demande d'apollon : flatter apollon       -> positif

        if (pos)
        {
            theme_de_affiche = "Positif";
        } else if (neg)
        {
            theme_de_affiche = "Negatif";
        } else 
        {
            theme_de_affiche = "Positif";
        }

        themeName = themeName.Replace("(Clone)", "") + "_" + theme_de_affiche;
        using (StreamReader sr = new StreamReader(filePath))
        {
            string headerLine = sr.ReadLine(); // Lire l'entête

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(',');

                string nom_affiche_et_theme = values[0];
                string sticker_nom = values[1];
                string emplacement = values[2];
                int point = int.Parse(values[3]);

                if (obj.StickerName.Contains(sticker_nom) && obj.name == emplacement && themeName.Contains(nom_affiche_et_theme))
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
        //Animation Left
        if (isGoingLeft)
        {
            if (!isFirstAnimation) {
                foreach (Transform child in transform)
                {
                    if (child.CompareTag("Sticker"))
                    {
                        if (child.GetComponent<DragAndDrop>().isTextSticker) {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
            }

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
                    if (isMainAffiche) 
                    {
                        UnattachStickers();
                    }
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

            if (!isFirstAnimation) {
                foreach (Transform child in transform)
                    {
                        if (child.CompareTag("Sticker"))
                        {
                            if (child.GetComponent<DragAndDrop>().isTextSticker) {
                                child.gameObject.SetActive(true);
                            }
                        }
                    }
            }

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
                    if (isMainAffiche) 
                    {
                        UnattachStickers();
                    }

                }
                isGoingRight = false;
                elapsedTimeRight = 0f;
            }
        }
    }

    public void AddScore()
    {
        if (detect_sticker)
        {
            List<UnseenPoints.ClosestObjectInfo> ObjectsForTheScore = ClosePage();
            foreach (UnseenPoints.ClosestObjectInfo obj in ObjectsForTheScore)
            {
                int nb_score = ReadCSV(obj, name);
                score += nb_score;
            }
            Debug.Log("hihi");
            GameManager.Instance.score += score / 2;
            detect_sticker = false;
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
