using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSound : MonoBehaviour
{
    public AudioClip flashlightAudio;
    public AudioSource flashlightSound;
    void Start()
    {
        flashlightSound.Play();
    }



}
