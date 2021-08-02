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
        if(flameDamage > 0)
        {
            Instantiate(PrefabsStatic.BurningMan,transform.position,Quaternion.Euler(0,0,Random.Range(0,360)));
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