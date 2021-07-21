using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    GameController _gameController;

    [SerializeField] int hitPoints;
    [SerializeField] AudioClip hitClip;
    bool isHit;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "HammerHit":
                if (!isHit)
                {
                    isHit = true;
                    StartCoroutine(waitHit());
                    GameObject temp = Instantiate(_gameController.hitPrefab, transform.position, transform.localRotation);
                    Destroy(temp, 0.5f);
                    takeHit(_gameController.hammerDamage);
                }
                break;
            case "BallHit":
                Destroy(collision.gameObject, 0.1f);

                if (!isHit)
                {
                    isHit = true;
                    StartCoroutine(waitHit());
                    GameObject temp = Instantiate(_gameController.hitPrefab, transform.position, transform.localRotation);
                    Destroy(temp, 0.5f);
                    takeHit(_gameController.ballDamage);
                }
                break;
            case "HitBox":
                if (!isHit)
                {
                    _gameController.playFX(hitClip);
                    isHit = true;
                    StartCoroutine(waitHit());
                    GameObject temp = Instantiate(_gameController.hitPrefab, transform.position, transform.localRotation);
                    Destroy(temp, 0.5f);

                    Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(new Vector2(0, 250));
                    takeHit(_gameController.hitBoxDamage);
                }
                break;
        }
    }

    void takeHit(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }

    IEnumerator waitHit()
    {
        yield return new WaitForSeconds(0.25f);
        isHit = false;
    }
}
