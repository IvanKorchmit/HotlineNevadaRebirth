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
