using UnityEngine;

[CreateAssetMenu(fileName = "New Firearm", menuName = "Weapons/Firearm/Generic")]
public class Firearm : WeaponBase
{
    [SerializeField] protected bool requiresMagazine;
    [SerializeField] protected float cone;
    [SerializeField] private GameObject shell;
    [SerializeField] private Magazine[] allowedMagazines;
    [SerializeField] private int ammoCapacity;

    public int AmmoCapacity => ammoCapacity;
    public Magazine[] AllowedMagazines => allowedMagazines;
    public GameObject ShellType => shell;
    public bool RequiresMagazine => requiresMagazine;
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
    public override void Attack(GameObject owner, Magazine magazine)
    {
        var bullet = Instantiate(PrefabsStatic.Bullet, owner.transform.position, Quaternion.Euler(0, 0, owner.transform.Find("Visual").eulerAngles.z + Random.Range(-cone, cone))).GetComponent<Projectile>();
        bullet.owner = owner.transform;
    }
}