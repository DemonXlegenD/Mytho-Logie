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
        Vector3 position = transform.position;
        position.z = 0f;
        transform.position = position;
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
                }
                else if (stickerText.Length < 18)
                {
                    stickerText += c; // Ajouter le caractère au texte du sticker
                }
            }
        }

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Vérifier si la souris survole l'objet
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            //Debug.Log("HAAAN");
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
        Cursor.SetCursor(cursorTextureClic, hotSpot, cursorMode);
        offset = transform.position - GetMouseWorldPos();
        spriteRenderer.sortingOrder = sortingOrder++;
        isDragging = true;

        if (isTextSticker)
        {
            isEditingText = true; // Activer l'édition du texte au clic
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
        isDragging = false;
        Cursor.SetCursor(cursorTextureHover, Vector2.zero, cursorMode);
        if (isTextSticker)
        {
            isEditingText = false; // Désactiver l'édition lorsque l'on relâche la souris
        }
    }

    void OnGUI()
    {
        if (isTextSticker)
        {
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontSize = 20; // Taille de la police
            textStyle.fontStyle = FontStyle.Bold; // Texte en gras
            textStyle.normal.textColor = Color.white; // Couleur du texte

            // Afficher le texte sur le sticker
            Vector2 stickerPosition = mainCamera.WorldToScreenPoint(transform.position);
            GUI.Label(new Rect(stickerPosition.x + 18, Screen.height - stickerPosition.y - 12, 200, 50), stickerText, textStyle);
        }
    }
}
