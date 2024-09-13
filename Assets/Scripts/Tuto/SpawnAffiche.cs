using Nova;
using UnityEngine;

public class SpawnAffiche : MonoBehaviour
{
    [SerializeField] private Tuto tuto;
    [SerializeField] private GameObject affiche;
    [SerializeField] private UIBlock2D background;

    [SerializeField] private bool isAnimationActive = false;

    public float fadeDuration = 0.5f; // Durée de la montée et de la descente de la transparence

    private float targetAlpha = 1f;
    private float fadeTimer = 0f;
    private bool fadingIn = true;

    private bool changeTextOnce = false;
    private bool changeTextTwice = false;
    private bool changeTextThree = false;
    private void Start()
    {
        affiche.SetActive(false);
        affiche.GetComponent<Affiche>().SetIsMainAffiche(true);
        SetAlpha(0f);
    }

    void Update()
    {
        if (isAnimationActive)
        {
            // Faire avancer le timer
            fadeTimer += Time.deltaTime;

            if (fadeTimer > fadeDuration)
            {
                fadeTimer = 0f;
                fadingIn = !fadingIn;
                targetAlpha = fadingIn ? 1f : 0f; // 1 pour 100% opaque, 0 pour transparent
            }

            // Calculer l'alpha à appliquer basé sur le timer
            float alpha = Mathf.Lerp(background.Color.a, targetAlpha, fadeTimer / fadeDuration);
            SetAlpha(alpha);
        }

        if (affiche.GetComponent<Affiche>().stickersIn.Count == 1 && !changeTextOnce)
        {
            changeTextOnce = true;
            tuto.NextStep();

        }


        if (affiche.GetComponent<Affiche>().stickersIn.Count == 3 && !changeTextThree)
        {
            changeTextThree = true;
            tuto.NextStep();

        }

        if (affiche.GetComponent<Affiche>().stickersIn.Count == 4 && !changeTextTwice)
        {
            changeTextTwice = true;
            tuto.NextStep();
        }
    }

    private void SetAlpha(float alpha)
    {
        Color currentColor = background.Color;
        currentColor.a = alpha;
        background.Color = currentColor;
    }

    public void ShowChild()
    {
        affiche.SetActive(true);
    }

    public void ActiveAnime()
    {
        isAnimationActive = true;
    }

    public void UnactiveAnime()
    {
        isAnimationActive = false;
        SetAlpha(0f);
    }

    public GameObject GetAffiche() { return affiche; }


    
}
