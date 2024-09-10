using Nova;
using NovaSamples.UIControls;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    [SerializeField] private Zeus zeus;
    [SerializeField] private UIBlock2D block;
    [SerializeField] private Button nextAfficheButton;
    private DragAndDrop[] dragAndDrops;
    // Start is called before the first frame update
    void Start()
    {

        zeus.gameObject.SetActive(false);
        block.gameObject.SetActive(false);
        nextAfficheButton.gameObject.SetActive(false);
        dragAndDrops = FindObjectsOfType<DragAndDrop>();
        foreach( DragAndDrop dragAndDrop in dragAndDrops)
        {
            dragAndDrop.enabled = false;
        }
        StartCoroutine(TutoStarter(5));
        GameManager.Instance.PauseGame();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator TutoStarter(int _timer)
    {
        yield return new WaitForSeconds(_timer);
        zeus.gameObject.SetActive(true);
        block.gameObject.SetActive(true);

    }

    public void ActiveDragAndDrops()
    {
        foreach (DragAndDrop dragAndDrop in dragAndDrops)
        {
            dragAndDrop.enabled = true;
        }
    }

    public void ActiveNextButtonTuto()
    {
        nextAfficheButton.gameObject.SetActive(true);
    }

    public void ActiveNextAfficheButton()
    {
        nextAfficheButton.gameObject.SetActive(true);
    }
}
