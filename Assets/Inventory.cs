using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Weapon primary;
    [SerializeField] private Weapon secondary;
    private bool isPlayer;
    private Animator animator;

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
        isPlayer = gameObject.CompareTag("Player");
        animator = GetComponent<Animator>();
    }
}
[System.Serializable]
public class Weapon
{


    [SerializeField] private WeaponBase weaponItself;
    [SerializeField] private int ammo;

    public bool isNone()
    {
        return weaponItself == null;
    }

    public void Shoot(GameObject owner)
    {
        if (weaponItself is Firearm)
        {
            if (ammo > 0)
            {
                ammo--;
                Debug.Log("Shooting");
                weaponItself.Attack(owner);
                if (ammo == 0)
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

    public int Ammo => ammo;


    public Weapon(int ammo, WeaponBase weapon)
    {
        this.ammo = ammo;
        weaponItself = weapon;
    }
    public Weapon Copy()
    {
        return new Weapon(ammo, weaponItself);
    }
    public void Destroy()
    {
        weaponItself = null;
        ammo = 0;
    }
}