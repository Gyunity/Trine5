using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static PlayerState_HMJ;

public class CharacterUISystem : MonoBehaviour
{
    // 최대 HP
    public float maxHP = 1000;
    // 현재 HP
    public float currHP;

    //HPBar
    public Image UIBar;

    PlayerState_HMJ playerState;

    HPSystem_HMJ hpSystem;

    public PlayerCharacterType characterType;

    void Start()
    {
        //현재 HP를 최대 HP로 생성
        currHP = maxHP;

        GameObject player = GameObject.Find("Player");
        playerState = player.GetComponentInChildren<PlayerState_HMJ>();

        hpSystem = player.GetComponentInChildren<HPSystem_HMJ>();
    }

    void Update()
    {
        UIBar.fillAmount = hpSystem.currHP[(int)characterType] / hpSystem.maxHP;
    }
}
