using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill : MonoBehaviour
{
    [SerializeField] private UIBlock2D m_fill;
    private UIBlock2D parentBlock;

    public float percentageX = 1f;
    // Start is called before the first frame update
    void Start()
    {
        parentBlock = GetComponent<UIBlock2D>();
        m_fill.Size.X = 0;
        m_fill.Size.Y = parentBlock.Size.Y.Value;
    }

    public void ChangeSize(float percent)
    {
          
                // Utilise la taille X du parent comme référence
                float referenceSizeX = parentBlock.Size.X.Value;
                Debug.Log("RefSize " + referenceSizeX);
                // Calcule la nouvelle taille en X en fonction du pourcentage
                float newSize = m_fill.Size.X.Value;
            Debug.Log(newSize);
            newSize = referenceSizeX * percent;
            Debug.Log("Percent " + percent);
            Debug.Log("NewSize " + newSize);
            // Applique la nouvelle taille
            m_fill.Size.X.Value = newSize;
    }
}
