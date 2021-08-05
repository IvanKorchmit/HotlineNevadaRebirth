using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] private WeaponLand closestWeapon;
    [SerializeField] private MagazineLand closestMagazine;
    private Inventory inventory;
    private Animator animator;
    private void Start()
    {
        inventory = GetComponent<Inventory>();
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out closestWeapon);
        collision.TryGetComponent(out closestMagazine);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.TryGetComponent(out closestWeapon);
        collision.TryGetComponent(out closestMagazine);
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
        if (collision.TryGetComponent(out MagazineLand ml))
        {
            if (closestMagazine == ml)
            {
                closestMagazine = null;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PickUpWeapon();
        }
        if(Input.GetKeyDown(KeyCode.Mouse2))
        {
            PickUpWeapon(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (closestMagazine != null)
            {
                inventory.AddItem(closestMagazine.magazine);
                Destroy(closestMagazine.gameObject);
            }
        }
    }
    private void PickUpWeapon(bool isAkimbo = false)
    {
        if (!isAkimbo)
        {
            if (!inventory.PrimaryWeapon.isNone())
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Akimbo"))
                {
                    Rigidbody2D item = Instantiate(PrefabsStatic.Weapon, transform.position, Quaternion.Euler(transform.Find("Visual").eulerAngles)).GetComponent<Rigidbody2D>();
                    item.angularVelocity = 180;
                    item.GetComponent<WeaponLand>().weapon = inventory.PrimaryWeapon.Copy();
                    inventory.PrimaryWeapon.Destroy();
                    item.velocity = item.transform.right * 20;
                    animator.Play("No Gun", 0);
                }
            }
            if (closestWeapon != null)
            {

                if (inventory.PrimaryWeapon.isNone() && animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
                {
                    inventory.PrimaryWeapon = closestWeapon.weapon;
                    animator.Play("Neutral", 0);
                    Destroy(closestWeapon.gameObject);
                }
            }
        }
        else
        {
            if (inventory.SecondaryWeapon.isNone())
            {
                if (closestWeapon != null)
                {
                    if (!inventory.PrimaryWeapon.isNone() && animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
                    {
                        inventory.SecondaryWeapon = closestWeapon.weapon;
                        animator.Play("Akimbo", 0);
                        Destroy(closestWeapon.gameObject);
                    }
                }
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Akimbo"))
                {
                    Rigidbody2D item = Instantiate(PrefabsStatic.Weapon, transform.position, Quaternion.Euler(transform.Find("Visual").eulerAngles)).GetComponent<Rigidbody2D>();
                    item.angularVelocity = 180;
                    item.GetComponent<WeaponLand>().weapon = inventory.SecondaryWeapon.Copy();
                    inventory.SecondaryWeapon.Destroy();
                    item.velocity = item.transform.right * 20;
                    animator.Play("Neutral", 0);
                }
            }
        }
    }
}
