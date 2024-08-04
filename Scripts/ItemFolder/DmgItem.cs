using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgItem : MonoBehaviour
{
    public bool isPickUp;

    public Player player;

    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (isPickUp && Input.GetKey(KeyCode.F))
        {
            PickUp();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isPickUp = false;
        }
    }


    void PickUp()
    {  
        player.AtkDmg *= 1.2f;  //공격력 증가
        Destroy(gameObject);
        isPickUp = false;
        
    }
}
