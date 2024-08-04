using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class HpItem : MonoBehaviour
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
        if(collision.gameObject.tag.Equals("Player"))
        {
            isPickUp = false;
        }
    }


    void PickUp()
    {
        if (player.Hp < player.MaxHp - 30) //HP가 최대체력 - 30 보다 작으면 HP+30
        {
            player.Hp += 30;
            Destroy(gameObject);
            isPickUp = false;
        }
        else    //HP가 최대체력 - 30 보다 크거나 같으면 최대체력으로(체력 초과 방지)
        {
            player.Hp = player.MaxHp;
            Destroy(gameObject);
            isPickUp = false;
        }
       
    }
}
