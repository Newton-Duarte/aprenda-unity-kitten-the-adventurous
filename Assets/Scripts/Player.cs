using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] LayerMask whatIsGround;

    float speedX;
    float speedY;
    bool isGrounded;
    bool isLookLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = rb.velocity.y;

        if (isLookLeft && speedX > 0) { flip(); }
        if (!isLookLeft && speedX < 0) { flip(); }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            attack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            shot();
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position, whatIsGround);
        rb.velocity = new Vector2(speedX * moveSpeed, speedY);
    }

    void LateUpdate()
    {
        updateAnimator();
    }

    void updateAnimator()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetInteger("speedX", (int)speedX);
        anim.SetFloat("speedY", speedY);
    }

    void jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
    }

    void flip()
    {
        isLookLeft = !isLookLeft;

        float scaleX = transform.localScale.x;
        scaleX *= -1;

        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    void attack()
    {
        anim.SetTrigger("Attack");
    }

    void shot()
    {
        anim.SetTrigger("Shot");
    }
}
