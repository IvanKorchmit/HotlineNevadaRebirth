using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is for accessing prefabs anywhere. DO NOT CHANGE THE STATIC FIELDS DURING RUNTIME!!!!!!!!
/// </summary>
public class PrefabsStatic : MonoBehaviour
{
    public GameObject _bullet;
    public static GameObject Bullet;
    public GameObject _weapon;
    public static GameObject Weapon;
    public GameObject _magazine;
    public static GameObject Magazine;
    public GameObject _burningMan;
    public static GameObject BurningMan;
    public GameObject _corpse;
    public static GameObject Corpse;
    public GameObject _bloodParticle;
    public static GameObject BloodParticle;
    public GameObject _bloodFloor;
    public static GameObject BloodFloor;
    private void Start()
    {
        Bullet = _bullet;
        Weapon = _weapon;
        Magazine = _magazine;
        BurningMan = _burningMan;
        Corpse = _corpse;
        BloodParticle = _bloodParticle;
        BloodFloor = _bloodFloor;

        Destroy(gameObject);
    }
}
