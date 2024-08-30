using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour
{
    public audioClip[] Soundlist;

    public static audioManager instance;


    private void Awake()
    {
        foreach (audioClip sound in Soundlist)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

        }
    }
    public void PlaySound(string name)
    {
        audioClip sound = Array.Find(Soundlist, Audio => Audio.SoundName == name);
        sound.source.Play();
    }
    public void StopSound(string name)
    {
        audioClip sound = Array.Find(Soundlist, Audio => Audio.SoundName == name);
        sound.source.Stop();
    }


    void Start()
    {

        instance = this;
    }


}