using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spreadbullet : MonoBehaviour
{
    Rigidbody2D rb;

    Transform spre;
    Vector2 dir;
    public float speed;
    void Start()
    {
        spre = GameObject.Find("Rolling").GetComponent<Transform>();
        dir = spre.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(dir.normalized * speed);

        if (GameObject.Find("Heart") == null)
        {
            Destroy(gameObject);
        }

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
