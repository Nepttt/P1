using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject Tri;

    public int nextPatten;
    public int Max;

    public float attackRate;
    private float timeAfterAttack;

    void Start()
    {

        Destroy(gameObject, 3.5f);
        nextPatten = Random.Range(0, 2);

        transform.position = new Vector3(transform.position.x , transform.position.y, 0);


        
        if (nextPatten == 1)
        {
            Invoke("P2", 0);
        }
    }

    void Update()
    {
        timeAfterAttack += Time.deltaTime;

        if (nextPatten == 0)
        {
            P1();
        }

        if (GameObject.Find("Heart") == null)
        {
            Destroy(gameObject);
        }
    }

    private void P1()
    {
        if (timeAfterAttack >= attackRate)
        {
            Debug.Log("ÆÐÅÏ1");

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            timeAfterAttack = 0f;
            Max += 1;
        }
        if (Max >= 15)
        {
            nextPatten = 4;
            Max = 0;
        }
    }

    private void P2()
    {
        GameObject Trigger = Instantiate(Tri, transform.position, transform.rotation);

        Destroy(Trigger, 3f);
    }
}
