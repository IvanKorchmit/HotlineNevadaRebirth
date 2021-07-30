using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 m;
    private Vector2 movement;
    private Animator animator;
    private Rigidbody2D rb;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        m.x = Input.GetAxisRaw("Horizontal");
        m.y = Input.GetAxisRaw("Vertical");
        m.Normalize();
        movement = m * speed * Time.fixedDeltaTime;
    }
    private void FixedUpdate()
    {
        if(m.magnitude == 0 && animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
        {
            animator.speed = 0;
        }
        else
        {
            animator.speed = 1;
        }
        rb.MovePosition(rb.position + movement);
    }

}
