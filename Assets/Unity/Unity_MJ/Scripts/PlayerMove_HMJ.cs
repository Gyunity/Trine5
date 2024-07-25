using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float jumpPower = 3;

    // �߷�
    float gravity = -9.81f;

    // y ���� �ӷ�
    float yVelocity;

    // ���� �ִ� Ƚ��
    public int maxJumpN = 2;

    // ���� Ƚ��
    int JumpCurN = 0;

    // �뽬
    bool dash = false;

    // �뽬 ����
    Vector3 dashDir;

    Vector3 dirH;

    // bool bRight = false;

    Vector3 movement;

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
            dash = true;
        }
            
        if (dash)
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
            dash = false;
            moveSpeed = 4.0f;
        }
    }

    void Jump()
    {
        // ���� ����
        if (cc.isGrounded)
        {
            JumpCurN = 0;
        }
        if (Input.GetButtonDown("Jump"))
        {
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
        cc.Move((movement * moveSpeed + new Vector3(0.0f, yVelocity, 0.0f)) * Time.fixedDeltaTime);
    }
    private void FixedUpdate()
    {
        PlayerMove();
    }

}
