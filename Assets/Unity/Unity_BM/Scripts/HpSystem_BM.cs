using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSystem_BM: MonoBehaviour
{
    
    //public float maxHp = 12;
    public float maxHp = 120;
    public float currHp;
    public Image hpBar;

    //public Action onDie;
    Enemy_BM enemy;

    // Start is called before the first frame update
    void Start()
    {
        currHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHp(float value)
    {
        // 현재 HP 를 value 만큼 더하자.
        currHp += value;

        // HPBar UI 갱신 (0 ~ 1)
        hpBar.fillAmount = currHp / maxHp;

        if (currHp <= 0)
        {
            enemy.ChangeState(Enemy_BM.EEnemyState_BM.Die);
        }
        //if (currHp > 90f)
        //{
        //    Debug.Log("1");
        //    Phase_01();
        //}
        //else if (currHp > 60f)
        //{
        //    Debug.Log("2");
        //    Phase_02();
        //}
        //else if (currHp > 30f)
        //{
        //    Debug.Log("3");
        //    Phase_03();
        //}   
        //// 만약에 현재 HP 가 0보다 작거나 같으면
        //else if (currHp <= 0)
        //{
        //    Debug.Log("4");
        //    // onDie 에 있는 함수를 실행하자.
        //    //if (onDie != null)
        //    //{
        //    //    Debug.Log("5");
        //    //    onDie();
        //    //}
        //}
    }
   
   
}
