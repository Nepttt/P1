using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.UIElements;


public class Boss : MonoBehaviour
{
    public int nextPatten;
    public int Max;

    Rigidbody2D rd;
    private Transform player;
    public GameObject bulletPrefab;
    public GameObject smashPrefab;

    public GameObject p2bossPrefab;
    public float attackRate;
    private float timeAfterAttack;
    public int HP;

    float Ding;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        timeAfterAttack = 0f;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        rd = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Ding = 1;

        Think();
    }


    void Update()
    {
        timeAfterAttack += Time.deltaTime;

        if (nextPatten == 0)
        {
            P1();
        }
        else if(nextPatten == 1) 
        {
            Smash();
        }
        else
        {
            
        }

        if(HP <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Ding -= Time.deltaTime;

        spriteRenderer.color = new Color(1, 1, 1, Ding);

        if (Ding <= 0)
        {
            Destroy(gameObject);
        }
    }

    void P1()
    {
        if (timeAfterAttack >= attackRate)
        {
            Debug.Log("패턴1");

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            timeAfterAttack = 0f;
            Max += 1;
        }
        if (Max >= 5)
        {
            nextPatten = 4;
            Max = 0;
        }
    }

    public void Smash()
    {
        Debug.Log("패턴2");

        GameObject bullet = Instantiate(smashPrefab, transform.position, transform.rotation);
        nextPatten = 4;
    }

    void Think()
    {
        nextPatten = Random.Range(0, 2);
        Invoke("Think", 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            HP -= 2;
        }
    }
}
