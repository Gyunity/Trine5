using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 이동 속력
    float moveSpeed = 4.0f;

    float playDashTime = 0.0f;
    // 대쉬 최대 속력
    float dashMaxSpeed = 20.0f;
    float dashTime = 0.3f;

    // Character Controller
    public CharacterController cc;

    // 점프파워
    public float jumpPower = 3;

    // 중력
    float gravity = -9.81f;

    // y 방향 속력
    float yVelocity;

    // 점프 최대 횟수
    public int maxJumpN = 2;

    // 점프 횟수
    int JumpCurN = 0;

    // 대쉬
    bool dash = false;

    // 대쉬 방향
    Vector3 dashDir;

    Vector3 dirH;

    bool bRight = false;

    // Start is called before the first frame update
    void Start()
    {
        // Character Controller
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        dirH = Vector3.right * h;
        

        dirH.Normalize();

        if (cc.isGrounded)
        {
            yVelocity = 0;
            JumpCurN = 0;
        }

        // 스페이스 바를 누르면
        if (Input.GetKeyDown(KeyCode.A)) // 왼쪽
            bRight = false;

        if (Input.GetKeyDown(KeyCode.D)) // 오른쪽
            bRight = true;

        // 스페이스 바를 누르면
        if (Input.GetButtonDown("Jump"))
        {
            // 만약에 현재 점프 횟수가 최대 점프 횟수보다 작으면
            if (JumpCurN < maxJumpN)
            {
                yVelocity = jumpPower;
                JumpCurN++;
            }
        }
        // yVelocity를 중력값을 이용해서 감소시킨다.
        // v = v0 + at;
        yVelocity += gravity * Time.deltaTime;

        // dir.y 값에 yVelocity를 셋팅
        dirH.y = yVelocity;


        cc.Move(dirH * moveSpeed * Time.deltaTime);
        Dash();
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Dash!");
            dash = true;

            if (bRight)
                dashDir = new Vector3(1.0f, 0.0f, 0.0f);
            else
                dashDir = new Vector3(-1.0f, 0.0f, 0.0f);
        }
            

        if (dash)
        {
            playDashTime += Time.deltaTime;
            // 0 ~ 1 -> 대쉬 플레이 시간 / 대쉬 시간
            moveSpeed = Mathf.Lerp(moveSpeed, dashMaxSpeed, playDashTime / dashTime);
            // moveSpeed -> dashMaxSpeed로 값 변경
            cc.Move(dashDir * moveSpeed * Time.deltaTime);
        }

        if (playDashTime > dashTime)
        {
            playDashTime = 0.0f;
            dash = false;
            moveSpeed = 4.0f;
        }
    }

}
