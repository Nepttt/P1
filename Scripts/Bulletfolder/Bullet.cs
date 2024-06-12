using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    Transform playerPos;
    Vector2 dir;
    public float speed;

    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>(); 
        dir = playerPos.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(dir.normalized * speed);

        Destroy(gameObject, 3f);
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}
