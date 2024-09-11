using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    //private float zCoord = 0;
    private SpriteRenderer spriteRenderer;
    private static int sortingOrder = 10;
    private bool isDragging = false;

    [SerializeField] private Texture2D cursorTextureHover;
    [SerializeField] private Texture2D cursorTextureClic;

    // Variables de configuration pour le curseur
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public float scaleSpeed = 0.1f; // Vitesse de mise à l'échelle
    public float rotationSpeed = 150f; // Vitesse de rotation
    private Camera mainCamera;
    private float totalScroll = 0;

    void Start()
    {
        Vector3 position = transform.position;
        position.z = 0f;
        transform.position = position;
        isDragging = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Initialisation de l'ordre de tri (sorting order) pour gérer la superposition des objets
        spriteRenderer.sortingOrder = sortingOrder;
        mainCamera = Camera.main;
    }

    void Update()
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

                if (totalScroll != 0 && totalScroll != 1)
                {
                    Vector3 newScale = transform.localScale + Vector3.one * scroll * scaleSpeed;
                    newScale.x = Mathf.Max(0.1f, newScale.x); // Empêcher la taille négative
                    newScale.y = Mathf.Max(0.1f, newScale.y);
                    transform.localScale = newScale;
                }
            }

            // Rotation en maintenant le clic droit
            if (Input.GetMouseButton(1)) // Bouton droit de la souris
            {
                transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
        }
    }


    void OnMouseEnter()
    {
        Debug.Log(isDragging);
        if (!isDragging)
        {
            // Changement du curseur quand la souris survole l'objet
            Cursor.SetCursor(cursorTextureHover, hotSpot, cursorMode);
        }
    }

    void OnMouseExit()
    {
        Debug.Log(isDragging);
        if (!isDragging)
        {
            // Réinitialisation du curseur quand la souris quitte l'objet
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    void OnMouseDown()
    {
        // Changement du curseur lors du clic
        Cursor.SetCursor(cursorTextureClic, hotSpot, cursorMode);

        // Récupération de la coordonnée Z et calcul de l'offset pour maintenir la position de l'objet par rapport à la souris
        //zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();

        // Augmentation de l'ordre de tri pour mettre l'objet au-dessus des autres quand il est sélectionné
        spriteRenderer.sortingOrder = sortingOrder++;

        isDragging = true;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 0; // zCoord; // On garde la coordonnée Z originale pour éviter des mouvements sur cet axe
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        // Fin du drag, réinitialisation du curseur
        isDragging = false;
        Cursor.SetCursor(cursorTextureHover, Vector2.zero, cursorMode);
    }
}
