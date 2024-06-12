using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;

    public int HP = 10;

    public float speed = 1.0f;
    public float maxSpeed = 6.0f;
    public float jumpPower = 10.0f;
    public float coolTime;
    private float realtime = 0f;


    public bool Damage = false;
    public bool OnGround = true;
    public bool cDamage = false;
    Animator animator;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        realtime += Time.deltaTime;

        if(Damage == false)
        {
            if (Input.GetKey(KeyCode.D) && cDamage == false)
            {
                rigid.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
                spriteRenderer.flipX = true;
                animator.SetBool("Move", true);
            }
            else if (Input.GetKey(KeyCode.A) && cDamage == false)
            {
                rigid.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
                spriteRenderer.flipX = false;
                animator.SetBool("Move", true);
            }
            else
            {
                animator.SetBool("Move", false);
            }

            if (Input.GetKeyDown(KeyCode.L) && coolTime <= realtime)
            {
                realtime = 0f;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            }

            if (rigid.velocity.x > maxSpeed)
            {
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            }
            else if(rigid.velocity.x < maxSpeed * (-1))
            {
                rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
            }
            if (Input.GetKeyDown(KeyCode.W) && cDamage == false)
            {
                Jump();         
            }
        }

        if(HP <= 0)
        {
            Debug.Log("You Died");
            Destroy(gameObject);
        }
    }

    void Jump()
    {
        if(OnGround == true)
        {
            OnGround = false;
            Debug.Log("มกวม!");
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bullet")
        {
            OnDamaged(collision.transform.position);
            cDamage = true;
        }

        if (collision.gameObject.tag == "Platform")
        {
            Damage = false;
            OnGround = true;
            cDamage = false;
        }
    }

    void OnDamaged(Vector2 tartgetPos)
    {
        Damage = true;

        HP -= 2;

        Debug.Log(HP);

        gameObject.layer = 8;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x-tartgetPos.x > 0 ? 1 : -1;

        rigid.AddForce(new Vector2(dirc, 2) * 2, ForceMode2D.Impulse);

        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 7;

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
