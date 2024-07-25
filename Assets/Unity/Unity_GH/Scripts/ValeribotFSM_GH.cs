using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValeribotFSM_GH : MonoBehaviour
{

    //점프 이동
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
    void Start()
    {
        transform.position = pointC.position;
    }

    void Update()
    {

        BossJumpMove();

    }

    void BossJumpMove()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            jumpState = true;
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
                        //currBossPisState = 0;
                    }
                    else
                    {
                        currentPoint = CalculateBezierPoint(currTime / jumpDuration, pointC.position, pointRC.position, pointR.position);
                        //currBossPisState = 2;
                    }
                    break;

                case 2:
                    currentPoint = CalculateBezierPoint(currTime / jumpDuration, pointR.position, pointRC.position, pointC.position);
                    //currBossPisState = 1;
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


    Vector3 CalculateBezierPoint(float t, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 p1p2 = Vector3.Lerp(p1, p2, t);
        Vector3 p2p3 = Vector3.Lerp(p2, p3, t);
        return Vector3.Lerp(p1p2, p2p3, t);
    }
}
