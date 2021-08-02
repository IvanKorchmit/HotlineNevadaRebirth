using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour, IDamagable
{
    public int bulletAmount;
    public int bulletDamage;
    public int sharpDamage;
    public int bluntDamage;
    public int explosionDamage;
    public int flameDamage;
    public Transform lastKiller;
    public enum DamageType
    {
        blunt, sharp, bullet, explosion, flame
    }
    private bool isTimer;
    private float maxTime;
    private float time;
    public void Damage(Transform killer, int damage, DamageType damageType)
    {
        isTimer = true;
        lastKiller = killer;
        switch (damageType)
        {
            case DamageType.blunt:
                bluntDamage += damage;
                break;
            case DamageType.sharp:
                sharpDamage += damage;
                break;
            case DamageType.bullet:
                bulletAmount++;
                bulletDamage += damage;
                break;
            case DamageType.explosion:
                explosionDamage += damage;
                break;
            case DamageType.flame:
                flameDamage += damage;
                break;
            default:
                break;
        }
    }
    private void Die(Transform killer)
    {
        Vector3 dir = killer.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion ang_q = Quaternion.AngleAxis(angle, Vector3.forward);
        if (flameDamage > 0)
        {
            Instantiate(PrefabsStatic.BurningMan,transform.position,Quaternion.Euler(0,0,Random.Range(0,360)));
            Destroy(gameObject);
        }
        else if (bulletDamage > 10)
        {
            var Corpse = Instantiate(PrefabsStatic.Corpse, transform.position, ang_q).GetComponent<Animator>();
            Corpse.SetInteger("Random", Random.Range(0, 3));
            Corpse.SetInteger("bulletDamage", bulletDamage);
            Corpse.SetInteger("bulletAmount", bulletAmount);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(isTimer)
        {
            time += Time.deltaTime;
            if (time >= maxTime)
            {
                Die(lastKiller);
            }
        }
    }
}
public interface IDamagable
{
    void Damage(Transform killer, int damage, Damagable.DamageType damageType);
}