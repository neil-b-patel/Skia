using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("SOUND: " + name + " NOT FOUND!");
            return;
        }

        s.source.Play();
    }

    public void Stop (string name, bool fade=false)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("SOUND: " + name + " NOT FOUND!");
            return;
        }

        if (fade)
        {
            Debug.Log("FADE STOP");
            s.source.Stop();
        }
        else
        {
            s.source.Stop();
        }
    }
}
