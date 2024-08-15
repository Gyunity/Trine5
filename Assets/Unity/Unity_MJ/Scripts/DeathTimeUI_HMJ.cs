using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathTimeUI_HMJ : MonoBehaviour
{
    // 최대 Death 타임
    public float maxDeath = 100.0f;

    // 현재 Death
    public float currDeath;

    //DeathBar
    public Image DeathBar;

    PlayerState_HMJ playerState;
    HPSystem_HMJ hpSystem;

    bool fillDeath = false;
    bool changeCharacter = false;

    float deathTime = 10.0f;
    float lerpData = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //현재 HP를 최대 HP로 생성
        currDeath = 0.0f;

        playerState = GameObject.Find("Player").GetComponentInChildren<PlayerState_HMJ>();

        hpSystem = GameObject.Find("Player").GetComponentInChildren<HPSystem_HMJ>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fillDeath)
        {
            FillLerpDeathData();
        }
        DeathBar.fillAmount = currDeath / maxDeath;
    }



    public void DeathStart()
    {
        ResetDeath();
        fillDeath = true;
    }

    public void SetDeath(float value)
    {
        currDeath = value;
    }

    public void ResetDeath()
    {
        currDeath = 0.0f;
        lerpData = 0.0f;
    }

    public void FillDeathData(bool bfillStamina)
    {
        fillDeath = bfillStamina;
    }

    public void FillLerpDeathData()
    {
        lerpData += Time.deltaTime * 0.3f;
        currDeath = Mathf.Lerp(0.0f, maxDeath, lerpData);
        Debug.Log("현재 죽음값: " + currDeath);
        if ((lerpData) >= 1.0f && fillDeath)
        {
            // 캐릭터 변경 또는 별 줄이기
            fillDeath = false;
            changeCharacter = true;
        }
    }

    public bool GetChangeCharacter()
    {
        return changeCharacter;
    }
    public void SetChangeCharacter(bool changeCharacterData)
    {
        changeCharacter = changeCharacterData;
    }
    public bool EnableDeath()
    {
        if (DeathBar.fillAmount >= 0.98f)
            return true;
        else
            return false;
    }
}
