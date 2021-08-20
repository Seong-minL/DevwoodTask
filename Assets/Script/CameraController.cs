using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject character;  // 캐릭터
    float x_Pos;
    float y_Pos;
    void Start()
    {
        this.character = GameObject.Find("Character");  // 캐릭터
    }

    void Update()
    {
        Vector3 characterPos = this.character.transform.position;  // 캐릭터 위치

        // 캐릭터 위치에 맞춰 카메라를 이동시킴
        transform.position = new Vector3(
            characterPos.x + 4.5f, characterPos.y + 2.2f, characterPos.z);

        // 캐릭터가 벽에 가까이 갈 경우 일정 거리가 되면 더 이상 이동 안 함
        if (transform.position.x >= 10.0f)
        {
            transform.position = new Vector3(
                10.0f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -10.0f)
        {
            transform.position = new Vector3(
                -10.0f, transform.position.y, transform.position.z);
        }

        // 캐릭터가 낙하할 경우 일정 거리가 되면 더 이상 낙하 안 함
        if (transform.position.y <= -1.0f)
        {
            transform.position = new Vector3(
                transform.position.x, -1.0f, transform.position.z);
        }
    }
}
