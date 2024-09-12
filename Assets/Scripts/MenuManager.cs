using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool isMute = false;
    public void OnPlayButtonClick()
    {
        GameManager.Instance.StartGame();
    }


    public void OnTutoButtonClick()
    {
        GameManager.Instance.ChangeScene("TutoScene");
    }

    public void OnSoundButtonClick()
    {
        if(isMute) GameManager.Instance.UnmuteAudio();
        else GameManager.Instance.MuteAudio();
        isMute = !isMute;
    }
}
