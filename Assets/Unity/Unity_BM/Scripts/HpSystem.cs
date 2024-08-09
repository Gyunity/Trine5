using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSystem : MonoBehaviour
{
    public float maxHp = 3;
    private float currHp;
    public Image hpBar;

    public Action onDie;
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

        // 만약에 현재 HP 가 0보다 작거나 같으면
        if (currHp <= 0)
        {
            // onDie 에 있는 함수를 실행하자.
            if (onDie != null)
            {
                onDie();
            }
        }
    }
}
