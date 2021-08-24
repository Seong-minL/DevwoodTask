using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    // 몬스터 기본 스탯
    public int Attack = 10;
    public int Defense = 0;
    public int Hp = 50;


    // 몬스터 제어 관련 변수
    Rigidbody2D rigid2D;  // 몬스터의 물리엔진
    Animator animator;  // 몬스터의 애니메이터
    GameObject Character;
    GameObject Weapon;
    float MaxWalkSpeed = 2.0f;  // 몬스터의 최대 이동속도
    float WalkForce = 27.0f;  // 몬스터의 이동속도
    int turning_Ratio = 40;  // 방향전환 확률
    float turning_Span = 7.0f;  // 방향전환 주기
    float turning_delta = 0;  // 방향전환 주기 측정
    float stop_delta = 0;  // 정지시간 측정
    int key = 1;  // 방향
    bool stop = false;  // 현재 멈춰있는가

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword" && this.Weapon.transform.localEulerAngles.z != 0)
        {
            int Attack_Force = this.Character.GetComponent<UserController>().Character_Stat.STR * 2;
            int Damage = Attack_Force - this.Defense;
            this.Hp -= Damage;
            if (this.Hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.Character = GameObject.Find("Character");
        this.Weapon = GameObject.Find("Sword");
    }

    void Update()
    {
        // 7초마다 일정확률로 방향전환
        this.turning_delta += Time.deltaTime;
        if (this.turning_delta > this.turning_Span)
        {
            // 2초간 정지
            this.stop_delta += Time.deltaTime;
            if (this.stop_delta <= 2.0f)
            {
                this.stop = true;
                this.rigid2D.velocity = new Vector2 (0, 0);
            }
            else
            {
                this.stop = false;
                this.turning_delta = 0;
            }

            // 40% 확률로 몬스터 방향전환
            int turn = Random.Range(1, 101);
            if (turn <= this.turning_Ratio && turn >= 1 && !this.stop)
            {
                this.key *= -1;
                this.stop_delta = 0;
            }
            else if (!this.stop)
            {
                this.stop_delta = 0;
            }
        }

        // 현재 몬스터 이동속도
        float Current_Speed = Mathf.Abs(this.rigid2D.velocity.x);

        // 몬스터 이동
        if (Current_Speed < this.MaxWalkSpeed && !this.stop)
        {
            this.rigid2D.AddForce(transform.right * key * this.WalkForce);
        }

        // 이동 방향에 따라 이미지 반전
        if (key != 0)
        {
            this.transform.localScale = new Vector3 (key * 0.5f, 0.5f, 1);
        }

        // 이동속도에 따라 애니메이션 재생 속도 조절
        this.animator.speed = Current_Speed / 2.0f;

        // 몬스터가 지정 구역의 끝에 도달했을 경우 잠시 멈추고 반대 방향으로 이동
        if (transform.position.x <= -7.9f)
        {
            this.turning_delta = 0;
            this.key = 1;
            this.rigid2D.AddForce(transform.right * key * this.WalkForce);

            // 1초간 정지 후 다시 움직임
            this.stop_delta += Time.deltaTime;
            if (this.stop_delta <= 1.0f)
            {
                this.stop = true;
                this.rigid2D.velocity = new Vector2 (0, 0);
            }
            else
            {
                this.stop = false;
            }
        }
        else if (transform.position.x >= 17.1f)
        {
            this.turning_delta = 0;
            this.key = -1;
            this.rigid2D.AddForce(transform.right * key * this.WalkForce);

            // 1초간 정지 후 다시 움직임
            this.stop_delta += Time.deltaTime;
            if (this.stop_delta <= 1.0f)
            {
                this.stop = true;
                this.rigid2D.velocity = new Vector2 (0, 0);
            }
            else
            {
                this.stop = false;
            }
        }
    }
}
