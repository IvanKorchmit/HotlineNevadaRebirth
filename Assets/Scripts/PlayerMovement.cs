using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 m;
    private Vector2 movement;
    private Animator animator;
    private Animator legs_animator;
    private Rigidbody2D rb;
    private Vector2 direction_smooth;
    private Transform legs;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        legs = transform.Find("Legs");
        legs_animator = legs.GetComponent<Animator>();
    }
    private void Update()
    {
        m.x = Input.GetAxisRaw("Horizontal");
        m.y = Input.GetAxisRaw("Vertical");
        m.Normalize();
        movement = m * speed * Time.fixedDeltaTime;
        direction_smooth.x = Input.GetAxis("Horizontal") * Time.deltaTime;
        direction_smooth.y = Input.GetAxis("Vertical") * Time.deltaTime;
        if (m.magnitude > 0)
        {
            var angle = Mathf.Atan2(direction_smooth.y, direction_smooth.x) * Mathf.Rad2Deg;
            legs.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
    private void FixedUpdate()
    {
        
        if (m.magnitude == 0 && animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
        {
            animator.speed = 0;
            legs_animator.speed = 0;
        }
        else
        {
            animator.speed = 1;
        }
        if(m.magnitude > 0)
        {
            legs_animator.speed = 1;
        }
        rb.MovePosition(rb.position + movement);
    }

}
