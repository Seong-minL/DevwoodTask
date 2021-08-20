using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserController : MonoBehaviour
{
    // 유저 스탯
    public int Hp = 200;  // 캐릭터 Hp
    public int Mp = 200;  // 캐릭터 Mp
    public int Level = 1;  // 캐릭터 레벨
    public int STR = 5;  // 캐릭터 힘 스탯
    public int DEX = 5;  // 캐릭터 민첩 스탯
    public int INT = 5;  // 캐릭터 지능 스탯
    public string Job = "초보자";  // 캐릭터 직업


    // 캐릭터 제어 관련 변수
    Animator animator;  // 애니메이터
    Rigidbody2D rigid2D;  // 물리엔진
    float jumpForce = 520.0f;  // 점프력
    float walkForce = 35.0f;  // 이동속도
    float maxWalkSpeed = 3.0f;  // 최대속도
    Scene current_Scene;  // 현재 씬(맵)
    float ropeSpeed = 0.1f;  // 로프 올라가는 속도
    public bool ropeClimbing = false;  // 로프를 사용 중인지 여부
    float charlength = 0.53f;  // 캐릭터 대략적인 크기
    float groundlength;  // 맵 크기

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();  // 물리 엔진
        this.animator = GetComponent<Animator>();  // 애니메이터
        this.current_Scene = SceneManager.GetActiveScene();  // 현재 씬(맵)
        int current_Scene_number = current_Scene.buildIndex;
        if (current_Scene_number >= 0)
        {
            this.groundlength = 18.0f;
        }
    }

    // 포탈 / 로프
    void OnTriggerStay2D(Collider2D other)
    {
        // 오른쪽 포탈
        if (other.gameObject.tag == "Right_Portal" && Input.GetKey(KeyCode.UpArrow))
        {
            SceneManager.LoadScene(current_Scene.buildIndex + 1);
        }
        // 로프
        else if (other.gameObject.tag == "Rope")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.rigid2D.velocity = new Vector2(0, 0);
                this.ropeClimbing = true;
                this.rigid2D.isKinematic = true;
                this.animator.SetTrigger("Rope");
                this.transform.position = new Vector3(
                    other.transform.position.x, this.transform.position.y, 0);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(0, ropeSpeed, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(0, -ropeSpeed, 0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rope")
        {
            this.rigid2D.isKinematic = false;
            this.ropeClimbing = false;
        }
    }

    void Update()
    {
        // 스페이스바를 누르면 점프한다
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0 && !this.ropeClimbing)
        {
            // 점프 시에는 JumpTrigger를 활성화 해 점프 애니메이션 실행
            this.animator.SetTrigger("JumpTrigger");  
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // 방향키에 따라 캐릭터 이동방향설정
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // 현재 이동속도를 구함
        float walkspeed = Mathf.Abs(this.rigid2D.velocity.x);

        // 방향키를 누르면 이동
        if ((walkspeed < this.maxWalkSpeed) && !this.ropeClimbing)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // 방향키를 누르면 해당 방향에 맞게 이미지 반전
        if (key != 0)
        {
            transform.localScale = new Vector3(-key, 1, 1);
        }

        // 캐릭터의 속도에 맞게 애니메이션 속도 조정
        if (this.rigid2D.velocity.y == 0 || !this.ropeClimbing)
        {
            this.animator.speed = walkspeed / 2.0f;
        }
        else if (this.rigid2D.velocity.y != 0 && !this.ropeClimbing)
        {
            this.animator.speed = 1.5f;
        }

        // 캐릭터가 맵 밖으로 나가지 못하게 함
        if (transform.position.x <= (-groundlength + charlength))
        {
            transform.position = new Vector3(
                -groundlength + charlength, transform.position.y, 0);
        }
        else if (transform.position.x >= (groundlength - charlength))
        {
            transform.position = new Vector3(
                groundlength - charlength, transform.position.y, 0);
        }
    }
}
