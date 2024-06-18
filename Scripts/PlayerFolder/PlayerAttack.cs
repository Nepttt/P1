using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

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
public Button btn1; //(버튼 혹은 캔버스)
public TMP_Text t1; //스킬 쿨타임 표시 텍스트

//스킬 2
public static float cooltime2 = 5f; //쿨타임
public static float c2timer = 0;
public GameObject skill2; //스킬 2 오브젝트(prefab)
public static bool sk2using; //스킬 2 사용가능여부
//스킬 2 ui
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
hitbox.transform.position = new Vector3(transform.position.x - 0.8
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
if (timer > delay && Input.GetKey(KeyCode.F))//총 발사 딜레이
{
Instantiate(bullet, transform.position, transform.rotation);//총알 발사
timer = 0;
}
timer += Time.deltaTime;
}
void Skill1()
{
if (c1timer > cooltime1)//스킬 사용 여부(ui에서 필요)
{
sk1using = false;
}
else
{
sk1using = true;
}
if (Input.GetKey(KeyCode.E) && c1timer > cooltime1)//스킬 쿨타임 조절하려면 cooltime1조절하세요
{
if (watchl)
{
for (float i = 0; i <= 1; i += 0.5f)
{
Instantiate(skill1, new Vector3(transform.position.x + (-i - 2.5f),
transform.position.y + 5f, 0), transform.rotation); //캐릭터 왼쪽 위 (위에서 떨어짐)
}
}
if (watchr)
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
if (c2timer > cooltime2)//ui에서 필요
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
void Skill_ui1() //스킬 ui
{
string ct1 = Mathf.CeilToInt(cooltime1 - c1timer).ToString(); //쿨타임 표시, 일의 자리 반올림

if (sk1using)
{
btn1.image.color = new Color(0.5f, 0.5f, 0.5f, 1); //스킬 사용 불가 상태일때 어두워짐
t1.text = ct1;
}
else
{
btn1.image.color = new Color(1, 1, 1, 1); //스킬 사용 가능 시
t1.text = "";
}
}
void Skill_ui2()
{
string ct2 = Mathf.CeilToInt(cooltime2 - c2timer).ToString(); //쿨타임 표시, 일의 자리 반올림
if (sk2using)
{
btn2.image.color = new Color(0.5f, 0.5f, 0.5f, 1);//스킬 사용 불가 상태일때 어두워짐
t2.text = ct2;
}
else
{
btn2.image.color = new Color(1, 1, 1, 1); //스킬 사용 가능 시
t2.text = "";
}
}
}
