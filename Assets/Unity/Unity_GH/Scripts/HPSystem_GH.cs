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
    public Image hpBar_red;

    float hp_red_currTime = 0;
    float hp_red_delayTime = 3;


    void Start()
    {
        //현재 HP를 최대 HP로 생성
        currHP = maxHP;
    }

    void Update()
    {
        hp_red_currTime += Time.deltaTime;

        hpBar.fillAmount = currHP / maxHP;

        if (hp_red_currTime > hp_red_delayTime)
        {
            hpBar_red.fillAmount = Mathf.Lerp(hpBar_red.fillAmount, currHP / maxHP, 1 * Time.deltaTime);
        }

    }

    public void UpdateHP(float value)
    {
        hp_red_currTime = 0;

        // 현재 HP를 value만큼 더하자
        currHP += value;
        // 만약 현재 HP가 0보다 작거나 같으면
        if (currHP <= 0)
        {
            currHP = 0;
        }
    }

}
