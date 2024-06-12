using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spread : MonoBehaviour
{
    public float speed;

    private float circle;

    public Transform firePrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(firePrefab.position, Vector3.forward, speed * Time.deltaTime);
    }
}
