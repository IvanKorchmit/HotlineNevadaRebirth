using UnityEngine;

[CreateAssetMenu(fileName ="New Shotgun",menuName = "Weapons/Firearm/Shotgun")]
public class Shotgun : Firearm
{
    [SerializeField] private int pellets;
    public override void Attack(GameObject owner)
    {
        for (int i = 0; i < pellets; i++)
        {
            var bullet = Instantiate(PrefabsStatic.Bullet, owner.transform.position, Quaternion.Euler(0, 0, owner.transform.Find("Visual").eulerAngles.z + Random.Range(-cone, cone))).GetComponent<Projectile>();
            bullet.owner = owner.transform;

        }
    }
}
