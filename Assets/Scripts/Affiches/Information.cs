using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{
    [SerializeField] private GameObject m_One;
    [SerializeField] private GameObject m_Two;

    private void Start()
    {
        m_One.SetActive(true);
        m_Two.SetActive(false);
    }

    public void Switch()
    {
        m_One.SetActive(!m_One.active);
        m_Two.SetActive(!m_Two.active);
    }
}
