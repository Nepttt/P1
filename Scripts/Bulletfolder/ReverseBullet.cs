using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseBullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;

    private Transform player;
    private SpriteRenderer sprite;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4f);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = player.GetComponent<SpriteRenderer>();

        if (sprite.flipX == true)
        {
            rb.AddForce(new Vector3(speed * Time.deltaTime, 0, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector3(speed * -1 * Time.deltaTime, 0, 0), ForceMode2D.Impulse);
        }
    }
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
        }
    }
}
