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

    #region 레이저 전역 변수
    //레이저 발사
    public GameObject firePoint;
    public float maxLength;
    public GameObject laserPrefab;

    private GameObject[] lasers;
    private LaserFire_GH laserScript;

    float laserCurrTime = 0;

    public bool onReadyLaser = true;
    #endregion

    #region 타게팅 레이저 공격
    //레이저 발사 시간
    float targetLaserReadyTime = 3;
    float targetLaserDuraTime = 6;

    //레이저 방향
    Vector3 laserToPlayer;

    //레이저 발사
    bool onTargetingLaser = false;
    #endregion

    #region 회전 레이저
    bool onRtateLaser = false;

    //레이저 발사 시간
    float rotateLaserReadyTime = 2;
    float rotateLaserDuraTime = 9;
    #endregion

    #region 땅 레이저
    bool onGroundLaser = false;
    #endregion

    #region 레이저머신
    public GameObject laserMachineFactory;

    private GameObject[] laserMachines;

    bool onLaserMachine = false;

    float laserMachineMoveTime = 1.5f;

    float laserMachineMoveCurrTime = 0;
    #endregion

    #region 폭탄발로차기
    //폭탄 쏘기
    // 폭탄 프리팹
    public GameObject bombFactory;

    GameObject bomb;
    // 폭탄의 발사 위치
    public Transform[] bombPoints;
    // 발사 힘
    public float launchForce = 10f;
    // 발사 각도
    public float angle = 45f;


    #endregion

    void Start()
    {
        chicken = GetComponent<Chicken_GH>();
        transform.position = pointC.position;

        lasers = new GameObject[8];

        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i] = Instantiate(laserPrefab, firePoint.transform.position, firePoint.transform.rotation);
            lasers[i].transform.parent = transform;
            laserScript = lasers[i].GetComponent<LaserFire_GH>();
            lasers[i].SetActive(false);
        }

        laserMachines = new GameObject[4];

        for (int i = 0; i < laserMachines.Length; i++)
        {
            laserMachines[i] = Instantiate(laserMachineFactory);
            laserMachines[i].transform.position = transform.position;
            laserMachines[i].SetActive(false);
        }


    }

    void Update()
    {
        TargetingLaser();
        RotateLaser();
        GroundLaser();
        BossJumpMove();
        LaserMachine();
        BombShoot();
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
    Vector3 CalculateBezierPoint(float t, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 p1p2 = Vector3.Lerp(p1, p2, t);
        Vector3 p2p3 = Vector3.Lerp(p2, p3, t);
        return Vector3.Lerp(p1p2, p2p3, t);
    }



    void TargetingLaser()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            laserCurrTime = 0;
            lasers[0].SetActive(true);

            onTargetingLaser = true;
        }
        if (onTargetingLaser)
        {
            laserCurrTime += Time.deltaTime;
            if (laserCurrTime < targetLaserReadyTime)
            {
                laserToPlayer = player.transform.position - firePoint.transform.position;
                lasers[0].transform.forward = laserToPlayer;
                onReadyLaser = true;

            }
            else if (laserCurrTime >= targetLaserReadyTime && laserCurrTime < targetLaserDuraTime)
            {
                onReadyLaser = false;

            }
            else if (laserCurrTime >= targetLaserDuraTime)
            {
                onReadyLaser = true;

                laserCurrTime = 0;
                onTargetingLaser = false;

                lasers[0].GetComponent<LaserFire_GH>().LaserDone();

            }
        }
    }


    void RotateLaser()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            firePoint.transform.localEulerAngles = new Vector3(0, 0, 0);
            laserCurrTime = 0;

            lasers[0].SetActive(true);
            lasers[1].SetActive(true);

            onRtateLaser = true;
        }
        if (onRtateLaser)
        {
            laserCurrTime += Time.deltaTime;
            lasers[0].transform.forward = firePoint.transform.right;
            lasers[1].transform.forward = -firePoint.transform.right;
            if (laserCurrTime < rotateLaserReadyTime)
            {
                onReadyLaser = true;

            }
            else if (laserCurrTime >= rotateLaserReadyTime && laserCurrTime < rotateLaserDuraTime)
            {
                firePoint.transform.localEulerAngles += new Vector3(0, 0, 1) * 25 * Time.deltaTime;
                onReadyLaser = false;


            }
            else if (laserCurrTime >= rotateLaserDuraTime)
            {
                lasers[0].GetComponent<LaserFire_GH>().LaserDone();
                lasers[1].GetComponent<LaserFire_GH>().LaserDone();


                onRtateLaser = false;

                onReadyLaser = true;

            }
        }
    }

    void GroundLaser()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            firePoint.transform.localEulerAngles = new Vector3(0, 0, 0);

            if (currBossPisState == 0)
            {
                firePoint.transform.localEulerAngles = new Vector3(0, 0, -30);
            }
            else if (currBossPisState == 2)
            {
                firePoint.transform.localEulerAngles = new Vector3(0, 0, 30);
            }
            else
            {
                return;
            }

            lasers[0].SetActive(true);

            laserCurrTime = 0;

            onGroundLaser = true;

            onReadyLaser = true;

        }

        if (onGroundLaser)
        {
            laserCurrTime += Time.deltaTime;
            lasers[0].transform.forward = -firePoint.transform.up;
            if (laserCurrTime < rotateLaserReadyTime)
            {
                onReadyLaser = true;

            }
            else if (laserCurrTime >= rotateLaserReadyTime && laserCurrTime < rotateLaserDuraTime)
            {
                if (currBossPisState == 0)
                {
                    firePoint.transform.localEulerAngles += new Vector3(0, 0, 1) * 15 * Time.deltaTime;

                }

                else if (currBossPisState == 2)
                {

                    firePoint.transform.localEulerAngles -= new Vector3(0, 0, 1) * 15 * Time.deltaTime;

                }
                onReadyLaser = false;


            }
            else if (laserCurrTime >= rotateLaserDuraTime)
            {
                onGroundLaser = false;
                laserCurrTime = 0;

                onReadyLaser = true;
                lasers[0].GetComponent<LaserFire_GH>().LaserDone();


            }
        }
    }

    void LaserMachine()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            laserCurrTime = 0;
            laserMachineMoveCurrTime = 0;
            onLaserMachine = true;
            laserMachines[0].transform.position = transform.position + Vector3.up * 2;
            laserMachines[1].transform.position = transform.position + Vector3.up * 2;

            laserMachines[0].SetActive(true);
            laserMachines[1].SetActive(true);
        }

        if (onLaserMachine)
        {
            laserMachineMoveCurrTime += Time.deltaTime;

            if (laserMachineMoveCurrTime < laserMachineMoveTime)
            {
                laserMachines[0].transform.position += (Vector3.down + (Vector3.left * 3)) * Time.deltaTime;
                laserMachines[1].transform.position += (Vector3.down + (Vector3.right * 3)) * Time.deltaTime;
            }
            else
            {
                laserCurrTime += Time.deltaTime;
                for (int i = 0; i < 4; i++)
                {
                    lasers[i].transform.position = laserMachines[0].transform.position;
                    laserMachines[0].transform.localRotation = Quaternion.Euler(0, 0, 45 + (i * 90));
                    lasers[i].transform.forward = laserMachines[0].transform.up;
                }
                for (int i = 4; i < 8; i++)
                {
                    lasers[i].transform.position = laserMachines[1].transform.position;
                    laserMachines[1].transform.localRotation = Quaternion.Euler(0, 0, 45 + (i * 90));
                    lasers[i].transform.forward = laserMachines[1].transform.up;
                }
                if (laserCurrTime < rotateLaserReadyTime)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        lasers[i].SetActive(true);
                    }
                    onReadyLaser = true;

                }
                else if (laserCurrTime >= rotateLaserReadyTime && laserCurrTime < rotateLaserDuraTime - 1.5f)
                {

                    onReadyLaser = false;


                }
                else if (laserCurrTime < rotateLaserDuraTime && laserCurrTime >= rotateLaserDuraTime - 1.5f)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        lasers[i].GetComponent<LaserFire_GH>().LaserDone();
                    }
                    laserMachines[0].transform.position -= (Vector3.down + (Vector3.left * 3)) * Time.deltaTime;
                    laserMachines[1].transform.position -= (Vector3.down + (Vector3.right * 3)) * Time.deltaTime;
                }
                else
                {
                    laserMachines[0].SetActive(false);
                    laserMachines[1].SetActive(false);

                    laserCurrTime = 0;
                    laserMachineMoveCurrTime = 0;
                    onReadyLaser = true;


                    onLaserMachine = false;

                }
            }
        }

    }

    void BombShoot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (currBossPisState == 0)
            {
                bomb = Instantiate(bombFactory, bombPoints[1].position, bombPoints[1].rotation);
                //포탄 생성

                //포탄의 Rigidbody
                Rigidbody rb = bomb.GetComponent<Rigidbody>();

                //발사각도 라디안으로 변경
                float radianAngle = angle * Mathf.Deg2Rad;

                //발사 백터 계산
                Vector3 launchDirection = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0);

                //발사 힘 적용
                rb.velocity = launchDirection * launchForce;

            }
            else
            {
                bomb = Instantiate(bombFactory, bombPoints[0].position, bombPoints[0].rotation);
                //포탄 생성

                //포탄의 Rigidbody
                Rigidbody rb = bomb.GetComponent<Rigidbody>();

                //발사각도 라디안으로 변경
                float radianAngle = angle * Mathf.Deg2Rad;

                //발사 백터 계산
                Vector3 launchDirection = new Vector3(-Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0);

                //발사 힘 적용
                rb.velocity = launchDirection * launchForce;
            }

        }
    }
}