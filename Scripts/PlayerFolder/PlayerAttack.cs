using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    public static bool watchr;
    public static bool watchl;
    public GameObject hitbox;
    public GameObject bullet;
  void Update()
  {
      Attack();
      Fire();
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
}
