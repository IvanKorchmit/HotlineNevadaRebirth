using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineLand : MonoBehaviour
{
    public MagazineItem magazine;
    private void Awake()
    {
        magazine.ammo = magazine.magazine.AmmoCapacity;
        GetComponent<SpriteRenderer>().sprite = magazine.magazine.Sprite;
    }
}
