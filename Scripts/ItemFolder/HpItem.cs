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
        if (player.Hp < player.MaxHp - 30) //HP�� �ִ�ü�� - 30 ���� ������ HP+30
        {
            player.Hp += 30;
            Destroy(gameObject);
            isPickUp = false;
        }
        else    //HP�� �ִ�ü�� - 30 ���� ũ�ų� ������ �ִ�ü������(ü�� �ʰ� ����)
        {
            player.Hp = player.MaxHp;
            Destroy(gameObject);
            isPickUp = false;
        }
       
    }
}
