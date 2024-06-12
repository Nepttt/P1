using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootingnenmy : MonoBehaviour
{
    Rigidbody2D rigid;
    private Transform player;
    public GameObject bulletPrefab;
    public float attackRate;
    private float timeAfterAttack;
    private bool Detacted;
    SpriteRenderer sprite;

    float dir;

    public int HP;

    void Start()
    {
        timeAfterAttack = 0f;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();

        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        timeAfterAttack += Time.deltaTime;
        dir = player.position.x - transform.position.x;
        if (Detacted == true)
        {
            if(dir >= 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }

            if (timeAfterAttack >= attackRate)
            {
                timeAfterAttack = 0f;

                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            }
        }

        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Detacted = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Detacted = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            HP -= 2;
        }
    }
}
