using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �̵� �ӷ�
    float moveSpeed = 4.0f;
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
    }

    void GetInput()
    {

    }
}
