using NovaSamples.UIControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jauges : MonoBehaviour
{
     private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();    
    }

    public void Change(float value)
    {
        slider.Value = value;
    }
    
    
}
