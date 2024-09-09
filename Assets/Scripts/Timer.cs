using Nova.TMP;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timer = 300f;
    private GameManager gameManager;

    [SerializeField] private TextMeshProTextBlock textBlock;
    private string _textContent;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        _textContent = string.Format("{0}:{1:00}", Mathf.FloorToInt(timer / 60), (int)timer % 60);
        textBlock.text = _textContent;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsPlaying())
        {
            timer -= Time.deltaTime;

            _textContent = string.Format("{0}:{1:00}", Mathf.FloorToInt(timer / 60), (int)timer % 60);
            textBlock.text = _textContent;

            if (timer < 0)
            {
                timer = 0;
                gameManager.PauseGame();
            }
        }
    }
}
