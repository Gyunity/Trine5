using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSystem_BM : MonoBehaviour
{
    bool die = false;

    ////public float maxHp = 12;
    //public float maxHp = 90;
    //public float currHp;
    //public Image hpBar; 

    ////public Action onDie;
    Enemy_BM enemy;

    // 최대 HP
    public float maxHP = 1000;
    // 현재 HP
    public float currHP;

    //HPBar
    public Image hpBar;
    public Image hpBar_red;

    float hp_red_currTime = 0;
    float hp_red_delayTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        enemy = GetComponent<Enemy_BM>();
    }

    // Update is called once per frame
    void Update()
    {
        hp_red_currTime += Time.deltaTime;

        hpBar.fillAmount = currHP / maxHP;

        if (hp_red_currTime > hp_red_delayTime)
        {
            hpBar_red.fillAmount = Mathf.Lerp(hpBar_red.fillAmount, currHP / maxHP, 1 * Time.deltaTime);
        }

        if (currHP <= 0)
        {
            if (!die)
            {
                enemy.ChangeState(Enemy_BM.EEnemyState_BM.Die);
                die = true;
            }

            //Debug.Log("3");
            // onDie 에 있는 함수를 실행하자.
            //if (onDie != null)
            //{
            //    Debug.Log("5");
            //    onDie();
            //}
        }


    }
    public void UpdateHp(float value)
    {
        hp_red_currTime = 0;

        // 현재 HP를 value만큼 더하자
        currHP += value;
        // 만약 현재 HP가 0보다 작거나 같으면
        if (currHP <= 0)
        {
            currHP = 0;
        }




        //// 현재 HP 를 value 만큼 더하자.
        //currHp += value;

        //// HPBar UI 갱신 (0 ~ 1)
        //hpBar.fillAmount = currHp / maxHp;

        ////if (currHp <= 0)
        ////{
        ////    enemy.ChangeState(Enemy_BM.EEnemyState_BM.Die);
        ////}
        //if (currHp > 60f)
        //{
        //    Debug.Log("1");
        //    //Phase_01();
        //}
        //else if (currHp > 30f)
        //{
        //    Debug.Log("2");
        //    //Phase_02();
        //}
        ////else if (currHp > 30f)
        ////{
        ////    Debug.Log("3");
        ////    Phase_03();
        ////}
        //// 만약에 현재 HP 가 0보다 작거나 같으면
        //else if (currHp <= 0)
        //{
        //    Debug.Log("3");
        //    enemy.ChangeState(Enemy_BM.EEnemyState_BM.Die);
        //    // onDie 에 있는 함수를 실행하자.
        //    //if (onDie != null)
        //    //{
        //    //    Debug.Log("5");
        //    //    onDie();
        //    //}
        //}
    }


}
