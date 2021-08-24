using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public string Weapon_Type;  // 무기 종류
    public bool isAttack = false;  // 공격 중인가

    GameObject character;  // 캐릭터
    Rigidbody2D rigid;
    Animator anim;

    void Start()
    {
        this.character = GameObject.Find("Character");  // 캐릭터
        this.Weapon_Type = this.gameObject.tag;
        this.anim = this.GetComponent<Animator>();
        this.rigid = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        this.isAttack = false;

        // 캐릭터 위치
        Vector3 characterPos = this.character.transform.position;

        // 무기가 캐릭터를 따라다니도록 위치 설정
        transform.position = new Vector3(
            characterPos.x, characterPos.y-0.2f, characterPos.z);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.anim.SetInteger("Dir", 1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.anim.SetInteger("Dir", -1);
        }

        if (Input.GetKeyDown(KeyCode.A) && this.anim.GetInteger("Dir") == 1)
        {
            this.anim.SetTrigger("Right_Attack");
            this.isAttack = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && this.anim.GetInteger("Dir") == -1)
        {
            this.anim.SetTrigger("Left_Attack");
            this.isAttack = true;
        }

        this.anim.speed = 2.0f;
    }
}
