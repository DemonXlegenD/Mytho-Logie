using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float scale = 2f;
    public Vector3 baseLocalScale = new Vector3(1f, 1f, 1f);
    public float timer = 0f;
    public float duration = 1f;
    public bool flipFlop = false;

    [SerializeField] private Vector3 affichePosition = new Vector3(6f, 0f, 0f);
    [SerializeField] private Vector3 afficheRotation = new Vector3(0f, 0f, -80f);

    [SerializeField] private Vector3 stickersPosition = new Vector3(-2.5f, 0f, 0f);
    [SerializeField] private Vector3 stickersRotation = new Vector3(0f, 0f, -80f);

    [SerializeField] private Vector3 placeStickersPosition = new Vector3(5f, 0.65f, 0f);
    [SerializeField] private Vector3 placeStickersRotation = new Vector3(0f, 0f, -80f);

    [SerializeField] private Vector3 nextPosition = new Vector3(-2.5f, 0.65f, 0f);
    [SerializeField] private Vector3 nextRotation = new Vector3(0f, 0f, -80f);

    private void Start()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

        // Si le timer est inférieur à la durée
        if (timer < duration)
        {
            // On incrémente le timer en fonction du temps écoulé entre chaque frame
            timer += Time.deltaTime;

            // Calculer le pourcentage de progression (de 0 à 1)
            float t = timer / duration;

            // Interpoler entre startValue et endValue
            if (flipFlop) transform.localScale = Vector3.Lerp(transform.localScale, baseLocalScale, t);
            else transform.localScale = Vector3.Lerp(transform.localScale, baseLocalScale * scale, t);
        }
        else
        {
            flipFlop = !flipFlop;
            timer = 0f;
        }


    }

    public void GoToAffiche()
    {
        gameObject.SetActive(true);
        transform.position = affichePosition;
        transform.rotation = Quaternion.Euler(afficheRotation);
    }

    public void GoToStickers()
    {
        transform.position = stickersPosition;
        transform.rotation = Quaternion.Euler(stickersRotation);
    }

    public void GoToPlaceStickers()
    {
        transform.position = placeStickersPosition;
        transform.rotation = Quaternion.Euler(placeStickersRotation);
    }

    public void GoToNextButton()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        transform.position = nextPosition;
        transform.rotation = Quaternion.Euler(nextRotation);
    }


}
