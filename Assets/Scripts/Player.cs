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
    [SerializeField] float delayAttack = 0.3f;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform spawnBall;
    [SerializeField] float ballSpeed;

    [Header("Colliders Config.")]
    [SerializeField] BoxCollider2D hammerCol;
    [SerializeField] CapsuleCollider2D defaultCol;
    [SerializeField] CapsuleCollider2D flyCol;

    float speedX;
    float speedY;
    bool isGrounded;
    bool isIdle;
    bool isAttacking;
    bool isLookLeft;

    // Start is called before the first frame update
    void Start()
    {
        defaultCol.enabled = true;
        flyCol.enabled = false;
        hammerCol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = rb.velocity.y;

        if (speedX == 0 && isGrounded && !isIdle)
        {
            isIdle = true;
            StartCoroutine(idle2());
        }
        else if (speedX != 0 || !isGrounded)
        {
            stopIdleAnimation();
        }

        if (isLookLeft && speedX > 0) { flip(); }
        if (!isLookLeft && speedX < 0) { flip(); }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump();
            stopIdleAnimation();
        }

        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            attack();
            stopIdleAnimation();
        }

        if (Input.GetButtonDown("Fire2") && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Shot");
            stopIdleAnimation();
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
        updateColliders();
    }

    void updateAnimator()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetInteger("speedX", (int)speedX);
        anim.SetFloat("speedY", speedY);
    }

    void updateColliders()
    {
        if (isFlying && !flyCol.enabled)
        {
            defaultCol.enabled = false;
            flyCol.enabled = true;
        }
        else if (!isFlying && !defaultCol.enabled)
        {
            defaultCol.enabled = true;
            flyCol.enabled = false;
        }
    }

    internal void OnAttackComplete()
    {
        hammerCol.enabled = false;
        StartCoroutine(activateAttack());
    }

    IEnumerator activateAttack()
    {
        yield return new WaitForSeconds(delayAttack);
        isAttacking = false;
    }

    void stopIdleAnimation()
    {
        isIdle = false;
        StopCoroutine(idle2());
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
        ballSpeed *= -1;

        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    void attack()
    {
        anim.SetTrigger("Attack");
    }

    internal void shot()
    {
        GameObject ball = Instantiate(ballPrefab, spawnBall.position, transform.localRotation);
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(ballSpeed, 0);
    }

    IEnumerator idle2()
    {
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("Idle");
    }
}
