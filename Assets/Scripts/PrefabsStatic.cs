using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        Bullet = _bullet;
        Weapon = _weapon;
        Magazine = _magazine;
        BurningMan = _burningMan;
        Corpse = _corpse;
        Destroy(gameObject);
    }
}
