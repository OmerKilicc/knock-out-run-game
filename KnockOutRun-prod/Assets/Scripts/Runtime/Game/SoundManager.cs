using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource _source;

    [SerializeField] List<AudioSource> _channels = new List<AudioSource>();
    [SerializeField] List<AudioData> _clips = new List<AudioData>();
    [SerializeField] float _randomizeRange = 0.3f;

    public static void Play(string name) => Play(name, 0, false);
    public static void Play(string name, int channel) => Play(name, channel, false);
    public static void Play(string name, int channel, bool randomize, float pitchMid = 1f, float _randRange = 0.2f, float volume = 1f)
    {
        foreach (var data in Instance._clips)
        {
            if (data.Name != name)
                continue;

            if (Instance._channels.Count < 1)
                return;

            channel = channel < Instance._channels.Count ? channel : Instance._channels.Count - 1;
            AudioSource src = Instance._channels[channel];
            src.clip = data.Clip;

            float pitch = randomize ? Random.Range(pitchMid - _randRange, pitchMid + _randRange) : 1f;
            src.pitch = pitch;
            src.volume = volume;
            src.Play();
        }
    }

    public static void Play(AudioClip clip, int channel, bool randomize, float pitchMid = 1f, float _randRange = 0.2f, float volume = 1f)
    {
        channel = channel < Instance._channels.Count ? channel : Instance._channels.Count - 1;
        AudioSource src = Instance._channels[channel];
        src.clip = clip;

        float pitch = randomize ? Random.Range(pitchMid - _randRange, pitchMid + _randRange) : 1f;
        src.pitch = pitch;
        src.volume = volume;
        src.Play();
    }
}

[System.Serializable]
struct AudioData
{
    public string Name;
    public AudioClip Clip;
}