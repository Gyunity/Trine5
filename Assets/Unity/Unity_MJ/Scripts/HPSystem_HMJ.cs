using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static PlayerState_HMJ;

public class HPSystem_HMJ : MonoBehaviour
{
    // 최대 HP
    public float maxHP = 1000;
    // 현재 HP
    public float[] currHP;

    public int[] lifeData;

    //HPBar
    public Image hpBar;

    PlayerState_HMJ playerState;

    DeathTimeUI_HMJ deathTimeUI;

    ChangeCharacter changeCharacter;

    public GameObject LifeCanavs;

    public GameObject HpCanvas;
    public GameObject StaminaCanvas;

    bool bUpdateDeath = false;
    private void Awake()
    {
        currHP = new float[3];
        //현재 HP를 최대 HP로 생성

        lifeData = new int[3];

        for (int i = 0; i < 3; i++)
        {
            currHP[i] = maxHP;
            lifeData[i] = 3;
        }

        playerState = GameObject.Find("Player").GetComponentInChildren<PlayerState_HMJ>();
        deathTimeUI = GameObject.Find("Player").GetComponentInChildren<DeathTimeUI_HMJ>();
        changeCharacter  = GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>();
        LifeCanavs.SetActive(false);
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            UpdateHP(-300.0f, GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>().GetPlayerCharacterType());
        }
        if (deathTimeUI.EnableDeath() && playerState.GetState() != PlayerState.DrawArrow)
        {
            playerState.SetState(PlayerState_HMJ.PlayerState.Idle);
        }

        if(bUpdateDeath)
        {
            
            if(deathTimeUI.GetChangeCharacter())
            {
                deathTimeUI.SetChangeCharacter(false);
                playerState.SetState(PlayerState.reIdle);
                HpCanvas.SetActive(true);
                StaminaCanvas.SetActive(true);
                LifeCanavs.SetActive(false);

                if (lifeData[(int)GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>().GetPlayerCharacterType()] == 0)
                {
                    ChangeCharacter();
                }
                SetCurMaxHp();

            }

            
        }
    }

    public void SetCurMaxHp()
    {
        currHP[(int)GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>().GetPlayerCharacterType()] = maxHP;
        UpdateHP(0.0f, GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>().GetPlayerCharacterType());
    }

    public void UpdateHP(float value, PlayerCharacterType characterType)
    {
        // 현재 HP를 value만큼 더하자
        currHP[(int)characterType] += value;

        hpBar.fillAmount = currHP[(int)characterType] / maxHP;

        // 만약 현재 HP가 0보다 작거나 같으면
        if (currHP[(int)characterType] <= 0)
        {
            currHP[(int)characterType] = 0;
            --lifeData[(int)GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>().GetPlayerCharacterType()];

     
            LifeCanavs.SetActive(true);
            deathTimeUI.DeathStart();
            playerState.SetState(PlayerState.Death);
            bUpdateDeath = true;
            HpCanvas.SetActive(false);
            StaminaCanvas.SetActive(false);


            return;
        }
        playerState.SetState(PlayerState.Damaged);
    }

    public void Dead()
    {

        SetCurMaxHp();
        playerState.SetState(PlayerState.Idle);
        bUpdateDeath = false;
    }

    void ChangeCharacter()
    {
        for(PlayerCharacterType i = PlayerCharacterType.WizardType; i < PlayerCharacterType.PlayerCharacterTypeEnd; i++)
        {
            if(lifeData[(int)i] >= 1)
            {
                changeCharacter.SetMeshData(i);
            }

        }
    }
}
