using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private SpriteRenderer spriteRenderer;
    private static int sortingOrder = 10;
    public bool isDragging = false;
    private string stickerText = ""; // Le texte du sticker
    private bool isEditingText = false;
    private bool isFocused = false; // Pour vérifier si le sticker est sélectionné pour éditer

    [SerializeField] private Texture2D cursorTextureHover;
    [SerializeField] private Texture2D cursorTextureClic;
    [SerializeField] public bool isTextSticker = false; // Variable pour déterminer si c'est un sticker texte

    // Variables de configuration pour le curseur
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public float scaleSpeed = 0.1f;
    public float rotationSpeed = 150f;
    private Camera mainCamera;
    private float totalScroll = 0;

    void Start()
    {
        isDragging = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = sortingOrder;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isTextSticker && isEditingText)
        {
            // Ajouter du texte avec les touches clavier
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // Backspace
                {
                    if (stickerText.Length > 0)
                    {
                        stickerText = stickerText.Substring(0, stickerText.Length - 1);
                    }
                }
                else if ((c == '\n') || (c == '\r')) // Retour à la ligne ou entrée
                {
                    isEditingText = false; // Fin de l'édition du texte
                    isFocused = false; // Désactiver l'encadrement
                }
                else if (stickerText.Length < 18)
                {
                    stickerText += c; // Ajouter le caractère au texte du sticker
                }
            }

            // Désactiver l'édition si on clique ailleurs
            if (Input.GetMouseButtonDown(0) && !IsMouseOverSticker())
            {
                isEditingText = false;
                isFocused = false;
            }
        }

        // Si ce n'est pas un sticker texte, autoriser redimensionnement et rotation
        if (!isTextSticker)
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Vérifier si la souris survole l'objet
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Redimensionnement avec la molette de la souris
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0f)
                {
                    totalScroll = Mathf.Clamp(totalScroll + scroll, -1, 1);

                    if (totalScroll != -1 && totalScroll != 1)
                    {
                        Vector3 newScale = transform.localScale + Vector3.one * 2 * scroll * scaleSpeed;
                        newScale.x = Mathf.Max(0.1f, newScale.x);
                        newScale.y = Mathf.Max(0.1f, newScale.y);
                        transform.localScale = newScale;
                    }
                }

                // Rotation en maintenant le clic droit
                if (Input.GetMouseButton(1))
                {
                    transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
                }

                // Flip horizontal avec le clic de la molette
                if (Input.GetMouseButtonDown(2)) // Bouton de la molette
                {
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                    //Vector3 currentScale = transform.localScale;
                    //currentScale.x *= -1; // Inverser l'échelle X pour un flip horizontal
                   // transform.localScale = currentScale;
                }
            }
        }
    }


    void OnMouseEnter()
    {
        if (!isDragging)
        {
            Cursor.SetCursor(cursorTextureHover, hotSpot, cursorMode);
        }
    }

    void OnMouseExit()
    {
        if (!isDragging)
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    void OnMouseDown()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Sticker_pickup");
        Cursor.SetCursor(cursorTextureClic, hotSpot, cursorMode);
        offset = transform.position - GetMouseWorldPos();
        spriteRenderer.sortingOrder = sortingOrder++;
        isDragging = true;

        if (isTextSticker)
        {
            isFocused = true; // Activer l'encadrement
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 0;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Sticker_stamp");
        isDragging = false;
        Cursor.SetCursor(cursorTextureHover, Vector2.zero, cursorMode);
        
        if (isTextSticker)
        {
            isEditingText = true; // Activer l'édition après avoir lâché le sticker
        }
    }

    void OnGUI()
    {
        if (isTextSticker)
        {
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontSize = 30; // Taille de la police
            textStyle.fontStyle = FontStyle.Bold; // Texte en gras
            textStyle.normal.textColor = Color.white; // Couleur du texte

            // Afficher le texte sur le sticker
            Vector2 stickerPosition = mainCamera.WorldToScreenPoint(transform.position);

            // Encadrer le texte avec un rectangle blanc
            if (isFocused)
            {
                Rect textRect = new Rect(stickerPosition.x + 30, Screen.height - stickerPosition.y - 15, 300, 30);
                GUI.Box(textRect, GUIContent.none); // Encadrement blanc
                GUI.Label(textRect, stickerText, textStyle);
            }
            else
            {
                GUI.Label(new Rect(stickerPosition.x + 30, Screen.height - stickerPosition.y - 15, 200, 50), stickerText, textStyle);
            }
        }
    }

    // Vérifie si la souris est au-dessus du sticker
    private bool IsMouseOverSticker()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        return hit != null && hit.gameObject == gameObject;
    }
}
