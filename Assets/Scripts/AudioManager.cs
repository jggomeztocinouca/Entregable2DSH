using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        if (clip != null) { source.PlayOneShot(clip);  }
    }
}
