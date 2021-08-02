using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticle : MonoBehaviour
{
    public Transform owner;
    private void Start()
    {
        owner = transform.root;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (owner != other.transform && other.transform != transform)
        {
            if (other.TryGetComponent(out IDamagable damage))
            {

                Debug.Log(transform.root);
                damage.Damage(owner, 10, Damagable.DamageType.flame);
            }
        }
    }
}
