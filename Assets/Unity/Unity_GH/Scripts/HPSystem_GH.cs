using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem_GH : MonoBehaviour
{
    // 최대 HP
    public float maxHP = 1000;
    // 현재 HP
    public float currHP;

    //HPBar
    public Image hpBar;

    void Start()
    {
        //현재 HP를 최대 HP로 생성
        currHP = maxHP;
    }

    void Update()
    {

    }

    public void UpdateHP(float value)
    {
        // 현재 HP를 value만큼 더하자
        currHP += value;

        hpBar.fillAmount = currHP / maxHP;

        // 만약 현재 HP가 0보다 작거나 같으면
        if (currHP <= 0)
        {
            currHP = 0;
        }
    }

}
