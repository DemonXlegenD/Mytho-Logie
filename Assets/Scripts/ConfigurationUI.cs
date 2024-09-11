using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationUI : MonoBehaviour
{
    [SerializeField] private UIBlock2D uiGame;
    [SerializeField] private UIBlock2D PanelWin;
    [SerializeField] private UIBlock2D PanelLose;
    [SerializeField] private UIBlock2D PanelCommentary;

    private void Start()
    {
        uiGame.gameObject.SetActive(true);
        PanelWin.gameObject.SetActive(false);
        PanelCommentary.gameObject.SetActive(false);
        PanelLose.gameObject.SetActive(false);
    }

    public void StartCommentary()
    {
        uiGame.gameObject.SetActive(false);
        PanelWin.gameObject.SetActive(false);
        PanelLose.gameObject.SetActive(false);
        StartCoroutine(ActivateAndCallFunction(PanelCommentary.gameObject));
    }

    IEnumerator ActivateAndCallFunction(GameObject obj)
    {
        obj.SetActive(true);
        yield return null; // Attend une frame
        obj.GetComponent<PanelCommentary>().ShowAffiche();
    }


    public void StartPanelWin()
    {
        uiGame.gameObject.SetActive(false);
        PanelCommentary.gameObject.SetActive(false);
        PanelLose.gameObject.SetActive(false);
        PanelWin.gameObject.SetActive(true);
    }

    public void StartPanelLose()
    {
        uiGame.gameObject.SetActive(false);
        PanelCommentary.gameObject.SetActive(false);
        PanelWin.gameObject.SetActive(false);
        PanelLose.gameObject.SetActive(true);
    }

    public void OnMainMenu()
    {
        GameManager.Instance.ChangeScene("MainMenu");
    }
    public void OnRetry()
    {
        GameManager.Instance.StartGame();
    }
}
