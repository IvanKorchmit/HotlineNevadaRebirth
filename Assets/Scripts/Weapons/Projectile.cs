using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float Speed = 45;
    private Vector2 startPosition;
    private Transform trail;
    public Transform owner;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * (Speed+Random.Range(-5,5));
        trail = transform.Find("Trail");
        startPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            trail.SetParent(null);
            Destroy(gameObject);
        }
        if(collision.TryGetComponent(out IDamagable damage))
        {
            damage.Damage(owner);
            trail.SetParent(null);
            Destroy(gameObject);
            
        }
    }
    private void FixedUpdate()
    {
        if(Vector2.Distance(startPosition,rb.position) > 500)
        {
            trail.SetParent(null);
            Destroy(gameObject);
        }
    }

}
