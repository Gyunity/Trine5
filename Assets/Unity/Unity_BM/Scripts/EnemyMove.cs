using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public enum EEnemyState
    {
        Idle,
        Stun,
        Sleep,
        Die
    }
    public EEnemyState currState;

    Animator anim;

    private bool canAttack = false;

    //BallFactory
    public Action GoBall;
    //FireBallFactory
    public Action FireGo;
    //BoxFactory
    public Action GoBox;
    //FirePilarFactory
    public Action GoPilar;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        HpSystem hpSystem = GetComponent<HpSystem>();
        hpSystem.onDie = OnDie;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case EEnemyState.Idle:
                UpdateIdle();
                break;

            case EEnemyState.Stun:
                UpdateStun();
                break;

            case EEnemyState.Sleep:
                UpdateSleep();
                break;

            case EEnemyState.Die:
                break;
        }
    }
    public void ChangeState(EEnemyState state)
    {
        print(currState + "-->" + state);
        currState = state;

        switch (currState)
        {
            case EEnemyState.Idle:
                anim.SetTrigger("Idle");
                break;
            case EEnemyState.Stun:
                anim.SetTrigger("Stun");
                break;
            case EEnemyState.Sleep:
                canAttack = true;
                break;

            case EEnemyState.Die:
                anim.SetTrigger("Die");
                break;
        }
    }
    void UpdateIdle()
    {
        //파이어볼 쏘고 전기공 굴리고
        //GoBall();
        //FireGo();

        //불기둥 만들고 박스 내리고


        //if(스턴당하면)
        //ChangeState(EEnemyState.Stun);
    }
    void UpdateStun()
    {
        //하는 거 없음 찌릿찌릿
        ChangeState(EEnemyState.Sleep);
    }
    void UpdateSleep()
    {
        //두들겨 맞음 
        //if 피가 남아 있으면 ChangeState(EEnemyState.Idle)
        // 피가 없으면 ChangeState(EEnemyState.Die)
    }
    void OnDie()
    {
        
    }
    public void OnDamaged()
    {
        HpSystem hpSystem = GetComponent<HpSystem>();
        hpSystem.UpdateHp(-1);
    }
}
