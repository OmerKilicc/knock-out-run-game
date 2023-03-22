using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Channel", menuName = "Sound/Channel")]
public class SoundPlayChannelSO : ScriptableObject
{
    public AudioClip Sound;
    public int SoundChannel;
    public bool Randomize = false;
    public float PitchMid = 1f;
    public float RandomRange = 0.2f;
    public float Volume = 1f;

    public void Play() => SoundManager.Play(Sound, SoundChannel, Randomize, PitchMid, RandomRange, Volume);
}
