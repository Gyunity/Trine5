using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_HMJ : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        LeftRun,
        RightRun,
        Jump,
        Dash,
        Grap,
        GrapDown,
        PlayerStateEnd

    }

    PlayerState curPlayerState;
    PlayerState prePlayerState;

    float grabyPos;
    // Start is called before the first frame update
    void Start()
    {
        curPlayerState = PlayerState.PlayerStateEnd;
        prePlayerState = PlayerState.PlayerStateEnd;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    void UpdateState()
    {
        switch(curPlayerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.LeftRun:
                break;
            case PlayerState.RightRun:
                break;
            case PlayerState.Jump:
                break;
            case PlayerState.Dash:
                break;
            case PlayerState.Grap:
                transform.position = new Vector3(transform.position.x, grabyPos, transform.position.z);
                Debug.Log("ÇöÀç ±×·¦Áß");
                break;
        }
    }

    public void SetState(PlayerState playerState)
    {
        if(curPlayerState != playerState)
        {
            switch (playerState)
            {
                case PlayerState.Idle:
                    break;
                case PlayerState.LeftRun:
                    break;
                case PlayerState.RightRun:
                    break;
                case PlayerState.Jump:
                    break;
                case PlayerState.Dash:
                    break;
                case PlayerState.Grap:
                    grabyPos = transform.position.y;
                    break;
            }

            prePlayerState = curPlayerState;
            curPlayerState = playerState;
        }
    }

    public PlayerState GetState()
    {
        return curPlayerState;
    }
}
