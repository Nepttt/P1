using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHeart : MonoBehaviour
{
    public int HP;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            HP -= 2;
        }
    }
}
