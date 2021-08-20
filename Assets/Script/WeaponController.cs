using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public string Weapon_Type;  // 무기 종류
    public bool isAttack = false;  // 공격 중인가
    GameObject character;  // 캐릭터
    float current_angle = -50.0f;  // 현재 기울기
    int dir;  // 캐릭터 이동 방향
    float rotSpeed;

    void Start()
    {
        this.character = GameObject.Find("Character");  // 캐릭터
        this.Weapon_Type = this.gameObject.tag;
    }

    void Update()
    {
        // 캐릭터 위치
        Vector3 characterPos = this.character.transform.position;

        // 무기가 캐릭터를 따라다니도록 위치 설정
        transform.position = new Vector3(
            characterPos.x, characterPos.y-0.2f, characterPos.z);

        // 캐릭터 방향에 따라 이미지 반전
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.dir = -1;
            transform.localEulerAngles = new Vector3(
                0, 0, current_angle * dir);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.dir = 1;
            transform.localEulerAngles = new Vector3(
                0, 0, current_angle * dir);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            this.isAttack = true;
            if (this.Weapon_Type == "Sword")
            {
                this.rotSpeed = 10.0f;
            }
        }

        if (this.isAttack)
        {
            if (this.dir == -1)
            {
                while (this.transform.localEulerAngles.z > -90)
                {
                    this.transform.Rotate(0, 0, this.rotSpeed);
                }
            }
        }
        
    }
}
