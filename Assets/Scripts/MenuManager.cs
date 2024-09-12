using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonStart;
    [SerializeField] private GameObject buttonQuit;
    [SerializeField] private GameObject panelQuit;

    public bool isMute = false;
    public void OnPlayButtonClick()
    {
        GameManager.Instance.StartGame();
    }

    public void OnQuitButtonClick()
    {
        ShowPanelQuit();
    }

    public void OnTutoButtonClick()
    {
        GameManager.Instance.ChangeScene("TutoScene");
    }

    public void OnYesButtonClick()
    {
        GameManager.Instance.Quit();
    }

    public void OnNoButtonClick()
    {
        HidePanelQuit();
    }

    public void OnSoundButtonClick()
    {
        if(isMute) GameManager.Instance.UnmuteAudio();
        else GameManager.Instance.MuteAudio();
        isMute = !isMute;
    }


    public void HidePanelQuit()
    {
        buttonQuit.GetComponent<Interactable>().enabled = true;
        buttonStart.GetComponent<Interactable>().enabled = true;
        panelQuit.SetActive(false);
    }

    public void ShowPanelQuit()
    {
        buttonQuit.GetComponent<Interactable>().enabled = false;
        buttonStart.GetComponent<Interactable>().enabled = false;
        panelQuit.SetActive(true);
    }
}
