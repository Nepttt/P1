using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigbullet : MonoBehaviour
{
    private Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Transform playerPos;
    Vector2 dir;
    public float speed;
    public float rt;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        dir = playerPos.position - transform.position;
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 6f);
    }


    void Update()
    {
        rb.velocity = dir.normalized * speed;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            dir = playerPos.position - transform.position;
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
