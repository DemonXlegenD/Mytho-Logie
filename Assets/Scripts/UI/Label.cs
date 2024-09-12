using Nova;
using UnityEngine;

public class Label : MonoBehaviour
{
    private UIBlock2D labelBlock;
    private void Start()
    {
        labelBlock = GetComponent<UIBlock2D>();
    }

    public void HandleHover()
    {
        labelBlock.Size.Percent = new Vector3(0.40f, 0.40f, 0f);

    }

    public void HangleUnhover()
    {
        labelBlock.Size.Percent = new Vector3(0.5f, 0.5f, 0f);

    }
}
