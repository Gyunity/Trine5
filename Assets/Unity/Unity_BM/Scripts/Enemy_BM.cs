using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnemyMove;

public class Enemy_BM : MonoBehaviour
{
    
    public enum EEnemyState_BM
    {
        Idle,
        Jump,
        Attack,
        Throw,
        Damage,
        Die,
        AttackDelay
    }
    public EEnemyState_BM currState;

    //플레이어 게임 오브젝트
    GameObject player;

    //인지범위
    public float traceRange = 20;
    //공격범위
    public float attackRange = 2;
    //장거리 공격 범위
    public float longAttackRange = 8;

    //현재시간
    float currTime;
    //공격 딜레이
    public float attackDelayTime = 2;

    Animator anim;
    HpSystem_BM hpSystem;

    BallFactory ballfactory;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        hpSystem = GetComponent<HpSystem_BM>();
        ballfactory = GetComponent<BallFactory>();
        currState = EEnemyState_BM.Idle;
    }

    void Update()
    {
        switch (currState)
        {
            case EEnemyState_BM.Idle:
                UpdateIdle();
                break;
            case EEnemyState_BM.Jump:
                UpdateJump();
                break;
            case EEnemyState_BM.Attack:
                UpdateAttack();
                break;
            case EEnemyState_BM.Throw:
                UpdateThrow();
                break;
            case EEnemyState_BM.Damage:
                UpdateDamage();
                break;
            case EEnemyState_BM.Die:
                //UpdateDie();
                break;
            case EEnemyState_BM.AttackDelay:
                UpdateAttackDelay();
                break;
        }
    }

    public void ChangeState(EEnemyState_BM state)
    {
        print(currState + "-->" + state);
        currState = state;
        currTime = 0;
        switch (currState)
        {
            case EEnemyState_BM.Idle:
                anim.SetTrigger("Idle");
                break;
            case EEnemyState_BM.Jump:
                //양배추 팹토리
                anim.SetTrigger("Jump");
                break;
            case EEnemyState_BM.Attack:
                anim.SetTrigger("Attack");
                break;
            case EEnemyState_BM.Throw:
                //양배추 프리팹 등장
                anim.SetTrigger("Throw");
                break;
            case EEnemyState_BM.Damage:
                hpSystem.UpdateHp(-1);
                anim.SetTrigger("Damage");
                break;
            case EEnemyState_BM.Die:
                CapsuleCollider coll = GetComponent<CapsuleCollider>();
                coll.enabled = false;
                anim.SetTrigger("Die");
                break;
        }
    }
    void DecideStateByDist()
    {
        // 만약에 Player 와 거리가 attakRange 보다 작으면
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < attackRange)
        {
            ChangeState(EEnemyState_BM.Attack);
        }
        // 그렇지 않고 인지범위 보다 작으면
        else if (dist > attackRange && dist < traceRange)
        {

            ChangeState(EEnemyState_BM.Throw);
        }
        // 그렇지 않고 인지범위 보다 크면
        else
        {
            // 대기상태로 전환
            ChangeState(EEnemyState_BM.Idle);
        }
    }
    //대기 상태일때
    void UpdateIdle()
    {
        DecideStateByDist();
    }
    //점프상태 일때
    void UpdateJump()
    {
        //10초에 1번씩 점프,양배추 굴러가기
        ChangeState(EEnemyState_BM.AttackDelay);
    }
    void UpdateAttackDelay()
    {
        //공격 Delay시간 만큼
        if (IsDelayComplete(attackDelayTime))
        {
            DecideStateByDist();
        }
    }

    
    //어택상태일때
    void UpdateAttack()
    {
        //공격애니
        anim.SetTrigger("Attack");
        ChangeState(EEnemyState_BM.AttackDelay);

    }
    void RealAttack()
    {
        // 플레이어를 공격하자.
        print("공격! 공격!");
        // 플레이어 HP 줄이자.
        hpSystem.UpdateHp(-2);

    }
    //던지는 상태일때
    void UpdateThrow()
    {
        //던지는 애니
        anim.SetTrigger("Throw");
        ChangeState(EEnemyState_BM.AttackDelay);
    }
    //뎀지 상태 전환
    
    private float damageDelay = 1;
    //뎀지 상태일때
    void UpdateDamage()
    {
        if(IsDelayComplete(damageDelay))
        {
            DecideStateByDist();
        }
    }
    public void OnDamaged()
    {
        //뎀지 상태 전환
        ChangeState(EEnemyState_BM.Damage);
    }
    //죽음 상태일때
    void UpdateDie()
    {
       
    }
    bool IsDelayComplete(float delayTime)
    {
        currTime += Time.deltaTime;
        if(currTime >= delayTime)
        {
            currTime = 0;
            return true;
        }
        return false;
    }
    void ThrowBall()
    {
        ballfactory.BallGo();
        ChangeState(EEnemyState_BM.AttackDelay);
    }

}
