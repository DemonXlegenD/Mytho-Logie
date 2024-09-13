using Nova;
using UnityEngine;
using UnityEngine.UIElements;

public class Chibi : MonoBehaviour
{
    private UIBlock2D background;
    [SerializeField] private bool isAnimationActive = false;
    public float colorFadeDuration = 0.5f; // Durée du changement de couleur

    private Color startColor = Color.white;
    private Color targetColor = Color.yellow;
    private float fadeTimer = 0f;
    private bool changingToYellow = true;

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<UIBlock2D>();
        gameObject.SetActive(false);
        SetColor(startColor);
    }



    void Update()
    {
        if (isAnimationActive)
        {
            // Faire avancer le timer
            fadeTimer += Time.deltaTime;

            if (fadeTimer > colorFadeDuration)
            {
                fadeTimer = 0f;
                changingToYellow = !changingToYellow;
                targetColor = changingToYellow ? Color.yellow : Color.white;
            }

            // Calculer la couleur interpolée basée sur le timer
            Color currentColor = Color.Lerp(background.Color, targetColor, fadeTimer / colorFadeDuration);
            SetColor(currentColor);
        }
    }

    private void SetColor(Color color)
    {
        background.Color = color;
    }

    public void ActiveAnime()
    {
        isAnimationActive = true;
    }

    public void UnactiveAnime()
    {
        isAnimationActive = false;
        SetColor(startColor);
    }

    public void ShowChibi()
    {
        gameObject.SetActive(true);
    }
}
