using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Inventory inventory;
    private Transform shellPoint;
    [SerializeField] private MagazineItem oldMagazine;
    private void Start()
    {
        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
        shellPoint = transform.Find("Visual/ShellPoint");
    }
    private void Update()
    {
        if (!inventory.PrimaryWeapon.isNone())
        {
            animator.SetInteger("Weapon", inventory.PrimaryWeapon.WeaponBase.ID);
        }
        else
        {
            animator.SetInteger("Weapon", 0);
        }
        if (inventory.SecondaryWeapon.isNone())
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if(inventory.PrimaryWeapon.isNone() || inventory.PrimaryWeapon.WeaponBase is Melee)
                {
                    animator.SetBool("Attack", true);
                }
                else if (inventory.PrimaryWeapon.WeaponBase is Firearm)
                {
                    if(inventory.PrimaryWeapon.Ammo > 0)
                    {
                        animator.SetBool("Attack", true);
                    }
                }
            }
            else
            {
                animator.SetBool("Attack", false);
            }
            if (Input.GetKeyDown(KeyCode.R) && !inventory.PrimaryWeapon.isNone() && inventory.PrimaryWeapon.WeaponBase is Firearm)
            {
                if (inventory.PrimaryWeapon.Magazine != null && inventory.hasThisMagazine(inventory.PrimaryWeapon.Magazine))
                {
                    oldMagazine = inventory.PrimaryWeapon.MagazineBase.Copy();
                    animator.SetBool("Reloading", true);
                }
                else
                {
                    MagazineItem magazine = inventory.FindMagazine(inventory.PrimaryWeapon.MagazineBase).Copy();
                    if (magazine != null && magazine.magazine != null)
                    {
                        animator.SetBool("Reloading", true);
                        oldMagazine = inventory.PrimaryWeapon.MagazineBase.Copy();
                        inventory.PrimaryWeapon.Magazine = magazine.magazine;

                        if(inventory.PrimaryWeapon.WeaponBase is Firearm fa)
                        {
                            if(fa.RequiresMagazine)
                            {
                                inventory.PrimaryWeapon.Ammo = magazine.ammo;
                            }
                        }
                    }
                }
            }
        }
    }
    public void Attack()
    {
        inventory.PrimaryWeapon.Shoot(gameObject);
    }
    public void AltAttack ()
    {
        inventory.SecondaryWeapon.Shoot(gameObject);
    }
    public void Eject()
    {
        if (!inventory.PrimaryWeapon.isNone() && inventory.PrimaryWeapon.WeaponBase is Firearm f)
        {
            Rigidbody2D shell = Instantiate(f.ShellType, shellPoint.position, shellPoint.rotation * Quaternion.Euler(0, 0, Random.Range(-30, 30))).GetComponent<Rigidbody2D>();
            shell.velocity = -shell.transform.up * (14 + Random.Range(-4, 12));
            shell.angularVelocity = Random.Range(0, 360);   
        }   
    }
    public void ResetAnimation()
    {
        Firearm firearm = inventory.PrimaryWeapon.WeaponBase as Firearm;
        if (firearm != null && inventory.PrimaryWeapon.Ammo < firearm.AmmoCapacity)
        {
            int id = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            animator.Play(id, 0, 0);
            inventory.PrimaryWeapon.Ammo++;
            inventory.TakeItem(inventory.PrimaryWeapon.Magazine, 1);
            return;
        }
        animator.SetBool("Reloading", false);
    }
    public void EjectMagazine()
    {
        inventory.TakeItem(inventory.PrimaryWeapon.Magazine, 1);
        // Throwing magazine
        GameObject magazine = Instantiate(PrefabsStatic.Magazine, shellPoint.position, shellPoint.rotation * Quaternion.Euler(0, 0, Random.Range(-30, 30)));
        Rigidbody2D rb = magazine.GetComponent<Rigidbody2D>();
        rb.velocity = magazine.transform.up * (14 + Random.Range(-4, 12));
        magazine.GetComponent<SpriteRenderer>().sprite = oldMagazine.ammo == 0 ? oldMagazine.magazine.Empty : oldMagazine.magazine.Sprite;
        rb.angularVelocity = Random.Range(0, 360);
        magazine.GetComponent<MagazineLand>().magazine = oldMagazine;
        animator.SetBool("Reloading", false);
    }
}
