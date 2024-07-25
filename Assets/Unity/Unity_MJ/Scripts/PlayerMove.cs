using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 이동 속력
    float moveSpeed = 4.0f;
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

    float hAxis;
    float vAxis;

    Vector3 moveVec;

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
        Vector3 dirH = transform.right * h;

        dirH.Normalize();

        if (cc.isGrounded)
        {
            yVelocity = 0;
            JumpCurN = 0;
        }

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
    }

    void GetInput()
    {

    }
}
