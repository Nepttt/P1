using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    public Rigidbody2D rb;
    float timer = 0;
    float delay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        Fall();
    }
    void Fall()
    {
        if(timer > delay)
        {
            rb.gravityScale = 5;
            Destroy(this.gameObject, 2f);
        }
        timer += Time.deltaTime;
    }
}
