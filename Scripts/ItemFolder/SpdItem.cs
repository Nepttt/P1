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
        if (isPickUp && Input.GetKey(KeyCode.F))   //FŰ ���� ������ ȹ��
        {
            PickUp();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))  //Player�� ��ġ�� ȹ�� ������ ����
        {
            isPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))  //������ ȹ�� �Ұ����� ����
        {
            isPickUp = false;
        }
    }


    void PickUp()
    {
        player.Speed *= 1.2f;   //�̵��ӵ� ����
        Destroy(gameObject);    //������Ʈ �ı�
        isPickUp = false;   //������ ȹ�� �Ұ��� ���·� ����

    }
}
