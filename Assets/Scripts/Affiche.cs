using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class Affiche : MonoBehaviour
{
    [SerializeField] private Vector2 stockPosition = new Vector2(10, 0);
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
            transform.position = Vector2.Lerp(transform.position, stockPosition, 5.0f * Time.deltaTime);
        }
    }

    public void Next()
    {
        isNext = true;
    }
}
