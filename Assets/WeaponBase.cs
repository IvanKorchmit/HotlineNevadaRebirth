using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponBase : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private Sprite uiIcon;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int id = 1;
    public string Name => name;
    public Sprite UI => uiIcon;
    public Sprite Sprite => sprite;
    public virtual void Attack(GameObject owner, Magazine magazine)
    {
        Debug.Log("Pew pew");
    }
    public int ID => id;
}
public class Firearm : WeaponBase
{
    [SerializeField] protected float cone;
    [SerializeField] private int ammoCapacity;
    [SerializeField] private GameObject shell;
    [SerializeField] private Magazine[] allowedMagazines;
    public Magazine[] AllowedMagazines => allowedMagazines;
    public GameObject ShellType => shell;
    public int AmmoCapacity => ammoCapacity;
    public bool CheckMagazines(Magazine magazine)
    {
        foreach (Magazine m in allowedMagazines)
        {
            if(magazine == m)
            {
                return true;
            }
        }
        return false;
    }
}
public class Melee : WeaponBase
{

}

public class NormalWeapon : Firearm
{
    public override void Attack(GameObject owner, Magazine magazine)
    {
        var bullet = Instantiate(PrefabsStatic.Bullet, owner.transform.position, Quaternion.Euler(0, 0, owner.transform.Find("Visual").eulerAngles.z + Random.Range(-cone, cone))).GetComponent<Projectile>();
        bullet.owner = owner.transform;
    }
}


public class Magazine : ScriptableObject
{
    [SerializeField] private int stack;
    [SerializeField] private new string name;
    [SerializeField] private int damage;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite empty;
    public int Damage => damage;
    public string Name => name;
    public int Stack => stack;
    public Sprite Sprite => sprite;
    public Sprite Empty => empty;
}
