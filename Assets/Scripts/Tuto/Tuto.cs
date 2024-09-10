using Nova;
using System.Collections;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    [SerializeField] private Zeus zeus;
    [SerializeField] private UIBlock2D block;
    // Start is called before the first frame update
    void Start()
    {

        zeus.gameObject.SetActive(false);
        block.gameObject.SetActive(false);
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
}
