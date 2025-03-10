﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushRoomObject_HMJ : MonoBehaviour
{
    Vector3 minScale = new Vector3(0.7f, 0.7f, 0.7f);
    Vector3 maxScale = new Vector3(0.7f, 0.7f, 0.7f);

    float scaleTime = 0.0f;

    int playN = 4;
    bool bScaleMove = false;

    float effectPlayTime = 2.0f;
    PlayerScaleState m_ePlayerScaleState;

    EffectManager_HMJ effectManager;

    bool effectPlay = false;
    public enum PlayerScaleState
    {
        SmallState,
        BigState,
        PlayerScaleStateEnd
    }
    // Start is called before the first frame update
    void Start()
    {
        bScaleMove = false;
        effectManager = GetComponentInChildren<EffectManager_HMJ>();

        minScale = new Vector3(transform.localScale.x * 0.7f, transform.localScale.y * 0.7f, transform.localScale.z * 0.7f);
        maxScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z * 1.5f);

        m_ePlayerScaleState = PlayerScaleState.PlayerScaleStateEnd;

        playN = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(bScaleMove && !effectPlay)
        {
            scaleTime += Time.deltaTime;
            if (scaleTime > 1.0f)
            {
                --playN;
                if (m_ePlayerScaleState == PlayerScaleState.SmallState)
                    m_ePlayerScaleState = PlayerScaleState.BigState;
                else
                    m_ePlayerScaleState = PlayerScaleState.SmallState;
                scaleTime = 0.0f;
            }
            switch (m_ePlayerScaleState)
            {
                case PlayerScaleState.SmallState:
                    gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, minScale, scaleTime);
                    break;
                case PlayerScaleState.BigState:
                    gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, maxScale, scaleTime);
                    break;
                case PlayerScaleState.PlayerScaleStateEnd:
                    break;
            }
        }

        if(playN <= 0 && !effectPlay)
        {
            effectPlay = true;
            effectManager.SpawnAndPlayEffect(transform.position, 3.0f, false, new Vector3(0.0f, 0.0f, 0.0f));
        }
        if (playN <= 0)
        {
            if (effectPlayTime < 0.0f)
                Destroy(gameObject);
            effectPlayTime -= Time.deltaTime;
        }

        if(effectPlay)
        {

        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Player") && !bScaleMove)
        {
            bScaleMove = true;

            m_ePlayerScaleState = PlayerScaleState.BigState;
            scaleTime = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player") && !bScaleMove)
        {
            bScaleMove = true;

            m_ePlayerScaleState = PlayerScaleState.BigState;
            scaleTime = 0.0f;
        }
    }
}
