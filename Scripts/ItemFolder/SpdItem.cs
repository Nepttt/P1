using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpdItem : MonoBehaviour
{
    public bool isPickUp;

    public Player player;

    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (isPickUp && Input.GetKey(KeyCode.F))   //F키 눌러 아이템 획득
        {
            PickUp();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))  //Player와 겹치면 획득 가능한 상태
        {
            isPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))  //나가면 획득 불가능한 상태
        {
            isPickUp = false;
        }
    }


    void PickUp()
    {
        player.Speed *= 1.2f;   //이동속도 증가
        Destroy(gameObject);    //오브젝트 파괴
        isPickUp = false;   //아이템 획득 불가능 상태로 변경

    }
}
