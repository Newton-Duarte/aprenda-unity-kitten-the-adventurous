using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameController _gameController;

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
    [SerializeField] CapsuleCollider2D snorkCol;

    [Header("Fly Config.")]
    float gravityBase;

    [Header("Snork Config.")]
    [SerializeField] float gravityInWater;
    [SerializeField] float swimImpulse;

    float speedX;
    float speedY;
    bool isGrounded;
    bool isFlying;
    bool isFlyStarted;
    bool isSnorkeling;
    bool isWater;
    bool isIdle;
    bool isAttacking;
    bool isLookLeft;

    // Shop
    bool isHammer;
    bool isBall;
    bool isCloak;
    bool isSnork;

    // Exit
    bool isExit;

    // Start is called before the first frame update
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        defaultCol.enabled = true;
        flyCol.enabled = false;
        snorkCol.enabled = false;
        hammerCol.enabled = false;
        gravityBase = rb.gravityScale;
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

        if (isGrounded && isFlying)
        {
            stopFly();
        }

        if (!isWater)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                jump();
                stopIdleAnimation();
            }

            if (Input.GetButtonDown("Jump") && !isGrounded && !isFlying && isCloak)
            {
                if (!isFlyStarted)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0.9f);
                    isFlyStarted = true;
                }
                isFlying = true;
                rb.gravityScale = 0.1f;
            }

            if (Input.GetButtonUp("Jump"))
            {
                stopFly();
            }

            if (Input.GetButtonDown("Fire1") && !isAttacking && isHammer)
            {
                isAttacking = true;
                attack();
                stopIdleAnimation();
                stopFly();
            }

            if (Input.GetButtonDown("Fire2") && !isAttacking && isBall)
            {
                isAttacking = true;
                anim.SetTrigger("Shot");
                stopIdleAnimation();
                stopFly();
            }
        }
        else if (isSnorkeling)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(new Vector2(0, swimImpulse));
            }
        }

        if (isExit)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _gameController._fadeController.startFade(3);
            }
        }

        updateAnimator();
        updateColliders();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position, whatIsGround);
        if (isGrounded) { isFlyStarted = false; }
        rb.velocity = new Vector2(speedX * moveSpeed, speedY);
    }

    void updateAnimator()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFlying", isFlying);
        anim.SetBool("isSnorkeling", isSnorkeling);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("speedX", (int)speedX);
        anim.SetFloat("speedY", speedY);
    }

    void updateColliders()
    {
        if (isSnorkeling && !snorkCol.enabled)
        {
            snorkCol.enabled = true;
            defaultCol.enabled = false;
            flyCol.enabled = false;
        }
        else if (isFlying && !flyCol.enabled)
        {
            flyCol.enabled = true;
            snorkCol.enabled = false;
            defaultCol.enabled = false;
        }
        else if (!isFlying && !isSnorkeling && !defaultCol.enabled)
        {
            defaultCol.enabled = true;
            snorkCol.enabled = false;
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

    void stopFly()
    {
        isFlying = false;
        rb.gravityScale = gravityBase;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Water":
                print("Entrou na agua");
                isWater = true;
                isFlying = false;
                break;
            case "Submerged":
                if (!isSnorkeling)
                {
                    print("Submerso!");
                    isSnorkeling = true;
                    rb.velocity = new Vector2(0, 0);
                    rb.gravityScale = gravityInWater;
                }
                break;
            case "ShopItem":
                collision.SendMessage("openShop", SendMessageOptions.DontRequireReceiver);
                break;
            case "Collectable":
                collision.SendMessage("collect", SendMessageOptions.DontRequireReceiver);
                break;
            case "Exit":
                isExit = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Water":
                print("Saiu da agua");
                isWater = false;
                isSnorkeling = false;
                rb.gravityScale = gravityBase;
                break;
            case "Exit":
                isExit = false;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "MovablePlatform":
                if (groundCheckRight.position.y > collision.transform.position.y)
                {
                    rb.interpolation = RigidbodyInterpolation2D.None;
                    transform.parent = collision.transform;
                }
                break;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "MovablePlatform":
                rb.interpolation = RigidbodyInterpolation2D.Interpolate;
                transform.parent = null;
                break;
        }
    }

    internal void updateItems(int idItem)
    {
        switch(idItem)
        {
            case 0: // Hammer
                isHammer = true;
                break;
            case 1: // Ball
                isBall = true;
                break;
            case 2: // Cloak
                isCloak = true;
                break;
            case 3: // Snork
                isSnork = true;
                break;
        }
    }
}
