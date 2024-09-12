using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_audio : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Event;
    public bool PlayOnAwake;

    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
    }


    private void Start()
    {
        if (PlayOnAwake)
            PlayOneShot();

    }
        
        
    
       

    
}
