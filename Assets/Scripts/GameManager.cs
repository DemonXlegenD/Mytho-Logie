using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    IsPlaying, IsLoading, Pause
}
public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public GameState _state = GameState.IsPlaying;
    public float volume = -1f;
    public int currentLvlID = 0;
    public int[] scoreCapToChangeLvl = new int[] { 40, 45, 50, 55, 60 };
    public int score = 0;
    private string previousLoadedScene = null;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        previousLoadedScene = SceneManager.GetActiveScene().name;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public bool IsPlaying()
    {
        return _state == GameState.IsPlaying;
    }

    public void ChangeScene(string _sceneName)
    {
        previousLoadedScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(_sceneName);

    }

    public void PauseGame()
    {
        _state = GameState.Pause;

        //Time.timeScale = 0f;
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousLoadedScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}