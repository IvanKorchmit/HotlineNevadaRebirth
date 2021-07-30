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
public struct Weapon
{
    public WeaponBase WeaponBase => weaponItself;
    public bool isNone()
    {
        return weaponItself == null;
    }
    [SerializeField] private WeaponBase weaponItself;
    [SerializeField] private int ammo;
    public void Shoot(GameObject owner)
    {
        if (ammo > 0)
        {
            weaponItself.Attack(owner);
            ammo--;
        }
    }
    public int Ammo => ammo;
}