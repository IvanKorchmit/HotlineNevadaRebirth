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
public class Melee : WeaponBase
{

}


public class Magazine : ScriptableObject
{
    [SerializeField] private int stack;
    [SerializeField] private new string name;
    [SerializeField] private int damage;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite empty;
    [SerializeField] private int ammoCapacity;
    public int AmmoCapacity => ammoCapacity;
    public int Damage => damage;
    public string Name => name;
    public int Stack => stack;
    public Sprite Sprite => sprite;
    public Sprite Empty => empty;
}
