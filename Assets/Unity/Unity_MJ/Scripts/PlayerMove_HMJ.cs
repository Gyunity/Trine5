using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static ArrowMove_HMJ;
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

    public float bounceAmount = 0.2f; // 반동의 크기
    public float bounceSpeed = 0.5f;   // 반동의 속도

    LineRenderer lineRenderer;

    float startTime;

    public float swingSpeed = 10.0f;      // 흔들림 속도 (스윙 속도)
    public float swingRadius = 5.0f;     // 흔들림 반지름 (밧줄의 길이)
    public float swingAngle = 180.0f;     // 흔들림 각도 (최대 각도)

    private float currentAngle = 0f;   // 현재 각도
    private bool isSwingingLeft = false;  // 왼쪽으로 스윙 중인지 여부
    private bool isSwingingRight = false; // 오른쪽으로 스윙 중인지 여부

    float SwingingmoveSpeed = 0.1f;


    Vector3 originPos;
    Vector3 grapPos;

    float angle = 90.0f;

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

        // DrawArrow
        // 벡터 크기가 0보다 크면
        if (movement.magnitude > 0 && playerState.GetState() != PlayerState.DrawArrow && playerState.GetState() != PlayerState.Swinging)
        {
            // 이동 방향으로 캐릭터 회전
            Quaternion newRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10.0f);
            anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
        }

        UpdateKey(); // 반동 업데이트 키
        Jump();
        PlayerMove();
        Swinging();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerState.SetState(PlayerState.Dash);
        }
        // Dash();

        if (Input.GetMouseButton(0))
        {
            Debug.Log("드로우 에셋");
            playerState.SetState(PlayerState.DrawArrow);
        }
           

        if (Input.GetMouseButtonUp(0) && playerState.GetState() == PlayerState.DrawArrow) 
                playerState.SetState(PlayerState.ShootArrow);


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerState.SetState(PlayerState.Dash);
        }


        //z축 고정 추가 (규현)
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
    }

    void UpdateLineRender(Vector3 TargetPosition, Vector3 playerHandPositoin)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, playerHandPositoin);
        lineRenderer.SetPosition(1, TargetPosition);
    }

    public bool SelectHangingObject()
    {
        LayerMask targetLayer = 1 << LayerMask.NameToLayer("HangObject");
        // 마우스 포지션을 얻기 위해 Ray를 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 특정 레이어에 속한 오브젝트만 Raycast를 통해 검출
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
        {
            // 일단 궁수일때라고 가정(나중에 궁수일때만 그랩할 수 있도록 수정해야할 듯)


            grapPos = hit.collider.transform.position;
        }

        return true;
    }



    //public void MoveWithBounce()
    //{
    //    // 반동 효과 계산
    //    float bounceX = Mathf.Sin((Time.time - startTime) * bounceSpeed) * bounceAmount;
    //    float bounceY = Mathf.Cos((Time.time - startTime) * bounceSpeed) * bounceAmount * 0.5f; // Y축 반동은 약간 줄임
    //    transform.position += new Vector3(bounceX, bounceY, 0);

    //    //// 키를 떼면 이동 중지
    //    //if ((horizontal < 0 && Input.GetKeyUp(KeyCode.LeftArrow)) ||
    //    //    (horizontal > 0 && Input.GetKeyUp(KeyCode.RightArrow)))
    //    //{

    //    //}
    //}

    void UpdateKey()
    {
        if (playerState.GetState() != PlayerState.Swinging)
            return;
        // 왼쪽 또는 오른쪽 이동 입력 감지
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isSwingingLeft = true;
            isSwingingRight = false;
            Swing();

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            isSwingingRight = true;
            isSwingingLeft = false;
            Swing();
        }
        else
        {
            isSwingingLeft = false;
            isSwingingRight = false;
        }
    }

    void Swing()
    {
        // 현재 각도 업데이트 (왼쪽 또는 오른쪽에 따라 증가 또는 감소)
        if (isSwingingLeft)
        {
            angle += SwingingmoveSpeed * Time.deltaTime;
            //currentAngle = Mathf.Clamp(currentAngle + swingSpeed * Time.deltaTime, -swingAngle, swingAngle);
            UpdateSwing();
        }
        else if (isSwingingRight)
        {
            angle -= SwingingmoveSpeed * Time.deltaTime;
            //currentAngle = Mathf.Clamp(currentAngle - swingSpeed * Time.deltaTime, -swingAngle, swingAngle);
            UpdateSwing();
        }
    }

    void UpdateSwing()
    {
        // 스윙 각도에 따른 X, Y 좌표 계산
        float radianAngle = currentAngle * Mathf.Deg2Rad;

        float x = Mathf.Sin(radianAngle) * swingRadius;
        float y = Mathf.Cos(radianAngle) * swingRadius;

        Debug.Log("각도 y각도 이상!: x: " + x + "y: " + y);
        // 새로운 위치로 이동
        transform.position = originPos + new Vector3(x, y, 0.0f);


        lineRenderer = GetComponentInChildren<LineRenderer>();

        GameObject swingingPlayerObject = FindBoneManager_HMJ.Instance.FindBone(GameObject.Find("Player").transform, "SwingingObject").transform.gameObject;
        UpdateLineRender(grapPos, swingingPlayerObject.transform.position);

        Debug.Log("Swing - Distance: " + swingRadius);
        Debug.Log("SwingData-test: x: " + transform.position.x + "y: " + transform.position.y + "z: " + transform.position.z);
        // MOVE~~~~
    }

    void Swinging()
    {
        if (Input.GetMouseButtonDown(1))
        {
            startTime = Time.time;
            if(playerState.SetState(PlayerState.Swinging)) // 스테이트가 바뀌었으면
            {
                originPos = transform.position;
                GameObject swingingPlayerObject = FindBoneManager_HMJ.Instance.FindBone(GameObject.Find("Player").transform, "SwingingObject").transform.gameObject;
                swingRadius = Vector3.Distance(swingingPlayerObject.transform.position, grapPos);

                Vector3 direction = grapPos - swingingPlayerObject.transform.position;

                // 벡터의 각도를 구함 (y축 기준
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                if (angle < 0) angle += 360.0f;

                currentAngle = angle;
            }
                
        }

        if (Input.GetMouseButton(1))
        {
            Swing();
            Debug.Log("Swing~~");
        }
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
        if (cc.isGrounded && (playerState.GetState() != PlayerState.DrawArrow) && (playerState.GetState() != PlayerState.Swinging))
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
        if (playerState.GetState() == PlayerState.Grap) // 무언가를 잡고 있을때
        {
            yVelocity = 0.0f;
            JumpCurN = 0;
        }
        else
        {
            if(playerState.GetState() != PlayerState.DrawArrow && playerState.GetState() != PlayerState.Swinging)
                cc.Move((movement * moveSpeed + new Vector3(0.0f, yVelocity, 0.0f)) * Time.deltaTime);
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
    }

    public void SetCollisionCollider(Collider collider)
    {
        targetCollider = collider;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("충돌" + collision.gameObject.name);
    }

}
