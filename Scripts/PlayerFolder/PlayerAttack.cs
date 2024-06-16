using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    //플레이어 보는 방향
    public static bool watchr;
    public static bool watchl;

    //근거리 공격 히트박스
    public GameObject hitbox;

    //원거리
    public GameObject bullet;

    //스킬 1
    public static float cooltime1 = 5f; //쿨타임
    public static float c1timer = 0;
    public GameObject skill1; //스킬 1 오브젝트(prefab)
    public static bool sk1using;//스킬 1 사용가능여부
    //스킬 1 ui
    public Button btn; //(버튼 혹은 캔버스)
    public TMP_Text t; //스킬 쿨타임 표시 텍스트
    
    //스킬 2
    public static float cooltime2 = 5f; //쿨타임
    public static float c2timer = 0;
    public GameObject skill2; //스킬 2 오브젝트(prefab)
    public static bool sk2using; //스킬 2 사용가능여부
 void Start()
 {
    btn = GameObject.Find("skill1button").GetComponent<Button>();
    t = GameObject.Find("skill1text").GetComponent<TMP_Text>();
 }
  void Update()
  {
      Attack();
      Fire();
      Skill1();
      Skill2();
  }
  void Attack()
  {
    //근접 공격
        if (Input.GetKey(KeyCode.S))//키 바꾸기
        {
            GetComponent<Animator>().SetBool("Isattacking", true);//애니메이션 재생
        }
        else
        {
            GetComponent<Animator>().SetBool("Isattacking", false);//애니메이션 끊기
        }
        //애니메이션 상태 확인 
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("closeat") == true)
        {
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime >= 0 && animTime < 0.5f)
            {
                if (watchr)
                {
                    hitbox.transform.position = new Vector3(transform.position.x + 0.7f, 
                        transform.position.y, 0); //수치 변경
                }
                else if (watchl)
                {
                    hitbox.transform.position = new Vector3(transform.position.x - 0.8f, 
                        transform.position.y, 0); //수치 변경
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
    //총 발사
      if (timer > delay && Input.GetKey(KeyCode.F))
      {
          Instantiate(bullet, transform.position, transform.rotation);
          timer = 0;
      }
      timer += Time.deltaTime;
    }
    void Skill1()
    {
        if (c1timer > cooltime1)
        {
            sk1using = false;
        }
        else
        {
            sk1using = true;
        }
        if (Input.GetKey(KeyCode.E) && c1timer > cooltime1)//스킬 쿨타임 조절하려면 cooltime1조절하세요
        {
            if (Player.watchl)
            {
                for (float i = 0; i <= 1; i += 0.5f)
                {
                    Instantiate(skill1, new Vector3(transform.position.x + (-i - 2.5f), 
                    transform.position.y + 5f, 0), transform.rotation); //캐릭터 왼쪽 위 (위에서 떨어짐)
                }
            }
            if (Player.watchr)
            {
                for (float i = 0; i <= 1; i += 0.5f)
                {
                    Instantiate(skill1, new Vector3(transform.position.x + (i + 2.5f), 
                    transform.position.y + 5f, 0), transform.rotation); //캐릭터 오른쪽 위 (위에서 떨어짐)
                }
            }
            c1timer = 0;
        }
        c1timer += Time.deltaTime;
    }
    void Skill2()
    {
        if (c2timer > cooltime2)
        {
            sk2using = false;
        }
        else
        {
            sk2using = true;
        }
        if (Input.GetKey(KeyCode.Q) && c2timer > cooltime2) //마찬가지로 쿨타임 변경하려면 cooltime2 조절하세요
        {
            Instantiate(skill2, new Vector3(transform.position.x + 0.17f, 
            transform.position.y - 0.03f, 0), transform.rotation); //캐릭터의 위치에서 생성
        }
        c2timer = 0;
        c2timer += Time.deltaTime;
    }
    void Cooltime()
    {
        if (sk1using)
        {
            btn.image.color = new Color(0.5f,0.5f, 0.5f, 1);
        }
        else
        {
            btn.image.color = new Color(1, 1, 1, 1);
        }
    }
    void Timer()
    {
        int cooltime = Mathf.CeilToInt(cooltime1 - c1timer);
        string ct = cooltime.ToString();
        if (sk1using)
        {
            t.text = ct;
        }
        else
        {
            t.text = "";
        }
    }
}
