using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsStatic : MonoBehaviour
{
    public GameObject _bullet;
    public static GameObject Bullet;
    private void Start()
    {
        Bullet = _bullet;




        Destroy(gameObject);
    }
}
