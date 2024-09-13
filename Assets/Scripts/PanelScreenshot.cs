using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScreenshot : MonoBehaviour
{
    [SerializeField] private UIBlock2D Panel;
    [SerializeField] private UIBlock2D OneAffiche;
    [SerializeField] private Stacks stacks;

    public int afficheID = 0;
    private void Start()
    {
        Panel.gameObject.SetActive(false);
        OneAffiche.gameObject.SetActive(false);
    }

    public void ShowAffiche()
    {
        List<GameObject> list = stacks.GetAffiches();
        GameObject affiche = list[afficheID];
        UIBlock2D afficheBlock = OneAffiche;
        
        affiche.transform.SetParent(afficheBlock.transform.GetChild(0).transform, true);
        affiche.transform.position = afficheBlock.transform.GetChild(0).transform.position + new Vector3(0f,0.5f,0f);

        foreach (Transform child in affiche.transform)
        {
            if (child.CompareTag("Sticker"))
            {
                Vector3 position = child.transform.position;
                position.z = 0f;
                child.transform.position = position;
            }
        }
        
        //Debug.Log(afficheBlock.name);
        Panel.gameObject.SetActive(true);
        afficheBlock.gameObject.SetActive(true);
    }
}
