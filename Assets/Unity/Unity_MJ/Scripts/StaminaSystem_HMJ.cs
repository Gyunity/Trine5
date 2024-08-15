using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static PlayerState_HMJ;

public class StaminaSystem_HMJ : MonoBehaviour
{
    // 최대 Stamina
    public float maxStamina = 100.0f;
    // 현재 Stamina
    public float currStamina;

    //StaminaBar
    public Image StaminaBar;

    public GameObject StaminaCanvas;

    PlayerState_HMJ playerState;

    bool fillStamina = false;

    float staminaTime = 15.0f;
    float lerpData = 0.0f;

    float enableTime = 2.0f;
    void Start()
    {
        //현재 HP를 최대 HP로 생성
        currStamina = maxStamina;

        playerState = GameObject.Find("Player").GetComponentInChildren<PlayerState_HMJ>();
    }

    void Update()
    {
        if(fillStamina)
        {
            StaminaCanvas.SetActive(true);
            FillLerpStaminaData();
        }
        StaminaBar.fillAmount = currStamina / maxStamina;

    }

    public void DashStart()
    {
        ResetStamina();
        fillStamina = true;
    }

    public void SetStamina(float value)
    {
        currStamina = value;
    }

    public void ResetStamina()
    {
        currStamina = 0.0f;
        lerpData = 0.0f;
    }

    public void FillStaminaData(bool bfillStamina)
    {
        fillStamina = bfillStamina;
    }

    public void FillLerpStaminaData()
    {
        lerpData += Time.deltaTime;
        currStamina = Mathf.Lerp(currStamina, maxStamina, lerpData / staminaTime);

        if (currStamina > maxStamina)
        {
            StaminaCanvas.SetActive(false);
            fillStamina = false;
        }
    }
    
    public bool EnableDash()
    {
        if (StaminaBar.fillAmount >= 0.98f)
            return true;
        else
            return false;
    }
}
