using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class Affiche : MonoBehaviour
{
    [SerializeField] private Vector2 stockPosition = new Vector2(1000, 0);
    bool isNext = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isNext)
        {
            Debug.Log(transform.position);
            transform.position = Vector2.Lerp(transform.position, stockPosition, 5.0f * Time.deltaTime);
        }
    }

    public void Next()
    {
        Debug.Log("Next");
        isNext = true;
    }
}
