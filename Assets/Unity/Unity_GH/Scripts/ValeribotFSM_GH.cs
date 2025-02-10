using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;
using static UnityEngine.Rendering.DebugUI;
using static ValeribotPhase_GH;

public class ValeribotFSM_GH : MonoBehaviour
{
    //상태 enum
    public enum EValeribotState
    {
        IDLE,
        JUMP,
        TARGETLAGER,
        ROTATELAGER,
        GROUNDLAGER,
        MACHINELAGER,
        BOMBKICK,
        TAILATTACK,
        STAYDELAY,
        DAMAGE,
        DIE,
        START
    }


    public Animator dragonAni;

    public Animator dragonDieAni;

    public GameObject draModel;

    //현재 상태
    public EValeribotState currState;

    // 보스 행동 랜덤 변수
    int randBoss = 0;

    // 보스 페이즈
    public int bossPhase = 1;

    ValeribotPhase_GH valeribotPhase;

    public GameObject player;

    public float stateDelayTime = 3;

    //보스 쉴드
    Shield_GH shield;
    public bool onShield = true;

    //hp시스템
    HPSystem_GH valeriHP;

    //3페이즈 캐논 부시기
    bool phase3_Cannon_Break = false;

    float currTime;
    #region 점프 전역 변수
    //점프 포인트
    public Transform pointL, pointC, pointR, pointLC, pointRC;
    //점프 이전 포인트
    Vector3 previousPoint;
    //점프 다음 포인트
    Vector3 currentPoint;
    //점프 시간
    public float jumpDuration = 0.5f;
    public float jumpReadyTime = 1.5f;

    float jumpCurrTime = 0;

    float jumprotateValue = 0;

    Vector3 currPosition;

    bool jumpState = false;
    bool jumping = false;

    //보스가 있는 위치
    //보스가 지금 왼쪽 - 0, 가운데 - 1, 오른쪽 - 2에 있을 때 움직임
    public int currBossPosState = 1;

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

    public ParticleSystem[] laserBackEft;
    bool onLaserBack = false;
    bool laserBackOne = true;
    #endregion

    #region 타게팅 레이저 공격
    //레이저 발사 시간
    public float targetLaserReadyTime = 3;
    public float targetLaserDuraTime = 6;

    //레이저 방향
    Vector3 laserToPlayer;

    //레이저 발사
    bool onTargetingLaser = false;

    int targetLaserCount = 0;
    #endregion

    #region 회전 레이저
    public bool onRtateLaser = false;

    //레이저 발사 시간
    public float rotateLaserReadyTime = 2;
    public float rotateLaserDuraTime = 9;
    #endregion

    #region 땅 레이저
    bool onGroundLaser = false;

    public float groundLaserReadyTime = 2;
    public float groundLaserDuraTime = 6;

    public float groundLaserSpeed = 25;
    #endregion

    #region 레이저머신
    public GameObject laserMachineFactory;

    private GameObject[] laserMachines;

    bool onLaserMachine = false;

    public float machineLaserReadyTime = 2;
    public float machineLaserDuraTime = 5;

    public float laserMachineMoveTime = 1.5f;

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
    public float launchForce = 12f;
    // 발사 각도
    public float angle = 45f;

    public float bombTime = 5;

    #endregion

    #region 꼬리 치기
    TailCollider_GH tailSc;
    #endregion

    #region 공격력
    public float tailAttackValue = 50;
    public float laserAttackValue = 1f;
    #endregion

    void Start()
    {
        tailSc = GetComponentInChildren<TailCollider_GH>();

        valeribotPhase = GetComponent<ValeribotPhase_GH>();

        valeriHP = GetComponent<HPSystem_GH>();

        shield = GetComponentInChildren<Shield_GH>();

        transform.position = pointC.position;

        ChangeState(EValeribotState.START);

        //레이저 오브젝트 풀
        lasers = new GameObject[12];
        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i] = Instantiate(laserPrefab, firePoint.transform.position, firePoint.transform.rotation);
            lasers[i].transform.parent = transform;
            laserScript = lasers[i].GetComponent<LaserFire_GH>();
            lasers[i].SetActive(false);
        }

        //레이저머신 오브젝트 풀
        laserMachines = new GameObject[3];
        for (int i = 0; i < laserMachines.Length; i++)
        {
            laserMachines[i] = Instantiate(laserMachineFactory);
            laserMachines[i].transform.position = transform.position;
            laserMachines[i].SetActive(false);
        }
    }

    void Update()
    {
   

        if (onLaserBack)
        {
            if (laserBackOne)
            {
                for (int i = 0; i < laserBackEft.Length; i++)
                {
                    laserBackEft[i].Play();
                }
                //laserBackOne = true;
            }
        }
        else
        {
            if (!laserBackOne)
            {
                for (int i = 0; i < laserBackEft.Length; i++)
                {
                    laserBackEft[i].Stop();
                }
                //laserBackOne = false;

            }

        }

        OnShield();
        Damaged();


        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeState(EValeribotState.DIE);
        }


        switch (currState)
        {
            case EValeribotState.IDLE:
                UpdateIdle();
                break;
            case EValeribotState.JUMP:
                BossJumpMove();
                break;
            case EValeribotState.TARGETLAGER:
                TargetingLaser();
                break;
            case EValeribotState.ROTATELAGER:
                RotateLaser();
                break;
            case EValeribotState.GROUNDLAGER:
                GroundLaser();
                break;
            case EValeribotState.MACHINELAGER:
                LaserMachine();
                break;
            case EValeribotState.BOMBKICK:
                break;
            case EValeribotState.TAILATTACK:

                break;
            case EValeribotState.STAYDELAY:
                StateDelay();
                break;
            case EValeribotState.DAMAGE:
                break;
            case EValeribotState.DIE:
                break;
            case EValeribotState.START:
                break;
        }

    }
    //상태가 전환 될 때 한 번만 실행하는 동작
    public void ChangeState(EValeribotState state)
    {
        HPPhase();

        print(currState + "---->" + state);

        // 현재 상태를 state 값으로 설정
        currState = state;

        // 현재시간을 초기화
        currTime = 0;

        //레이저 머신 시간을 초기화
        laserMachineMoveCurrTime = 0;

        //레이저 시간을 초기화
        laserCurrTime = 0;


        onReadyLaser = true;

        firePoint.transform.localEulerAngles = new Vector3(0, 0, 0);


        switch (currState)
        {
            case EValeribotState.IDLE:
                //보스 움직임 순서대로
                /*
                if (randBoss < 6)
                {
                    randBoss++;
                }
                else
                {
                    randBoss = 0;
                }
                */


                // 보스 움직임 랜덤으로
                randBoss = Random.Range(1, 7);
                break;
            case EValeribotState.JUMP:
                jumpState = true;
                jumping = true;
                if (currBossPosState == 1)
                    RandMove = Random.Range(0, 2);

                dragonAni.SetTrigger("Jump");
                //chicken.ControllChicken();
                break;
            case EValeribotState.TARGETLAGER:
                targetLaserCount = 0;
                if (bossPhase < 3)
                {
                    dragonAni.SetTrigger("LaTarget");

                    onTargetingLaser = true;
                    lasers[0].transform.position = firePoint.transform.position;
                    lasers[0].SetActive(true);
                }
                else
                {
                    ChangeState(EValeribotState.IDLE);
                }
                break;
            case EValeribotState.ROTATELAGER:
                {
                    if (currBossPosState == 1)
                    {
                        dragonAni.SetTrigger("LaRotate");

                        for (int i = 0; i < bossPhase + 2; i++)
                        {
                            lasers[i].transform.position = firePoint.transform.position;
                            lasers[i].SetActive(true);
                        }

                        onRtateLaser = true;

                    }
                    else
                    {
                        ChangeState(EValeribotState.IDLE);
                    }
                }
                break;
            case EValeribotState.GROUNDLAGER:
                if (currBossPosState == 0)
                {
                    dragonAni.SetTrigger("LaGround");

                    firePoint.transform.localEulerAngles = new Vector3(0, 0, -30);
                    lasers[0].transform.position = firePoint.transform.position;
                    lasers[0].SetActive(true);

                    onReadyLaser = true;
                    onGroundLaser = true;
                }
                else if (currBossPosState == 2)
                {
                    dragonAni.SetTrigger("LaGround");

                    firePoint.transform.localEulerAngles = new Vector3(0, 0, 30);
                    lasers[0].transform.position = firePoint.transform.position;
                    lasers[0].SetActive(true);

                    onReadyLaser = true;
                    onGroundLaser = true;
                }
                else
                {
                    ChangeState(EValeribotState.IDLE);
                }


                break;
            case EValeribotState.MACHINELAGER:
                if (currBossPosState == 1 && bossPhase > 1)
                {
                    dragonAni.SetTrigger("Machine");

                    laserMachines[0].transform.position = transform.position + Vector3.up * 2;
                    laserMachines[1].transform.position = transform.position + Vector3.up * 2;

                    laserMachines[0].SetActive(true);
                    laserMachines[1].SetActive(true);
                    if (bossPhase > 2)
                    {

                        laserMachines[2].transform.position = transform.position + Vector3.up * 2;
                        laserMachines[2].SetActive(true);
                    }
                    onLaserMachine = true;
                }
                else
                {
                    ChangeState(EValeribotState.IDLE);
                }
                break;
            case EValeribotState.BOMBKICK:
                if (bossPhase < 3)
                {
                    ChangeState(EValeribotState.IDLE);
                }
                else
                {
                    dragonAni.SetTrigger("TailAttack");
                    BombShoot();
                }
                break;

            case EValeribotState.TAILATTACK:
                dragonAni.SetTrigger("TailAttack");
                ChangeState(EValeribotState.STAYDELAY);
                break;
            case EValeribotState.DIE:
                dragonAni.SetTrigger("Die");
                dragonDieAni.SetTrigger("Die");
                laserMachines[0].SetActive(false);
                laserMachines[1].SetActive(false);
                laserMachines[2].SetActive(false);
                for (int i = 0; i < 12; i++)
                {
                    lasers[i].GetComponent<LaserFire_GH>().LaserDone();

                }

                break;
            case EValeribotState.START:
                dragonAni.SetTrigger("Start");
                StartCoroutine(StartAni());

                break;
        }
    }

    void StateDelay()
    {
        currTime += Time.deltaTime;

        if (currTime > stateDelayTime)
        {
            if (tailSc.tailFind)
            {
                ChangeState(EValeribotState.TAILATTACK);

            }
            else
            {
                ChangeState(EValeribotState.IDLE);
            }

        }
    }


    void UpdateIdle()
    {
        ChangeState((EValeribotState)randBoss);
    }

    void BossJumpMove()
    {
        if (jumpState)
        {
            currTime += Time.deltaTime;

            previousPoint = pointC.position;

            if (currTime < jumpReadyTime)
            {
                //보스가 지금 왼쪽 - 0, 가운데 - 1, 오른쪽 - 2에 있을 때 움직임
                switch (currBossPosState)
                {
                    case 0:
                        if (jumprotateValue <= 70f)
                        {
                            jumprotateValue += Time.deltaTime * 80f;
                            draModel.transform.localEulerAngles = new Vector3(0, -jumprotateValue, 0);
                        }
                        break;
                    case 1:
                        if (RandMove == 0)
                        {
                            if (jumprotateValue <= 70f)
                            {
                                jumprotateValue += Time.deltaTime * 80f;
                                draModel.transform.localEulerAngles = new Vector3(0, jumprotateValue, 0);
                            }
                        }
                        else
                        {
                            if (jumprotateValue <= 70f)
                            {
                                jumprotateValue += Time.deltaTime * 80f;
                                draModel.transform.localEulerAngles = new Vector3(0, -jumprotateValue, 0);
                            }
                        }
                        break;

                    case 2:
                        if (jumprotateValue <= 70f)
                        {
                            jumprotateValue += Time.deltaTime * 80f;
                            draModel.transform.localEulerAngles = new Vector3(0, jumprotateValue, 0);
                        }
                        break;
                }

            }

            if (currTime > jumpReadyTime)
            {
                if (jumping)
                {
                    jumpCurrTime += Time.deltaTime;

                    //보스가 지금 왼쪽 - 0, 가운데 - 1, 오른쪽 - 2에 있을 때 움직임
                    switch (currBossPosState)
                    {
                        case 0:
                            currentPoint = BezierPoint(jumpCurrTime / jumpDuration, pointL.position, pointLC.position, pointC.position);
                            break;
                        case 1:
                            if (RandMove == 0)
                            {
                                currentPoint = BezierPoint(jumpCurrTime / jumpDuration, pointC.position, pointLC.position, pointL.position);
                            }
                            else
                            {
                                currentPoint = BezierPoint(jumpCurrTime / jumpDuration, pointC.position, pointRC.position, pointR.position);
                            }
                            break;

                        case 2:
                            currentPoint = BezierPoint(jumpCurrTime / jumpDuration, pointR.position, pointRC.position, pointC.position);
                            break;
                    }

                    previousPoint = currentPoint;
                    transform.position = currentPoint;

                    if (jumpCurrTime > jumpDuration)
                    {
                        jumping = false;

                    }
                }

                if (currTime > 3.8)
                {
                    switch (currBossPosState)
                    {
                        case 0:
                            if (jumprotateValue >= 0f)
                            {
                                jumprotateValue -= Time.deltaTime * 100f;
                                draModel.transform.localEulerAngles = new Vector3(0, jumprotateValue, 0);
                            }
                            break;
                        case 1:
                            if (RandMove == 0)
                            {
                                if (jumprotateValue >= 0f)
                                {
                                    jumprotateValue -= Time.deltaTime * 100f;
                                    draModel.transform.localEulerAngles = new Vector3(0, -jumprotateValue, 0);
                                }
                            }
                            else
                            {
                                if (jumprotateValue >= 0f)
                                {
                                    jumprotateValue -= Time.deltaTime * 100f;
                                    draModel.transform.localEulerAngles = new Vector3(0, jumprotateValue, 0);
                                }
                            }
                            break;

                        case 2:
                            if (jumprotateValue >= 0f)
                            {
                                jumprotateValue -= Time.deltaTime * 100f;
                                draModel.transform.localEulerAngles = new Vector3(0, -jumprotateValue, 0);
                            }
                            break;
                    }
                }

                if (currTime > 5)
                {

                    ChangeState(EValeribotState.STAYDELAY);
                    jumpState = false;
                    jumpCurrTime = 0;
                }
                if (transform.position == pointC.position)
                {
                    currBossPosState = 1;
                }
                else if (transform.position == pointL.position)
                {
                    currBossPosState = 0;
                }
                else if (transform.position == pointR.position)
                {
                    currBossPosState = 2;
                }
            }
        }


    }
    Vector3 BezierPoint(float t, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 p1p2 = Vector3.Lerp(p1, p2, t);
        Vector3 p2p3 = Vector3.Lerp(p2, p3, t);
        return Vector3.Lerp(p1p2, p2p3, t);
    }

    void TargetingLaser()
    {
        if (onTargetingLaser)
        {
            laserCurrTime += Time.deltaTime;
            if (laserCurrTime < targetLaserReadyTime)
            {
                laserToPlayer = (player.transform.position + Vector3.up) - firePoint.transform.position;
                lasers[0].transform.forward = laserToPlayer;

            }
            else if (laserCurrTime >= targetLaserReadyTime && laserCurrTime < targetLaserDuraTime)
            {
                onReadyLaser = false;
                onLaserBack = true;
            }
            else if (laserCurrTime >= targetLaserDuraTime)
            {
                onLaserBack = false;
                if (bossPhase == 1)
                {
                    lasers[0].GetComponent<LaserFire_GH>().LaserDone();
                    ChangeState(EValeribotState.STAYDELAY);

                    onTargetingLaser = false;
                }
                else if (bossPhase == 2 && targetLaserCount < 1)
                {
                    dragonAni.SetTrigger("LaTarget");
                    laserCurrTime = 0;
                    onReadyLaser = true;
                    targetLaserCount++;
                }
                else if (bossPhase == 2 && targetLaserCount >= 1)
                {
                    lasers[0].GetComponent<LaserFire_GH>().LaserDone();
                    ChangeState(EValeribotState.STAYDELAY);

                    onTargetingLaser = false;
                }
            }
        }
    }

    void RotateLaser()
    {
        print(Quaternion.Euler(0, 0, 90) * Vector3.right);
        if (onRtateLaser)
        {
            laserCurrTime += Time.deltaTime;
            for (int i = 0; i < bossPhase + 2; i++)
            {
                lasers[i].transform.position = firePoint.transform.position;
                lasers[i].transform.forward = Quaternion.Euler(0, 0, 0 + i * (360 / (bossPhase + 1))) * firePoint.transform.right;

            }

            if (laserCurrTime >= rotateLaserReadyTime && laserCurrTime < rotateLaserDuraTime)
            {
                firePoint.transform.localEulerAngles += new Vector3(0, 0, 1) * 25 * Time.deltaTime;
                onReadyLaser = false;
                onLaserBack = true;
            }
            else if (laserCurrTime >= rotateLaserDuraTime)
            {
                onLaserBack = false;
                for (int i = 0; i < bossPhase + 2; i++)
                {
                    if (lasers[i].activeInHierarchy)
                        lasers[i].GetComponent<LaserFire_GH>().LaserDone();
                }



                ChangeState(EValeribotState.STAYDELAY);
                onRtateLaser = false;

            }
        }

    }

    void GroundLaser()
    {
        if (onGroundLaser)
        {
            laserCurrTime += Time.deltaTime;
            lasers[0].transform.forward = -firePoint.transform.up;
            //if (laserCurrTime < groundLaserReadyTime)
            //{
            //    onReadyLaser = true;

            //}
            if (laserCurrTime >= groundLaserReadyTime && laserCurrTime < groundLaserDuraTime)
            {
                if (currBossPosState == 0)
                {
                    firePoint.transform.localEulerAngles += new Vector3(0, 0, 1) * groundLaserSpeed * Time.deltaTime;

                }

                else if (currBossPosState == 2)
                {

                    firePoint.transform.localEulerAngles -= new Vector3(0, 0, 1) * groundLaserSpeed * Time.deltaTime;

                }
                onReadyLaser = false;
                onLaserBack = true;

            }
            else if (laserCurrTime >= groundLaserDuraTime)
            {
                onLaserBack = false;
                lasers[0].GetComponent<LaserFire_GH>().LaserDone();

                ChangeState(EValeribotState.STAYDELAY);

                onGroundLaser = false;
            }
        }

    }

    void LaserMachine()
    {
        if (onLaserMachine)
        {
            laserMachineMoveCurrTime += Time.deltaTime;

            if (laserMachineMoveCurrTime < laserMachineMoveTime)
            {
                if (bossPhase < 3)
                {
                    laserMachines[0].transform.position += (Vector3.down + (Vector3.left * 3)) * Time.deltaTime;
                    laserMachines[1].transform.position += (Vector3.down + (Vector3.right * 3)) * Time.deltaTime;

                }
                else
                {
                    laserMachines[0].transform.position += ((Vector3.down * 2) + (Vector3.left * 3)) * Time.deltaTime;
                    laserMachines[1].transform.position += ((Vector3.down * 2) + (Vector3.right * 3)) * Time.deltaTime;
                    laserMachines[2].transform.position += (Vector3.up * 3) * Time.deltaTime;

                }
            }
            else
            {
                laserCurrTime += Time.deltaTime;
                if (bossPhase < 3)
                {
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
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        lasers[i].transform.position = laserMachines[0].transform.position;
                        laserMachines[0].transform.localRotation = Quaternion.Euler(0, 0, 0 + (i * 90));
                        lasers[i].transform.forward = laserMachines[0].transform.up;
                    }
                    for (int i = 4; i < 8; i++)
                    {
                        lasers[i].transform.position = laserMachines[1].transform.position;
                        laserMachines[1].transform.localRotation = Quaternion.Euler(0, 0, 0 + (i * 90));
                        lasers[i].transform.forward = laserMachines[1].transform.up;
                    }
                    for (int i = 8; i < 12; i++)
                    {
                        lasers[i].transform.position = laserMachines[2].transform.position;
                        laserMachines[2].transform.localRotation = Quaternion.Euler(0, 0, 45 + (i * 90));
                        lasers[i].transform.forward = laserMachines[2].transform.up;
                    }
                }
                if (laserCurrTime < machineLaserReadyTime)
                {
                    if (bossPhase < 3)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            lasers[i].SetActive(true);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            lasers[i].SetActive(true);
                        }
                    }


                    onReadyLaser = true;

                }
                else if (laserCurrTime >= machineLaserReadyTime && laserCurrTime < machineLaserDuraTime - 1.5f)
                {

                    onReadyLaser = false;

                    onLaserBack = true;
                }
                else if (laserCurrTime < machineLaserDuraTime && laserCurrTime >= machineLaserDuraTime - 1.5f)
                {
                    onLaserBack = false;
                    if (bossPhase < 3)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            lasers[i].GetComponent<LaserFire_GH>().LaserDone();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            lasers[i].GetComponent<LaserFire_GH>().LaserDone();

                        }
                    }
                    if (bossPhase < 3)
                    {
                        laserMachines[0].transform.position -= (Vector3.down + (Vector3.left * 3)) * Time.deltaTime;
                        laserMachines[1].transform.position -= (Vector3.down + (Vector3.right * 3)) * Time.deltaTime;

                    }
                    else
                    {
                        laserMachines[0].transform.position -= ((Vector3.down * 2) + (Vector3.left * 3)) * Time.deltaTime;
                        laserMachines[1].transform.position -= ((Vector3.down * 2) + (Vector3.right * 3)) * Time.deltaTime;
                        laserMachines[2].transform.position -= (Vector3.up * 3) * Time.deltaTime;

                    }
                }
                else
                {

                    laserMachines[0].SetActive(false);
                    laserMachines[1].SetActive(false);
                    if (bossPhase > 2)
                    {
                        laserMachines[2].SetActive(false);

                    }


                    ChangeState(EValeribotState.STAYDELAY);

                    onLaserMachine = false;

                }
            }
        }

    }

    void BombShoot()
    {
        if (currBossPosState == 0)
        {
            bomb = Instantiate(bombFactory, bombPoints[1].position, bombPoints[1].rotation);
            //포탄 생성

            ////포탄의 Rigidbody
            //Rigidbody rb = bomb.GetComponent<Rigidbody>();

            ////발사각도 라디안으로 변경
            //float radianAngle = angle * Mathf.Deg2Rad;

            ////발사 백터 계산
            //Vector3 launchDirection = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0);

            ////발사 힘 적용
            //rb.velocity = launchDirection * launchForce;

            //ChangeState(EValeribotState.STAYDELAY);

            StartCoroutine(BoobKickA());


        }
        else if (currBossPosState == 2)
        {
            bomb = Instantiate(bombFactory, bombPoints[0].position, bombPoints[0].rotation);
            //포탄 생성

            ////포탄의 Rigidbody
            //Rigidbody rb = bomb.GetComponent<Rigidbody>();

            ////발사각도 라디안으로 변경
            //float radianAngle = angle * Mathf.Deg2Rad;

            ////발사 백터 계산
            //Vector3 launchDirection = new Vector3(-Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0);

            ////발사 힘 적용
            //rb.velocity = launchDirection * launchForce;

            //ChangeState(EValeribotState.STAYDELAY);

            StartCoroutine(BoobKickB());

        }
        else
        {
            ChangeState(EValeribotState.IDLE);
        }

        //if (bomb != null)
        //{
        //    Destroy(bomb, bombTime);
        //}

    }

    void OnShield()
    {
        if (onShield)
        {
            shield.gameObject.SetActive(true);
        }
        else
        {
            shield.gameObject.SetActive(false);
        }
    }

    void Damaged()
    {
        if (valeriHP.currHP <= 0 && currState != EValeribotState.DIE)
        {
            ChangeState(EValeribotState.DIE);
        }

        if (Input.GetKeyDown(KeyCode.M) && !onShield)
        {
            valeriHP.UpdateHP(-30);
        }
    }

    void HPPhase()
    {
        if (valeriHP.currHP <= valeriHP.maxHP * 0.75 && bossPhase == 1)
        {
            valeribotPhase.ChangeState(EValeribotPhase.PHASE_2);
        }
        else if (valeriHP.currHP <= valeriHP.maxHP * 0.5 && bossPhase == 2)
        {
            valeribotPhase.ChangeState(EValeribotPhase.PHASE_3);
        }
    }

   

    IEnumerator StartAni()
    {
        yield return new WaitForSeconds(5.7f);
        ChangeState(EValeribotState.IDLE);

    }

    IEnumerator BoobKickA()
    {
        yield return new WaitForSeconds(1.5f);
        //포탄의 Rigidbody
        Rigidbody rb = bomb.GetComponent<Rigidbody>();

        //발사각도 라디안으로 변경
        float radianAngle = angle * Mathf.Deg2Rad;

        //발사 백터 계산
        Vector3 launchDirection = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0);

        //발사 힘 적용
        rb.velocity = launchDirection * launchForce;

        ChangeState(EValeribotState.STAYDELAY);

    }
    IEnumerator BoobKickB()
    {
        yield return new WaitForSeconds(1.5f);
        //포탄의 Rigidbody
        Rigidbody rb = bomb.GetComponent<Rigidbody>();

        //발사각도 라디안으로 변경
        float radianAngle = angle * Mathf.Deg2Rad;

        //발사 백터 계산
        Vector3 launchDirection = new Vector3(-Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0);

        //발사 힘 적용
        rb.velocity = launchDirection * launchForce;

        ChangeState(EValeribotState.STAYDELAY);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow") && !onShield)
        {
            SoundManager.instance.PlayBossEftSound(SoundManager.EBossEftType.BOSS_HIT1);

            //todo
            valeriHP.UpdateHP(-150);
        }
    }
}