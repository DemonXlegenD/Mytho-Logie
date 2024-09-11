using NovaSamples.UIControls;
using System;
using UnityEngine;

public class Jauge : MonoBehaviour
{

    public float modifyValuePerTimer = 5f;
    public float timer = 0f;

    private float m_Value;
    [SerializeField] private float m_Min = -50f;
    [SerializeField] private float m_Max = 50f;

    [SerializeField] private Slider n_Slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AddToValue(GameManager.Instance.score);
    }

    public void ChangeValue()
    {
        Mathf.Clamp(m_Value, m_Min, m_Max);
        n_Slider.Value = m_Value;
    }

    public void AddToValue(float _adding)
    {
        m_Value += _adding;
        ChangeValue();
    }

    public void SubToValue(float _subing)
    {
        m_Value -= _subing;
        ChangeValue();
    }


    public float GetValue() { return m_Value; } 
}
