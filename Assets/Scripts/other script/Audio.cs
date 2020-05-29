using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Audio
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
    public bool loop;
}
