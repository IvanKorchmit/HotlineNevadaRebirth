using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Inventory inventory;
    private void Start()
    {
        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
    }
    private void Update()
    {
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
        }
    }
    public void Attack(bool isAlt)
    {
        if(!isAlt)
        {
            inventory.PrimaryWeapon.Shoot(gameObject);
        }
        else
        {
            inventory.SecondaryWeapon.Shoot(gameObject);
        }
    }
}
