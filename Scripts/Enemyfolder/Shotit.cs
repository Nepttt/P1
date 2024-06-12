using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotit : MonoBehaviour
{
    public float attackRate;
    private float timeAfterAttack;
    private bool Detacted;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterAttack += Time.deltaTime;
        if (Detacted == true)
        {
            if (timeAfterAttack >= attackRate)
            {
                timeAfterAttack = 0f;

                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            }
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
}
