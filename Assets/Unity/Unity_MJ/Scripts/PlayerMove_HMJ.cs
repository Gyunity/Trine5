using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
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

        // �����̽� �ٸ� ������
        if (Input.GetKeyDown(KeyCode.A)) // ����
            bRight = false;

        if (Input.GetKeyDown(KeyCode.D)) // ������
            bRight = true;

        // �����̽� �ٸ� ������
        if (Input.GetButtonDown("Jump"))
        {
            // ���࿡ ���� ���� Ƚ���� �ִ� ���� Ƚ������ ������
            if (JumpCurN < maxJumpN)
            {
                yVelocity = jumpPower;
                JumpCurN++;
            }
        }
        // yVelocity�� �߷°��� �̿��ؼ� ���ҽ�Ų��.
        // v = v0 + at;
        yVelocity += gravity * Time.deltaTime;

        // dir.y ���� yVelocity�� ����
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

}
