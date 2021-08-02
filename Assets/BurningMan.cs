using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningMan : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float angleSpeed;
    [SerializeField] private float dir = 1.0f;
    private IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        Die();

    }
    private void FixedUpdate()
    {
        rb.SetRotation(rb.rotation + angleSpeed * dir);
        transform.rotation = Quaternion.Euler(0, 0, rb.rotation);
        rb.velocity = transform.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        dir *= -1;
    }
    private void Die()
    {
        Animator corpse = Instantiate(PrefabsStatic.Corpse, transform.position, transform.rotation).GetComponent<Animator>();
        corpse.SetInteger("flame", 1);
        Destroy(gameObject);
    }
}
