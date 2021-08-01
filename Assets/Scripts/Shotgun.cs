using UnityEngine;

[CreateAssetMenu(fileName ="New Shotgun",menuName = "Weapons/Firearm/Shotgun")]
public class Shotgun : Firearm
{
    public override void Attack(GameObject owner, Magazine magazine)
    {
        int pellets = 0;    
        if(magazine is Buckshot buck)
        {
            pellets = buck.Pellets;
        }
        for (int i = 0; i < pellets; i++)
        {
            Projectile bullet = Instantiate(PrefabsStatic.Bullet, owner.transform.Find("Visual/ShellPoint").position, Quaternion.Euler(0, 0, owner.transform.Find("Visual").eulerAngles.z + Random.Range(-cone, cone))).GetComponent<Projectile>();
            bullet.owner = owner.transform;
            bullet.damage = magazine.Damage;
        }
    }
}
