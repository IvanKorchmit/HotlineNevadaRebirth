using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticle : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if(other.TryGetComponent(out IDamagable damage))
        {
            damage.Damage(transform.parent.parent, 10, Damagable.DamageType.flame);
        }
    }
}
