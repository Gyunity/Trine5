using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 5;
    //캐릭터 컨트롤러
    public CharacterController cc;

    //점프파워
    public float jumpPower = 3;
    //중력
    float gravity = -9.61f;
    //y방향 속력
    float yvelocity;

    //최대 점프 횟수
    public int jumpMaxCnt = 2;
    //현재 점프 횟수
    int jumpCurrCnt;

    // Start is called before the first frame update
    void Start()
    {
        //캐릭터 컨트롤러 가져오기
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //방향
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;
        //dir 크기 1로 변경
        dir.Normalize();

        //dir에 곱하기(실제)
        //dir*=moveSpeed;

        //만약에 땅에 있다면 yVelocity 를 0으로 초기화
        if (cc.isGrounded)
        {
            yvelocity = 0;
            jumpCurrCnt = 0;
        }

        //만약에 스페이스바를 누르면 
        if (Input.GetButtonDown("Jump"))

        {
            //만약에 현재 점프 횟수가 최대 점프횟수 보다 적은면
            if (jumpCurrCnt < jumpMaxCnt)
            {
                //yvelocity 에 jumpPower를 셋팅
                yvelocity = jumpPower;
                //현재 점프 횟수 증가
                jumpCurrCnt++;
            }

        }
        //yvelocity 를 중력값을 이용해서 감소시킨다.
        //v=v0+at;
        yvelocity += gravity * Time.deltaTime;


        //dir.y. 값에 yvelocity셋팅
        dir.y = yvelocity;


        //이동
        //transform.Translate(dir*moveSpeed*Time.deltaTime);
        cc.Move(dir * moveSpeed * Time.deltaTime);

    }
}
