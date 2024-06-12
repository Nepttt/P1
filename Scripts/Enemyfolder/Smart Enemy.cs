using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : MonoBehaviour
{
    Rigidbody2D rigid;
    private Transform player;
    public float speed = 2.0f;

    private float dis;
    private Vector2 direction;

    public int HP;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dis = Vector3.Distance(rigid.position, player.position);
        direction = player.position - transform.position;
        direction.Normalize();
        rigid.velocity = new Vector2(direction.x * speed, rigid.velocity.y);

        if(HP <= 0 )
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            HP -= 2;

            rigid.AddForce(new Vector2(direction.x * -1 * speed * 2, 2));
        }
    }
}
