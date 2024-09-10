using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCommentary : MonoBehaviour
{
    [SerializeField] private UIBlock2D Panel;
    [SerializeField] private UIBlock2D OneAffiche;
    [SerializeField] private UIBlock2D TwoAffiche; 
    [SerializeField] private UIBlock2D ThreeAffiche;

    [SerializeField] private Stacks stacks;

    private void Start()
    {
        Panel.gameObject.SetActive(false);
        OneAffiche.gameObject.SetActive(false);
        TwoAffiche.gameObject.SetActive(false); 
        ThreeAffiche.gameObject.SetActive(false);
    }

    public void ShowAffiche()
    {
        List<GameObject> list = stacks.GetAffiches();
        UIBlock2D afficheBlock = list.Count > 1 ? (list.Count == 3 ? ThreeAffiche : TwoAffiche) : OneAffiche;
        for (int i = 0; i < list.Count; i++) {

            list[i].transform.SetParent(afficheBlock.transform.GetChild(i).transform, true);
            list[i].transform.position = afficheBlock.transform.GetChild(i).transform.position + new Vector3(0f,0.5f,0f);
        }
        Debug.Log(afficheBlock.name);
        Panel.gameObject.SetActive(true);
        afficheBlock.gameObject.SetActive(true);
    }
}
