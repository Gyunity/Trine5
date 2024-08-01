using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    Collider targetCollider;
    // Character Controller
    public CharacterController cc;

    // 점프파워
    public float jumpPower = 2.0f;

    // 중력
    float gravity = -9.81f;

    // y 방향 속력
    public float yVelocity;

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

    Rigidbody rb;
    Animator anim;
    float horizontal = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Character Controller
        
        cc = GetComponent<CharacterController>();
        rb = GetComponentInChildren<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        playerState = GameObject.Find("Player").GetComponentInChildren<PlayerState_HMJ>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerState.GetState() != PlayerState.Grap && playerState.GetState() != PlayerState.Climb)
        horizontal = Input.GetAxis("Horizontal");

        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        //    playerState.SetState(PlayerState.Walk);
        //else
        //    playerState.SetState(PlayerState.Idle);

        movement = new Vector3(horizontal, 0.0f, 0.0f);
        // -1 ~ 1
        // -1 ~ 0 // 왼쪽 1 `
        // 0 ~ 1

        // 벡터 크기가 0보다 크면
        if(movement.magnitude > 0)
        {
            // 이동 방향으로 캐릭터 회전
            Quaternion newRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10.0f);
            anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
        }


        

        Jump();
        PlayerMove();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerState.SetState(PlayerState.Dash);
        }
        // Dash();
    }

    public void Dash()
    {
        Debug.Log("Dash Data: moveSpeed - " + moveSpeed + "dashMaxSpeed - " + dashMaxSpeed);
        playDashTime += Time.deltaTime;
        // 0 ~ 1 -> 대쉬 플레이 시간 / 대쉬 시간
        moveSpeed = Mathf.Lerp(moveSpeed, dashMaxSpeed, playDashTime / dashTime);
        // moveSpeed -> dashMaxSpeed로 값 변경 
        cc.Move(dashDir * moveSpeed * Time.deltaTime);

        if(playDashTime >= dashTime)
        {
            playerState.SetState(PlayerState.Idle);
        }
    }

    void Jump()
    {

        // 땅에 있음
        if (cc.isGrounded)
        {
            JumpCurN = 0;
            yVelocity = 0.0f;
           
            playerState.SetState(PlayerState.Idle);              
        }

        if (Input.GetButtonDown("Jump"))
        {
            playerState.SetState(PlayerState.Jump);
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
        if (playerState.GetState() == PlayerState.Grap) // 무a언가를 잡고 있을때
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                playerState.SetState(PlayerState.Climb);
                //cc.Move(new Vector3(10.0f, 30.0f, 0.0f) * moveSpeed * Time.deltaTime);
                yVelocity = 0.0f;
            }
        }
        else
        {
            cc.Move((movement * moveSpeed + new Vector3(0.0f, yVelocity, 0.0f)) * Time.deltaTime);
        }

        if (playerState.GetState() == PlayerState.Climb) // 올라가는 것
        {
            
        }
    }
    void GrabMove()
    {
        cc.Move(new Vector3(1.0f, 0.0f, 0.0f) * moveSpeed * Time.deltaTime);
    }

    public void ResetDashData()
    {
        playDashTime = 0.0f;
        moveSpeed = 4.0f;
    }

    private void FixedUpdate()
    {
       //PlayerMove();
    }

    public void SetCollisionCollider(Collider collider)
    {
        targetCollider = collider;
    }

}
