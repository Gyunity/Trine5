using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static PlayerState_HMJ;

public class ChangeLerpColorUI_HMJ : MonoBehaviour
{
    enum HpChangeType
    {
        WaitHpChange,
        MoveHpChange,
        ChangeHpChange,
        HpChangeTypeEnd
    };

    HPSystem_HMJ playerHpSystem;

    public Material targetMaterial;
    public Vector3 firstColorData;

    public Vector3 lastColorData;
    Image imageData;

    float maxDelayTime = 1.0f;
    float delayTime = 0.0f;

    float maxLerpTime = 1.0f;
    float lerpTime = 0.0f;

    float curHpValue = 0.0f;
    float preHpValue = 0.0f;

    float hpValue = 0.0f;

    HpChangeType hpCurChangeType;
    HpChangeType hpPreChangeType;

    public Image hpLerpBar;

    float _delayTime = 1.5f;
    bool changeHpLerp = false;
    bool moveHpLerp = false;
    float data = 0.0f;

    ChangeCharacter changeCharacter;
    // Start is called before the first frame update
    void Start()
    {
        imageData = GetComponent<Image>();

        targetMaterial = imageData.material;

        // 153, 243, 111

        firstColorData = new Vector3(153.0f / 255.0f, 243.0f / 255.0f, 111.0f / 255.0f);
        lastColorData = new Vector3(255.0f / 255.0f, 0.0f, 0.0f);

        playerHpSystem = GameObject.Find("Player").GetComponentInChildren<HPSystem_HMJ>();

        hpCurChangeType = HpChangeType.WaitHpChange;

        hpValue = playerHpSystem.maxHP;
        curHpValue = playerHpSystem.maxHP;

        changeCharacter = GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        hpLerpBar.fillAmount = hpValue / playerHpSystem.maxHP;

        if(changeHpLerp) // Lerp로 값을 바꾸어야 한다면
        {
            _delayTime -= Time.deltaTime;
            if(_delayTime < 0.0f)
            {
                lerpTime = 0.0f;
                moveHpLerp = true;
                changeHpLerp = false;

                Debug.Log("현재 lerpTime: " + lerpTime);
            }
        }

        if(moveHpLerp)
        {
            lerpTime += Time.deltaTime;
            hpValue = Mathf.Lerp(hpValue, playerHpSystem.currHP[(int)changeCharacter.GetPlayerCharacterType()], lerpTime);

            Vector3 lerpValue = Vector3.Lerp(firstColorData, lastColorData, 1.0f - lerpTime);
            imageData.color = new Color(lerpValue.x, lerpValue.y, lerpValue.z, 1.0f);

            Debug.Log("현재 hpValue: " + hpValue);
            if (lerpTime > maxLerpTime)
                moveHpLerp = false;
        }

        if (curHpValue > playerHpSystem.currHP[(int)changeCharacter.GetPlayerCharacterType()] && !changeHpLerp) // hp 변화
        {
            preHpValue = curHpValue; // 이전 hp 저장
            curHpValue = playerHpSystem.currHP[(int)changeCharacter.GetPlayerCharacterType()]; // 현재 hp 저장

            data = preHpValue;
            changeHpLerp = true;
            _delayTime = 1.5f;
        }

        //ChangeHpValue(); // 만약 hp가 깎였다면 ChangeHpChange로 상태 변환
        //UpdateHpState();
    }

    void ChangeHpValue()
    {
        preHpValue = curHpValue; // 이전 hp 저장
        curHpValue = playerHpSystem.currHP[(int)changeCharacter.GetPlayerCharacterType()]; // 현재 hp 저장

        if (preHpValue > curHpValue) // hp가 깎였으면 - hp가 변화했으면
        {
            ChangeHpState(HpChangeType.ChangeHpChange);
        }
    }

    void ChangeHpState(HpChangeType hpChangeType)
    {
        if (hpCurChangeType == hpChangeType) // 현재 같지 않으면
            return;

        switch(hpChangeType)
        {
            case HpChangeType.WaitHpChange:
                delayTime = maxDelayTime;
                break;
            case HpChangeType.MoveHpChange:
                break;
            case HpChangeType.ChangeHpChange:
                hpValue = preHpValue; // 이전 hp값으로 설정 (현재 보여지는 hp ui)
                ChangeHpState(HpChangeType.WaitHpChange);
                break;
            case HpChangeType.HpChangeTypeEnd:
                break;
        }
        hpPreChangeType = hpCurChangeType;
        hpCurChangeType = hpChangeType;
    }

    void UpdateHpState()
    {
        switch (hpCurChangeType)
        {
            case HpChangeType.WaitHpChange:
                delayTime -= Time.deltaTime;
                if (delayTime < 0.0f)
                    ChangeHpState(HpChangeType.MoveHpChange);
                break;
            case HpChangeType.MoveHpChange:
                lerpTime += Time.deltaTime;
                hpValue = Mathf.Lerp(preHpValue, curHpValue, lerpTime / 3.0f);
                if(lerpTime > maxLerpTime)
                    ChangeHpState(HpChangeType.HpChangeTypeEnd);
                break;
            case HpChangeType.ChangeHpChange:
                break;
            case HpChangeType.HpChangeTypeEnd:
                break;
        }
    }
}
