using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerState_HMJ;

public class PlayerMove_HMJ : MonoBehaviour
{
    // �̵� �ӷ�
    float moveSpeed = 4.0f;
    float playDashTime = 0.0f;
    // �뽬 �ִ� �ӷ�
    float dashMaxSpeed = 20.0f;
    float dashTime = 0.3f;

    // Character Controller
    public CharacterController cc;

    // �����Ŀ�
    public float jumpPower = 2.0f;

    // �߷�
    float gravity = -9.81f;

    // y ���� �ӷ�
    float yVelocity;

    // ���� �ִ� Ƚ��
    public int maxJumpN = 2;

    // ���� Ƚ��
    int JumpCurN = 0;

    // �뽬 ����
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

        // ���� ũ�Ⱑ 0���� ũ��
        if(movement.magnitude > 0)
        {
            // �̵� �������� ĳ���� ȸ��
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
            // 0 ~ 1 -> �뽬 �÷��� �ð� / �뽬 �ð�
            moveSpeed = Mathf.Lerp(moveSpeed, dashMaxSpeed, playDashTime / dashTime);
            // moveSpeed -> dashMaxSpeed�� �� ����
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
        // ���� ����
        if (cc.isGrounded)
        {
            JumpCurN = 0;
            playerState.SetState(PlayerState.Idle);
        }
        if (Input.GetButtonDown("Jump"))
        {
            playerState.SetState(PlayerState.Jump);
            Debug.Log("���� ��ư ����.");
            // ���࿡ ���� ���� Ƚ���� �ִ� ���� Ƚ������ ������
            if (JumpCurN < maxJumpN)
            {
                yVelocity = jumpPower;
                JumpCurN++;
            }
        }

        yVelocity += gravity * Time.deltaTime;

        // dir.y ���� yVelocity�� ����
        movement.y += yVelocity;
    }
    void PlayerMove()
    {
        if (playerState.GetState() == PlayerState.Grap) // ���𰡸� ��� ������
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
