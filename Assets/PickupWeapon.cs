using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] private WeaponLand closestWeapon;
    private Inventory inventory;
    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WeaponLand wl))
        {
            closestWeapon = wl;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
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
            if (!inventory.PrimaryWeapon.isNone())
            {
                Rigidbody2D item = Instantiate(PrefabsStatic.Weapon, transform.position, Quaternion.Euler(transform.Find("Visual").eulerAngles)).GetComponent<Rigidbody2D>();
                item.angularVelocity = 180;
                item.GetComponent<WeaponLand>().weapon = inventory.PrimaryWeapon.Copy();
                inventory.PrimaryWeapon.Destroy();
                item.velocity = item.transform.right * 20;
            }
            if (closestWeapon != null)
            {
                if (inventory.PrimaryWeapon.isNone())
                {
                    inventory.PrimaryWeapon = closestWeapon.weapon;
                    Destroy(closestWeapon.gameObject);
                }
            }
        }
    }
}
