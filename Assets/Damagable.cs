using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour, IDamagable
{
    public void Damage(Transform killer)
    {
        throw new System.NotImplementedException();
    }

    public void Die(Transform killer)
    {
        throw new System.NotImplementedException();
    }

    public void Stun(Transform fromWho)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public interface IDamagable
{

    void Damage(Transform killer);
    void Die(Transform killer);
    void Stun(Transform fromWho);
}