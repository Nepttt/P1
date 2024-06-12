using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemymove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    public int HP;
    public int speed;

    SpriteRenderer sprite;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Invoke("Think", 1);
    }
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove * speed,rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y );
        Debug.DrawRay(frontVec, Vector3.down, Color.red);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if(rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 1);
        }

        if(HP <= 0)
        {
            Destroy(gameObject);
        }

        if(nextMove == 1)
        {
            sprite.flipX = false;
        }
        else if(nextMove == -1) {
            sprite.flipX = true;
        }
    } 

    void Think()
    {
        nextMove = Random.Range(-1,2);

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think",nextThinkTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            HP -= 2;
        }
    }
}
