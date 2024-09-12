using Nova;
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
        m_fill.Size.Y = parentBlock.Size.Y.Value - 3;
    }

    public void ChangeSize(float percent)
    {
        if (parentBlock)
        {
            float referenceSizeX = parentBlock.Size.X.Value;
            float newSize = m_fill.Size.X.Value;
            newSize = referenceSizeX * percent;
            m_fill.Size.X.Value = newSize;
        }

    }
}
