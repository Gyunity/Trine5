using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class ValeribotFSM_GH : MonoBehaviour
{
    public GameObject player;

    //닭 점프 스크립트
    Chicken_GH chicken;

    #region 점프 전역 변수
    //점프 포인트
    public Transform pointL, pointC, pointR, pointLC, pointRC;
    //점프 이전 포인트
    Vector3 previousPoint;
    //점프 다음 포인트
    Vector3 currentPoint;
    //점프 시간
    public float jumpDuration = 2;

    float currTime;

    Vector3 currPosition;

    public bool jumpState = false;

    //보스가 있는 위치
    //보스가 지금 왼쪽 - 0, 가운데 - 1, 오른쪽 - 2에 있을 때 움직임
    int currBossPisState = 1;

    int RandMove = 0;
    #endregion

    #region 타게팅 레이저 공격
    //레이저 발사 위치
    public Transform layserPos;
    //레이저 방향
    Vector3 layserToPlayer;
    //레이저 길이
    float layserToPlayerDist;
    //레이저 정보
    RaycastHit hitInfo;
    //레이저 공장
    public GameObject layserFactory;

    float layserCurrTime = 0;
    float layserReadyTime = 3;
    float layserDuraTime = 6;

    GameObject layser;
    #endregion

    void Start()
    {
        chicken = GetComponent<Chicken_GH>();
        transform.position = pointC.position;
    }

    void Update()
    {

        BossJumpMove();
        TargetLayser();
    }

    void BossJumpMove()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            jumpState = true;
            chicken.ControllChicken();

        }

        if (jumpState)
        {
            currTime += Time.deltaTime;


            previousPoint = pointC.position;

            //보스가 지금 왼쪽 - 0, 가운데 - 1, 오른쪽 - 2에 있을 때 움직임
            switch (currBossPisState)
            {
                case 0:
                    currentPoint = CalculateBezierPoint(currTime / jumpDuration, pointL.position, pointLC.position, pointC.position);
                    break;
                case 1:
                    if (RandMove == 0)
                    {
                        currentPoint = CalculateBezierPoint(currTime / jumpDuration, pointC.position, pointLC.position, pointL.position);
                    }
                    else
                    {
                        currentPoint = CalculateBezierPoint(currTime / jumpDuration, pointC.position, pointRC.position, pointR.position);
                    }
                    break;

                case 2:
                    currentPoint = CalculateBezierPoint(currTime / jumpDuration, pointR.position, pointRC.position, pointC.position);
                    break;
            }
            previousPoint = currentPoint;
            transform.position = currentPoint;

            if (currTime > jumpDuration)
            {
                RandMove = Random.Range(0, 2);
                jumpState = false;
                currTime = 0;
            }

            if (transform.position == pointC.position)
            {
                currBossPisState = 1;
            }
            else if (transform.position == pointL.position)
            {
                currBossPisState = 0;
            }
            else if (transform.position == pointR.position)
            {
                currBossPisState = 2;
            }
        }


    }

    /*
    private void OnDrawGizmos()
    {
        if (pointC != null && pointL != null && pointLC != null && Time.deltaTime != 0)
        {
            Vector3 previousPoint = pointC.position;

            for (float t = 0; t <= jumpDuration; t += Time.deltaTime)
            {
                Vector3 currentPoint = CalculateBezierPoint(t / jumpDuration, pointC.position, pointLC.position, pointL.position);
                Gizmos.DrawLine(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }
        }
    }
    */

    Vector3 CalculateBezierPoint(float t, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 p1p2 = Vector3.Lerp(p1, p2, t);
        Vector3 p2p3 = Vector3.Lerp(p2, p3, t);
        return Vector3.Lerp(p1p2, p2p3, t);
    }



    void TargetLayser()
    {


        layserToPlayer = player.transform.position - layserPos.position;
        //layserToPlayer = hitInfo.point - layserPos.position;
        Ray ray = new Ray(layserPos.position, layserToPlayer);

        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, LayerMask.NameToLayer("Player")))
        {
            layserToPlayerDist = layserToPlayer.magnitude;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            layser = Instantiate(layserFactory);

        }
        if (layser != null)
        {
            layserCurrTime += Time.deltaTime;
            if (layserCurrTime < layserDuraTime)
            {
                if (layserCurrTime < layserReadyTime)
                {
                    layser.transform.position = layserPos.position;
                    layser.transform.localScale = new Vector3(1, layserToPlayerDist, 1);
                    layser.transform.up = -layserToPlayer;
                }
                
            }
            else
            {
                layserCurrTime = 0;
                Destroy(layser.gameObject);
            }
            print(layserCurrTime);
        }
    }
}