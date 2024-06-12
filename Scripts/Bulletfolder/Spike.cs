using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public bool Roll = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rigid.AddForce(new Vector3(2, 4, 0), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(Roll == false)
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * 100));
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            Roll = true;
        }
    }
}
