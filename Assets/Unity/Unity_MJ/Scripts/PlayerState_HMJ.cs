using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_HMJ : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Jump,
        Dash,
        Grap,
        Climb,
        PlayerStateEnd

    }

    PlayerState curPlayerState;
    PlayerState prePlayerState;

    float grabyPos;

    Animator anim;

    PlayerMove_HMJ playerMove;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerMove = GetComponentInChildren<PlayerMove_HMJ>();
    }
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
                Debug.Log("Idle State");
                break;
            case PlayerState.Walk:
                Debug.Log("Walk State");
                break;
            case PlayerState.Jump:

                Debug.Log("Jump State");
                break;
            case PlayerState.Dash:
                Debug.Log("Dash State");
                break;
            case PlayerState.Grap:
                Debug.Log("Grap State");
                transform.position = new Vector3(transform.position.x, grabyPos, transform.position.z);
                break;
            case PlayerState.Climb:
                Debug.Log("Climb State");
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
                    playerMove.ResetDashData();
                    break;
                case PlayerState.Walk:
                    playerMove.ResetDashData();
                    break;
                case PlayerState.Jump:
                    playerMove.ResetDashData();
                    anim.SetTrigger("Jump");
                    break;
                case PlayerState.Dash:
                    anim.SetTrigger("Dash");
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
