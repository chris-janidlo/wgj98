using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class SFX : Singleton<SFX>
{
    [SerializeField]
    AudioSource source;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

    public static void Play (AudioClip clip, ulong time = 0)
    {
        Instance.play(clip, time);
    }

    public static void Play (List<AudioClip> clips, ulong time = 0)
    {
        var actualClip = clips[Random.Range(0, clips.Count)];
        Instance.play(actualClip, time);
    }

    protected void play (AudioClip clip, ulong time)
    {
        source.clip = clip;
        source.Play(time);
    }
}
