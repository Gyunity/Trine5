using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    public enum EEnemyState
    {
        Idle,
        Idle_2,
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

    HpSystem_BM hpSystem;

    int bossPhase = 1;

    float currtTime = 0;
    float dieTime = 5;

    public Image[] shiledPhase;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        hpSystem = GetComponent<HpSystem_BM>();
        //hpSystem.onDie = OnDie;
    }

    // Update is called once per frame
    void Update()
    {
        OnDamaged();

        switch (currState)
        {
            case EEnemyState.Idle:
                UpdateIdle();
                break;

            case EEnemyState.Idle_2:
                UpdateIdle_2();
                break;

            case EEnemyState.Stun:
                UpdateStun();
                break;

            case EEnemyState.Sleep:
                UpdateSleep();
                break;

            case EEnemyState.Die:
                currtTime += Time.deltaTime;
               
                    SceneManager.LoadScene("Valeribot_GH");
                
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
            case EEnemyState.Idle_2:
                anim.SetTrigger("Idle_2");
                break;
            case EEnemyState.Stun:
                anim.SetTrigger("Stun");
                break;
            case EEnemyState.Sleep:
                if(bossPhase == 1)
                {
                    shiledPhase[0].gameObject.SetActive(false);
                }
                else if (bossPhase == 2)
                {
                    shiledPhase[1].gameObject.SetActive(false);
                }
                else if (bossPhase == 3)
                {
                    shiledPhase[2].gameObject.SetActive(false);
                }
                else if (bossPhase == 4)
                {
                    shiledPhase[3].gameObject.SetActive(false);
                }
                //print("체인지 슬립");
                //anim.SetTrigger("Sleep");
                //anim.SetTrigger("Idle");
                //anim.SetTrigger("Die");

                break;

            case EEnemyState.Die:
                anim.SetTrigger("Die");
                currtTime = 0;
                break;
        }
    }

    float currentTime = 0;
    float attackTime = 5f;
    void UpdateIdle()
    {
        print("공볼");
        currentTime += Time.deltaTime;

        if (currentTime > attackTime)
        {
            GoBall();
            FireGo();
            currentTime = 0;
        }
    }

    //float currentTime = 0;
    float Pilar_attackTime = 10f;
    float Box_attackTime = 15f;
    void UpdateIdle_2()
    {
        currentTime += Time.deltaTime;

        if (currentTime > Pilar_attackTime)
        {
            currentTime = 0;

            GoPilar();   
        }
        else if (currentTime > Box_attackTime)
        {
            currentTime = 3;

            GoBox();
        }
    }


    void UpdateStun()
    {
        //하는 거 없음 찌릿찌릿
        ChangeState(EEnemyState.Sleep);
    }
    void UpdateSleep()
    {
        
        print("업데이트 슬립");
        if (hpSystem.currHp <= 80f && bossPhase == 1)
        {
            ChangeState(EEnemyState.Idle);
            print("1");
            bossPhase++;
        }
        else if (hpSystem.currHp <= 60f && bossPhase  == 2)
        {
            ChangeState(EEnemyState.Idle_2);
            print("2");
            bossPhase++;

        }
        else if (hpSystem.currHp <= 40f && bossPhase == 3)
        {
            ChangeState(EEnemyState.Idle);
            print("3");
            bossPhase++;

        }
        else if (hpSystem.currHp <= 0 && bossPhase == 4)
        {
            ChangeState(EEnemyState.Die);
            print("4");
            bossPhase++;

        }
        

    }
    void OnDie()
    {
        ChangeState(EEnemyState.Die);
    }
    public void OnDamaged()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //if(currState == EEnemyState.Sleep)
            //{
            //    return;
            //}
            if (currState == EEnemyState.Sleep)
            {
            HpSystem_BM hpSystem = GetComponent<HpSystem_BM>();
            hpSystem.UpdateHp(-10);
            }
            else { return; }
        }    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow") && currState == EEnemyState.Sleep)
        {
            print("현재 화살과 충돌~~");
            hpSystem.UpdateHp(-10);
            // damage
        }
    }


}
