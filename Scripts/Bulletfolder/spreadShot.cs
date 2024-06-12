using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spreadShot : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("shot", 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void shot()
    {
        if (GameObject.FindGameObjectWithTag("Trigger") != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        }

        Invoke("shot", 0.05f);
    }
}
