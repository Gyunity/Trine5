using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UIElements;
using static ArrowMove_HMJ;
using static PlayerState_HMJ;
using static UnityEngine.Rendering.DebugUI;

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

    public float swingSpeed = 20.0f;      // 흔들림 속도 (스윙 속도)
    public float boundingSpeed = 1.0f;      // 반동 속도 (반동 속도)
    public float swingRadius = 5.0f;     // 흔들림 반지름 (밧줄의 길이)
    public float swingAngle = 90.0f;     // 흔들림 각도 (최대 각도)

    private float currentAngle = 0f;   // 현재 각도

    float boundingAngle = 0.0f;
    private bool isSwingingLeft = false;  // 왼쪽으로 스윙 중인지 여부
    private bool isSwingingRight = false; // 오른쪽으로 스윙 중인지 여부

    // 260 ~ 100


    float swingingmoveSpeed = 10.0f;

    float swingMaxAngle = 260.0f;
    float swingMinAngle = 100.0f;

    // angleFloat
    Vector3 originPos;
    Vector3 grapPos;

    float angle = 90.0f;


    bool left = false;
    float moveFirstAngle = 0.0f;
    float moveAngle = 0.0f;

    ChangeCharacter changeCharacter;

    ArrowManager_HMJ arrowManager;

    float attackLeftTime = 0.0f;

    Vector3 incDir;

    StaminaSystem_HMJ staminaSystem;

    // GetArrowDirection
    // Start is called before the first frame update
    void Start()
    {
        // Character Controller
        
        cc = GetComponent<CharacterController>();
        rb = GetComponentInChildren<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        
        playerState = GameObject.Find("Player").GetComponentInChildren<PlayerState_HMJ>();
        changeCharacter = GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>();
        //wayPointData = GameObject.Find("RootManager").GetComponentInChildren<PlayerWayPoint_HMJ>();

        //playerState.SetplayerMoveState(PlayerMoveState.Player_ZeroZ);

        arrowManager = GameObject.Find("ArrowManager").GetComponent<ArrowManager_HMJ>();

        staminaSystem = GameObject.Find("Player").GetComponentInChildren<StaminaSystem_HMJ>();
    }
    // Update is called once per frame
    void Update()
    {
        if(playerState.GetState() != PlayerState.DrawArrow)
            PlayerZFixZeroMove();

        if (movement.magnitude > 0 /*&& playerState.GetState() != PlayerState.DrawArrow*/ && playerState.GetState() != PlayerState.Swinging)
        {
            if (playerState.GetState() == PlayerState.DrawArrow)
                movement = new Vector3(arrowManager.GetArrowDirection().x, 0.0f, 0.0f);
            // 이동 방향으로 캐릭터 회전
            Quaternion newRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10.0f);
            anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
        }

        UpdateKey(); // 반동 업데이트 키
        Jump();
        PlayerMove();
        Swinging();
        if (Input.GetKeyDown(KeyCode.LeftShift) && staminaSystem.EnableDash())
        {
            playerState.SetState(PlayerState.Dash);
        }

        if(changeCharacter.GetPlayerCharacterType() == PlayerCharacterType.WarriorType)
        {
            attackLeftTime -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && (playerState.GetState() != PlayerState.Attack00))
            {
                playerState.SetState(PlayerState.Attack00);
            }

        }

        // Dash();

        if (changeCharacter.GetPlayerCharacterType() == PlayerCharacterType.ArcherType)
        {
            if (Input.GetMouseButton(0) && (playerState.GetState() == PlayerState.Idle || playerState.GetState() == PlayerState.Walk))
            {
                Debug.Log("드로우 에셋");
                playerState.SetState(PlayerState.DrawArrow);
            }


            if (Input.GetMouseButtonUp(0) && playerState.GetState() == PlayerState.DrawArrow)
                playerState.SetState(PlayerState.ShootArrow);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && staminaSystem.EnableDash())
        {
            playerState.SetState(PlayerState.Dash);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void PlayerZFixZeroMove()
    {
        if (playerState.GetState() != PlayerState.Grap && playerState.GetState() != PlayerState.Climb)
            horizontal = Input.GetAxis("Horizontal");
        movement = new Vector3(horizontal, 0.0f, 0.0f);
    }

    // z 방향만 바꾼다.
    void Player_FixZMove()
    {
        if (playerState.GetState() != PlayerState.Grap && playerState.GetState() != PlayerState.Climb)
            horizontal = Input.GetAxis("Horizontal"); // - 1 ~ 1 * moveDirection 

        //Vector3 Direction = wayPointData.GetMoveDirection();
        //Direction = new Vector3(horizontal * Direction.x, 0.0f, horizontal * Direction.z);
       // movement = Direction;
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

            GameObject swingingPlayerObject = FindBoneManager_HMJ.Instance.FindBone(GameObject.Find("Player").transform, "SwingingObject").transform.gameObject;

            grapPos.z = 0;
            Vector3 PlayerGrap = new Vector3(swingingPlayerObject.transform.position.x, swingingPlayerObject.transform.position.y, 0.0f);
            swingRadius = Vector3.Distance(GameObject.Find("Player").transform.position, grapPos);

            startTime = Time.time;
            return true;
        }

        return false;
    }

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

            Debug.Log("회전중 오른쪽~");

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            isSwingingRight = true;
            isSwingingLeft = false;
            Swing();

            Debug.Log("회전중 왼쪽~");
        }
        else
        {
            isSwingingLeft = false;
            isSwingingRight = false;
        }

        if (moveFirstAngle <= 0.0f)
            return;
        if (left) // 왼쪽
        {
            moveAngle -= boundingSpeed * Time.deltaTime;
            currentAngle -= boundingSpeed * Time.deltaTime;
            UpdateSwing();

            if(moveAngle <= 0.0f)
            {
                left = false;
                moveFirstAngle -= 2.0f;

                moveAngle = moveFirstAngle;
                moveAngle = Mathf.Clamp(moveAngle, 90.0f, 270.0f);
            }
        }
        else
        {
            moveAngle -= boundingSpeed * Time.deltaTime;
            currentAngle += boundingSpeed * Time.deltaTime;
            UpdateSwing();

            if (moveAngle <= 0.0f)
            {
                left = true;
                moveFirstAngle -= 2.0f;
  
                moveAngle = moveFirstAngle;
                moveAngle = Mathf.Clamp(moveAngle, 90.0f, 270.0f);
            }
        }
    }

    void Swing()
    {
        // 현재 각도 업데이트 (왼쪽 또는 오른쪽에 따라 증가 또는 감소)
        if (isSwingingLeft)
        {
            currentAngle -= swingSpeed * Time.deltaTime;
            //currentAngle = Mathf.Clamp(currentAngle, -200, -90); // Define minAngle and maxAngle
            UpdateSwing();
        }
        else if (isSwingingRight)
        {
            currentAngle += swingSpeed * Time.deltaTime;
            //currentAngle = Mathf.Clamp(currentAngle, -200, -90); // Define minAngle and maxAngle
            UpdateSwing();
        }



        // currentAngle


    }

    void UpdateSwing()
    {
        // 스윙 각도에 따른 X, Y 좌표 계산
        float radianAngle = currentAngle * Mathf.Deg2Rad;

        float x = Mathf.Cos(radianAngle) * swingRadius;
        float y = Mathf.Sin(radianAngle) * swingRadius;


        Debug.Log("중심점: " + grapPos);

        lineRenderer = GetComponentInChildren<LineRenderer>();

        GameObject swingingPlayerObject = FindBoneManager_HMJ.Instance.FindBone(GameObject.Find("Player").transform, "SwingingObject").transform.gameObject;
        UpdateLineRender(grapPos, swingingPlayerObject.transform.position);

        transform.position = grapPos + new Vector3(x, y, 0.0f);

        Debug.Log("Swing - Distance: " + swingRadius);
        Debug.Log("Distance-Grap - Player:" + Vector3.Distance(grapPos, transform.position));
        // MOVE~~~~
    }

    void Swinging()
    {

        if (Input.GetMouseButtonDown(1) && SelectHangingObject() && (changeCharacter.GetPlayerCharacterType() == PlayerCharacterType.ArcherType))
        {
            startTime = Time.time;
            if(playerState.SetState(PlayerState.Swinging)) // 스테이트가 바뀌었으면
            {
                originPos = grapPos;
                // 초기 그랩 시 각도 계산
                Vector3 direction = GameObject.Find("Player").transform.position - originPos;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                if (angle < 0)
                {
                    angle += 360.0f;
                    currentAngle = angle;
                }

                // 90 ~ 270
                if (currentAngle >= 180.0f)
                {
                    
                    left = true;
                    moveFirstAngle = Mathf.Clamp(currentAngle - 180.0f, 90.0f, 180.0f);
                }
                else
                { 
                    left = false;
                    moveFirstAngle = Mathf.Clamp(currentAngle, 90.0f, 180.0f); 
                }

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
        if (cc.isGrounded && (playerState.GetState() != PlayerState.DrawArrow) && (playerState.GetState() != PlayerState.Swinging) && (playerState.GetState() != PlayerState.Attack00) && (playerState.GetState() != PlayerState.Attack01))
        {
            JumpCurN = 0;
            yVelocity = 0.0f;
           
            playerState.SetState(PlayerState.Idle);              
        }

        if (Input.GetButtonDown("Jump"))
        {
            if(playerState.SetState(PlayerState.Jump))
            {
                swingSpeed = 20.0f;      // 흔들림 속도 (스윙 속도)
                boundingSpeed = 30.0f;      // 반동 속도 (반동 속도)
                swingRadius = 5.0f;     // 흔들림 반지름 (밧줄의 길이)
                swingAngle = 90.0f;     // 흔들림 각도 (최대 각도)
                currentAngle = 0.0f;
                boundingAngle = 0.0f;
                isSwingingLeft = false;
                isSwingingRight = false;
}
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
            incDir = GetIncVector();

            if (playerState.GetState() != PlayerState.DrawArrow && playerState.GetState() != PlayerState.Swinging)
                cc.Move((movement * moveSpeed + new Vector3(0.0f, yVelocity, 0.0f) + incDir)  * Time.deltaTime);
        }



    }

    Vector3 GetIncVector()
    {
        Vector3 result = Vector3.zero;

        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        RaycastHit hitinfo;

        if(Physics.Raycast(ray,out hitinfo,5, 1 << LayerMask.NameToLayer("SummonedObject")))
        {
           float dot = -Vector3.Dot(Vector3.right, hitinfo.normal);

            result = Vector3.Cross(Vector3.forward, hitinfo.normal) * dot;
        }

        return result;
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
