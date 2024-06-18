using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    //�÷��̾� ���� ����
    public static bool watchr;
    public static bool watchl;

    //�ٰŸ� ���� ��Ʈ�ڽ�
    public GameObject hitbox;

    //���Ÿ�
    public GameObject bullet;

    //��ų 1
    public static float cooltime1 = 5f; //��Ÿ��
    public static float c1timer = 0;
    public GameObject skill1; //��ų 1 ������Ʈ(prefab)
    public static bool sk1using;//��ų 1 ��밡�ɿ���
    //��ų 1 ui
    public Button btn1; //(��ư Ȥ�� ĵ����)
    public TMP_Text t1; //��ų ��Ÿ�� ǥ�� �ؽ�Ʈ

    //��ų 2
    public static float cooltime2 = 5f; //��Ÿ��
    public static float c2timer = 0;
    public GameObject skill2; //��ų 2 ������Ʈ(prefab)
    public static bool sk2using; //��ų 2 ��밡�ɿ���
    //��ų 2 ui
    public Button btn2;
    public TMP_Text t2;
    float delay = 0.5f;
    float timer = 0f;

    Animator anim;

    void Start()
    {
        btn1 = GameObject.Find("skill1button").GetComponent<Button>();
        t1 = GameObject.Find("skill1text").GetComponent<TMP_Text>();

        btn2 = GameObject.Find("skill2button").GetComponent<Button>();
        t2 = GameObject.Find("skill2text").GetComponent<TMP_Text>();

        anim = GetComponent<Animator>();

        hitbox = GameObject.Find("Hitbox").GetComponent<GameObject>();;
    }
    void Update()
    {
        Attack();

        Fire();

        Skill1();
        Skill2();

        Skill_ui1();
        Skill_ui2();
    }
    void Attack()
    {
        //���� ����
        if (Input.GetKey(KeyCode.S))//Ű �ٲٱ�
        {
            GetComponent<Animator>().SetBool("Isattacking", true);//�ִϸ��̼� ���
        }
        else
        {
            GetComponent<Animator>().SetBool("Isattacking", false);//�ִϸ��̼� ����
        }
        //�ִϸ��̼� ���� Ȯ�� 
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("closeat") == true)
        {
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime >= 0 && animTime < 0.5f)
            {
                if (watchr)
                {
                    hitbox.transform.position = new Vector3(transform.position.x + 0.7f,
                        transform.position.y, 0); //��ġ ����
                }
                else if (watchl)
                {
                    hitbox.transform.position = new Vector3(transform.position.x - 0.8f,
                        transform.position.y, 0); //��ġ ����
                }
            }
            else
            {
                hitbox.transform.position = new Vector3(0, 100, 0);
            }
        }
    }
    void Fire()
    {
        //�� �߻�
        if (timer > delay && Input.GetKey(KeyCode.F))//�� �߻� ������
        {
            Instantiate(bullet, transform.position, transform.rotation);//�Ѿ� �߻�
            timer = 0;
        }
        timer += Time.deltaTime;
    }
    void Skill1()
    {
        if (c1timer > cooltime1)//��ų ��� ����(ui���� �ʿ�)
        {
            sk1using = false;
        }
        else
        {
            sk1using = true;
        }
        if (Input.GetKey(KeyCode.E) && c1timer > cooltime1)//��ų ��Ÿ�� �����Ϸ��� cooltime1�����ϼ���
        {
            if (watchl)
            {
                for (float i = 0; i <= 1; i += 0.5f)
                {
                    Instantiate(skill1, new Vector3(transform.position.x + (-i - 2.5f),
                    transform.position.y + 5f, 0), transform.rotation); //ĳ���� ���� �� (������ ������)
                }
            }
            if (watchr)
            {
                for (float i = 0; i <= 1; i += 0.5f)
                {
                    Instantiate(skill1, new Vector3(transform.position.x + (i + 2.5f),
                    transform.position.y + 5f, 0), transform.rotation); //ĳ���� ������ �� (������ ������)
                }
            }
            c1timer = 0;
        }
        c1timer += Time.deltaTime;
    }
    void Skill2()
    {
        if (c2timer > cooltime2)//ui���� �ʿ�
        {
            sk2using = false;
        }
        else
        {
            sk2using = true;
        }
        if (Input.GetKey(KeyCode.Q) && c2timer > cooltime2) //���������� ��Ÿ�� �����Ϸ��� cooltime2 �����ϼ���
        {
            Instantiate(skill2, new Vector3(transform.position.x + 0.17f,
            transform.position.y - 0.03f, 0), transform.rotation); //ĳ������ ��ġ���� ����
        }
        c2timer = 0;
        c2timer += Time.deltaTime;
    }
    void Skill_ui1() //��ų ui
    {
        string ct1 = Mathf.CeilToInt(cooltime1 - c1timer).ToString(); //��Ÿ�� ǥ��, ���� �ڸ� �ݿø�

        if (sk1using)
        {
            btn1.image.color = new Color(0.5f, 0.5f, 0.5f, 1); //��ų ��� �Ұ� �����϶� ��ο���
            t1.text = ct1;
        }
        else
        {
            btn1.image.color = new Color(1, 1, 1, 1); //��ų ��� ���� ��
            t1.text = "";
        }
    }
    void Skill_ui2()
    {
        string ct2 = Mathf.CeilToInt(cooltime2 - c2timer).ToString(); //��Ÿ�� ǥ��, ���� �ڸ� �ݿø�
        if (sk2using)
        {
            btn2.image.color = new Color(0.5f, 0.5f, 0.5f, 1);//��ų ��� �Ұ� �����϶� ��ο���
            t2.text = ct2;
        }
        else
        {
            btn2.image.color = new Color(1, 1, 1, 1); //��ų ��� ���� ��
            t2.text = "";
        }
    }
}