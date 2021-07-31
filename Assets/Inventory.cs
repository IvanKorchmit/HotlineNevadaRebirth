using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Weapon primary;
    [SerializeField] private Weapon secondary;
    [SerializeField] private MagazineItem[] inventory;

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
                Debug.Log("Shooting");
                weaponItself.Attack(owner, magazine.magazine);
                if (magazine.ammo == 0)
                {
                    owner.GetComponent<Animator>().SetBool("Attack", false);
                }
            }
            else
            {
                owner.GetComponent<Animator>().SetBool("Attack", false);
            }
        }
    }

    public WeaponBase WeaponBase => weaponItself;

    public int Ammo
    {
        get => magazine.ammo;
        set => magazine.ammo = value;
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

[System.Serializable]
public class MagazineItem
{
    public Magazine magazine;
    public int ammo;
    public int quantity;
    public MagazineItem Copy()
    {
        return new MagazineItem(ammo,quantity,magazine);
    }
    public MagazineItem(int ammo, int quantity, Magazine magazine)
    {
        this.magazine = magazine;
        this.ammo = ammo;
        this.quantity = quantity;
    }
    public bool Equals(MagazineItem magazine)
    {
        return this.magazine == magazine.magazine && ammo == magazine.ammo;
    }
    public static MagazineItem Empty => new MagazineItem(0, 0, null);
}