using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerState_HMJ;

public class PlayerMove_HMJ : MonoBehaviour
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
    public float jumpPower = 2.0f;

    // 중력
    float gravity = -9.81f;

    // y 방향 속력
    float yVelocity;

    // 점프 최대 횟수
    public int maxJumpN = 2;

    // 점프 횟수
    int JumpCurN = 0;

    // 대쉬 방향
    Vector3 dashDir;

    Vector3 dirH;

    // bool bRight = false;

    Vector3 movement;

    PlayerState_HMJ playerState;

    // Start is called before the first frame update
    void Start()
    {
        // Character Controller
        cc = GetComponent<CharacterController>();
        playerState = GameObject.Find("Player").GetComponentInChildren<PlayerState_HMJ>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        movement = new Vector3(h, 0.0f, 0.0f);

        // 벡터 크기가 0보다 크면
        if(movement.magnitude > 0)
        {
            // 이동 방향으로 캐릭터 회전
            Quaternion newRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10.0f);
        }
        Jump();
        Dash();
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Dash!");
            playerState.SetState(PlayerState.Dash);
        }
            
        if (playerState.GetState() == PlayerState.Dash)
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
            playerState.SetState(PlayerState.Idle);
            moveSpeed = 4.0f;
        }
    }

    void Jump()
    {
        // 땅에 있음
        if (cc.isGrounded)
        {
            JumpCurN = 0;
            playerState.SetState(PlayerState.Idle);
        }
        if (Input.GetButtonDown("Jump"))
        {
            playerState.SetState(PlayerState.Jump);
            Debug.Log("점프 버튼 누름.");
            // 만약에 현재 점프 횟수가 최대 점프 횟수보다 작으면
            if (JumpCurN < maxJumpN)
            {
                yVelocity = jumpPower;
                JumpCurN++;
            }
        }

        yVelocity += gravity * Time.deltaTime;

        // dir.y 값에 yVelocity를 셋팅
        movement.y += yVelocity;
    }
    void PlayerMove()
    {
        if (playerState.GetState() == PlayerState.Grap) // 무언가를 잡고 있을때
        {
            cc.Move(dashDir * moveSpeed * Time.deltaTime);
        }
        else
        {
            cc.Move((movement * moveSpeed + new Vector3(0.0f, yVelocity, 0.0f)) * Time.fixedDeltaTime);
        }
    }
    private void FixedUpdate()
    {
        PlayerMove();
    }

}
