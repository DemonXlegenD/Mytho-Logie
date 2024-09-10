using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float scale = 2f;
    public Vector3 baseLocalScale = new Vector3(1f, 1f, 1f);
    public float timer = 0f;
    public float duration = 1f;
    public bool flipFlop = false;

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
        } else
        {
            flipFlop = !flipFlop;
            timer = 0f;
        }


    }



}
