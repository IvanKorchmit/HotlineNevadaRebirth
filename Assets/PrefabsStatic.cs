using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsStatic : MonoBehaviour
{
    public GameObject _bullet;
    public static GameObject Bullet;
    public GameObject _weapon;
    public static GameObject Weapon;
    private void Start()
    {
        Bullet = _bullet;
        Weapon = _weapon;



        Destroy(gameObject);
    }
}
