using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLand : MonoBehaviour
{
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = weapon.WeaponBase.Sprite;
    }
    public Weapon weapon;
}
