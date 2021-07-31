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
    private void Start()
    {
        Bullet = _bullet;
        Weapon = _weapon;
        Magazine = _magazine;


        Destroy(gameObject);
    }
}
