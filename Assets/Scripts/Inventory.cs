using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Weapon primary;
    [SerializeField] private Weapon secondary;
    [SerializeField] private MagazineItem[] inventory;
    public MagazineItem FindMagazine(MagazineItem magazine)
    {
        if(magazine.magazine != null)
        {
            foreach (var item in inventory)
            {
                if(item.magazine == magazine.magazine)
                {
                    return item;
                }
            }
        }
        return null;
    }
    public MagazineItem FindAllowedMagazines(Firearm firearm)
    {
        foreach (var item in inventory)
        {
            if (firearm.CheckMagazines(item.magazine))
            {
                return item;
            }
        }
        return null;
    }
    public MagazineItem TakeItem(Magazine magazine, int quantity)
    {
        foreach (var item in inventory)
        {
            if (item != null && item.magazine == magazine)
            {
                item.Take(quantity);
                MagazineItem newItem = new MagazineItem(item.ammo, quantity, item.magazine);
                return newItem;
            }
        }
        return null;
    }
    public bool hasThisMagazine(Magazine magazine)
    {
        foreach (var mag in inventory)
        {
            if(mag != null && mag.magazine == magazine)
            {
                return true;
            }
        }
        return false;
    }
    public void AddItem(MagazineItem magazine)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].Equals(magazine))
            {
                inventory[i].quantity += magazine.quantity;
                return;
            }
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null || inventory[i].magazine == null)
            {
                inventory[i] = magazine.Copy();
                return;
            }
        }
    }
    public Weapon PrimaryWeapon
    {
        get { return primary; }
        set { primary = value; }
    }
    public Weapon SecondaryWeapon
    {
        get { return secondary; }
        set { secondary = value; }
    }
    private void Start()
    {
            
    }
}
[System.Serializable]
public class Weapon
{

    [SerializeField] private MagazineItem magazine;
    [SerializeField] private WeaponBase weaponItself;

    /// <summary>
    /// Checks if the weapon is null (Shortcut for weapon == null)
    /// </summary>
    /// <returns>
    /// True if the weapon is Null or else False.
    /// </returns>
    public bool isNone()
    {
        return weaponItself == null;
    }
    public void Shoot(GameObject owner)
    {
        if (weaponItself is Firearm)
        {
            if (magazine.ammo > 0)
            {
                magazine.ammo--;
                weaponItself.Attack(owner, magazine.magazine);
                if (magazine.ammo == 0)
                {
                    if (owner.TryGetComponent(out Animator animator))
                    {
                        animator.SetBool("Attack", false);
                    }
                    else
                    {
                        owner.GetComponentInChildren<Animator>().SetBool("Attack", false);
                    }
                }
            }
            else
            {
                if (owner.TryGetComponent(out Animator animator))
                {
                    animator.SetBool("Attack", false);
                }
                else
                {
                    owner.GetComponentInChildren<Animator>().SetBool("Attack", false);
                }
            }
        }
    }

    public WeaponBase WeaponBase => weaponItself;

    public int Ammo
    {
        get => magazine.ammo;
        set => magazine.ammo = value;
    }
    public Magazine Magazine 
    { 
        get => magazine.magazine;
        set => magazine.magazine = value;
    }
    public MagazineItem MagazineBase
    {
        get => magazine;
        set => magazine = value;
    }

    public Weapon(MagazineItem magazine, WeaponBase weapon)
    {
        this.magazine = magazine;
        weaponItself = weapon;
    }
    public Weapon Copy()
    {
        return new Weapon(magazine, weaponItself);
    }
    public void Destroy()
    {
        weaponItself = null;
        magazine = MagazineItem.Empty;
    }
}
