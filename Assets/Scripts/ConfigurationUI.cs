using Nova;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigurationUI : MonoBehaviour
{
    [SerializeField] private UIBlock2D uiGame;
    [SerializeField] private UIBlock2D PanelWin;
    [SerializeField] private UIBlock2D PanelCommentary;
    [SerializeField] private UIBlock2D PanelScreenshot;
    [SerializeField] private GameObject screenshotButton;

    private string screenshotsFolder;   
    private void Start()
    {
        screenshotsFolder = Application.dataPath + "/Screenshots/";
        uiGame.gameObject.SetActive(true);
        PanelWin.gameObject.SetActive(false);
        PanelCommentary.gameObject.SetActive(false);
        PanelScreenshot.gameObject.SetActive(false);
    }

    public void StartCommentary()
    {
        uiGame.gameObject.SetActive(false);
        PanelWin.gameObject.SetActive(false);
        PanelScreenshot.gameObject.SetActive(false);
        StartCoroutine(ActivateAndCallFunction(PanelCommentary.gameObject));
    }

    IEnumerator ActivateAndCallFunction(GameObject obj)
    {
        obj.SetActive(true);
        yield return null; // Attend une frame
        obj.GetComponent<PanelCommentary>().ShowAffiche();
    }

    public void StartScreenshotOne() 
    {
        PanelScreenshot.GetComponent<PanelScreenshot>().afficheID = 0;
        StartScreenshot();
    }
    public void StartScreenshotTwo() 
    {
        PanelScreenshot.GetComponent<PanelScreenshot>().afficheID = 1;
        StartScreenshot();
    }
    public void StartScreenshot()
    {
        uiGame.gameObject.SetActive(false);
        PanelWin.gameObject.SetActive(false);
        PanelCommentary.gameObject.SetActive(false);
        StartCoroutine(ActivateAndCallFunctionScreenshot(PanelScreenshot.gameObject));
    }

    IEnumerator ActivateAndCallFunctionScreenshot(GameObject obj)
    {
        obj.SetActive(true);
        yield return null; // Attend une frame
        obj.GetComponent<PanelScreenshot>().ShowAffiche();
    }

    public void StartPanelWin()
    {
        uiGame.gameObject.SetActive(false);
        PanelCommentary.gameObject.SetActive(false);
        PanelScreenshot.gameObject.SetActive(false);
        PanelWin.gameObject.SetActive(true);
    }

    public void StartPanelLose()
    {
        uiGame.gameObject.SetActive(false);
        PanelCommentary.gameObject.SetActive(false);
        PanelWin.gameObject.SetActive(false);
        PanelScreenshot.gameObject.SetActive(false);
    }

    public void OnMainMenu()
    {
        GameManager.Instance.ChangeScene("MainMenu");
    }
    public void OnRetry()
    {
        GameManager.Instance.StartGame();
    }

    public void TakeScreenShot() 
    {
        string screenshotName = "screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string screenshotPath = Path.Combine(screenshotsFolder, screenshotName);
        Debug.Log(screenshotPath);
        ScreenCapture.CaptureScreenshot(screenshotPath);

        StartCoroutine(WaitStart());
    }


    private IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(1);
        StartCommentary();
    }
}
