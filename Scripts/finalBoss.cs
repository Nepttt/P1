using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBoss : MonoBehaviour
{
    Animator animator;

    public GameObject firePrefab;

    private Transform form;

    SpriteRenderer spriteRenderer;

    public bool Die = false;

    float Ding;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator.SetBool("Fire", true);
        Invoke("fire", 1f);

        Ding = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Heart") == null)
        {
            Dead();
        }
    }

    void ready()
    {
        animator.SetBool("Fire", true);
        Invoke("fire", 1f);
    }

    private void fire()
    {
        if(Die == false)
        {
            GameObject bullet = Instantiate(firePrefab, transform.position, transform.rotation);
            Invoke("close", 4);
        }
    }

    private void close()
    {
        animator.SetBool("Fire", false);

        Invoke("ready", 2f);
    }

    private void Dead()
    {
        Die = true;

        Ding -= Time.deltaTime;

        spriteRenderer.color = new Color(1, 1, 1, Ding);

        if(Ding == 0)
        {
            Destroy(gameObject);
        }
    }
}
