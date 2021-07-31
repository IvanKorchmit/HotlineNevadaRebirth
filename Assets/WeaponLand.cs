using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLand : MonoBehaviour
{
    public Weapon weapon;
    private IEnumerator Start()
    {
        GetComponent<SpriteRenderer>().sprite = weapon.WeaponBase.Sprite;
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        circle.enabled = false;
        yield return new WaitForEndOfFrame();
        circle.enabled = true;

    }
}
