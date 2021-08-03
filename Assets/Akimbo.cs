using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akimbo : MonoBehaviour
{
    private Inventory inventory;
    private bool isRight;
    private void Start()
    {
        inventory = GetComponentInParent<Inventory>();
        isRight = name.Contains("Right");
    }
    public void Attack()
    {
        if(isRight)
        {
            inventory.PrimaryWeapon.Shoot(transform.parent.gameObject);
        }
        else
        {
            inventory.SecondaryWeapon.Shoot(transform.parent.gameObject);
        }
    }
    public void PlaySound(AudioClip audio)
    {
        SoundStatic.PlaySound(audio);
    }
}
