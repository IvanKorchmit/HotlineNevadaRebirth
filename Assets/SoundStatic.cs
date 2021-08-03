using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStatic : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    private static Sound[] soundsStatic;
    private void Start()
    {
        soundsStatic = sounds;
        Destroy(gameObject);
    }
    public static void PlaySound(AudioClip clip)
    {
        AudioSource audio = new GameObject($"{clip.name} AUDIO", typeof(AudioSource)).GetComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();
        Destroy(audio.gameObject, clip.length);
    }
    public static void PlaySound(Sound.SoundType e)
    {
        foreach (var sfx in soundsStatic)
        {
            if(sfx.type == e)
            {
                PlaySound(sfx.GetSound());
            }
        }
    }
}
[System.Serializable]
public class Sound
{
    public enum SoundType
    {
        bulletFlesh, gibExplosion
    }
    public SoundType type;
    public AudioClip[] sounds;
    public AudioClip GetSound()
    {
        return sounds[Random.Range(0, sounds.Length)];
    }
}