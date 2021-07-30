using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponBase : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private Sprite uiIcon;
    [SerializeField] private Sprite sprite;
    public string Name => name;
    public Sprite UI => uiIcon;
    public Sprite Sprite => sprite;
    public virtual void Shoot(GameObject owner)
    {
        Debug.Log("Pew pew");
    }
}


public class Shotgun : Firearm
{

}
public class Firearm : WeaponBase
{
    [SerializeField] private int ammoCapacity;
    public int AmmoCapacity => ammoCapacity;
}
public class Melee : WeaponBase
{

}