using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerTraceSky : MonoBehaviour
{
    public float speed = 2f;
    public int HP = 4;
    Rigidbody2D rigid;
    private Transform player;
    public bool Detacted = false;

    void Start()
    {
        // 태그로 플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float dis = Vector3.Distance(rigid.position, player.position);
        Vector2 direction = player.position - transform.position;
        direction.Normalize();

        if (Detacted == true)
           {
                rigid.velocity = direction * speed;
           }
        else
          {
                rigid.velocity = Vector2.zero;
           }

        if(HP <=  0)
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
        if(collision.gameObject.tag == "PlayerBullet")
        {
            HP -= 2;
        }
    }
}
