using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }
    public void Flip()
    {
        sprite.flipY = !sprite.flipY;
    }
}
