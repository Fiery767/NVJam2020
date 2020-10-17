using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorEngine : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Clip;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip MyClip)
    {
        Source.enabled = true;
        if (Source.isActiveAndEnabled)
        {
            Source.clip = MyClip;
            Source.Play();
        }
    }
}
