using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 5;
    //ĳ���� ��Ʈ�ѷ�
    public CharacterController cc;

    //�����Ŀ�
    public float jumpPower = 3;
    //�߷�
    float gravity = -9.61f;
    //y���� �ӷ�
    float yvelocity;

    //�ִ� ���� Ƚ��
    public int jumpMaxCnt = 2;
    //���� ���� Ƚ��
    int jumpCurrCnt;

    // Start is called before the first frame update
    void Start()
    {
        //ĳ���� ��Ʈ�ѷ� ��������
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //�Է�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //����
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;
        //dir ũ�� 1�� ����
        dir.Normalize();

        //dir�� ���ϱ�(����)
        //dir*=moveSpeed;

        //���࿡ ���� �ִٸ� yVelocity �� 0���� �ʱ�ȭ
        if (cc.isGrounded)
        {
            yvelocity = 0;
            jumpCurrCnt = 0;
        }

        //���࿡ �����̽��ٸ� ������ 
        if (Input.GetButtonDown("Jump"))

        {
            //���࿡ ���� ���� Ƚ���� �ִ� ����Ƚ�� ���� ������
            if (jumpCurrCnt < jumpMaxCnt)
            {
                //yvelocity �� jumpPower�� ����
                yvelocity = jumpPower;
                //���� ���� Ƚ�� ����
                jumpCurrCnt++;
            }

        }
        //yvelocity �� �߷°��� �̿��ؼ� ���ҽ�Ų��.
        //v=v0+at;
        yvelocity += gravity * Time.deltaTime;


        //dir.y. ���� yvelocity����
        dir.y = yvelocity;


        //�̵�
        //transform.Translate(dir*moveSpeed*Time.deltaTime);
        cc.Move(dir * moveSpeed * Time.deltaTime);

    }
}
