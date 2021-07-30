using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] private WeaponLand closestWeapon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WeaponLand wl))
        {
            closestWeapon = wl;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WeaponLand wl))
        {
            if (closestWeapon == wl)
            {
                closestWeapon = null;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (closestWeapon != null)
            {
                GetComponent<Inventory>().PrimaryWeapon = closestWeapon.weapon;
                Destroy(closestWeapon.gameObject);
            }
        }
    }
}
