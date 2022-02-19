using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume =s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.IsLoop;
            s.source.name = s.name;
        }   
    }
    void Start()
    {
        PLay("Background");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void PLay(string name)
    {
        Sound s =Array.Find(sounds, sound => sound.name ==name);
        if (s == null)
        {
            return;
        }
       
        s.source.Play();
    }

}
