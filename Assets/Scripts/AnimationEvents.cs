using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Inventory inventory;
    private void Start()
    {
        sprite = transform.Find("Visual")?.GetComponent<SpriteRenderer>();
    }
    public void Flip()
    {
        sprite.flipY = !sprite.flipY;
    }
    public void PlaySound(AudioClip clip)
    {
        SoundStatic.PlaySound(clip);
    
    }
    public void PlaySoundEvent(Sound.SoundType e)
    {
        SoundStatic.PlaySound(e);
    }
    public void ShakeCamera(float duration)
    {
        Shake.ShakeCamera(duration);
    }
}
