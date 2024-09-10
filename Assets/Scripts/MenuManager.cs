using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        GameManager.Instance.StartGame();
    }


    public void OnTutoButtonClick()
    {
        GameManager.Instance.ChangeScene("TutoScene");
    }
}
