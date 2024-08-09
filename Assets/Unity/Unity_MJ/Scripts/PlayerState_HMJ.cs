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
        DrawArrow,
        ShootArrow,
        PlayerStateEnd

    }

    public PlayerState curPlayerState;
    public PlayerState prePlayerState;

    public float grabyPos;

    Animator anim;

    PlayerMove_HMJ playerMove;

    GameObject arrowManager;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerMove = GetComponentInChildren<PlayerMove_HMJ>();
        arrowManager = GameObject.Find("ArrowManager");
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
                playerMove.Dash();
                break;
            case PlayerState.Grap:
                Debug.Log("Grap State");
                transform.position = new Vector3(transform.position.x, grabyPos, transform.position.z);
                break;
            case PlayerState.Climb:
                Debug.Log("Climb State");
                transform.position = new Vector3(transform.position.x, grabyPos, transform.position.z);
                break;
            case PlayerState.ShootArrow:
                Debug.Log("Shoot State");
                break;
            case PlayerState.DrawArrow:

                Debug.Log("Draw State");
                break;
        }
    }

    public void SetState(PlayerState playerState)
    {
        if(curPlayerState != playerState)
        {
            playerMove.ResetDashData();

            switch (playerState)
            {
                case PlayerState.Idle:
                    anim.SetTrigger("Idle");
                    Debug.Log("Test: Idle State");
                    break;
                case PlayerState.Walk:
                    break;
                case PlayerState.Jump:
                    anim.SetTrigger("Jump");
                    Debug.Log("Test: Jump State");
                    break;
                case PlayerState.Dash:
                    anim.SetTrigger("Dash");
                    Debug.Log("Test: Dash State");
                    break;
                case PlayerState.Grap:
                    anim.SetTrigger("Hanging");
                    Debug.Log("Test: Hanging State");
                    grabyPos = 1.8f;
                    break;
                case PlayerState.Climb:
                    anim.SetTrigger("Climb");
                    Debug.Log("Test: Climb State");
                    grabyPos = 1.4f;
                    break;
                case PlayerState.DrawArrow:
                    arrowManager.GetComponentInChildren<ArrowManager_HMJ>().SpawnArrow();
                    Debug.Log("SpawnArrow~~~");
                    anim.SetTrigger("ArrowDraw");
                    Debug.Log("Test: ArrowDraw State");
                    break;
                case PlayerState.ShootArrow:
                    anim.SetTrigger("ArrowShoot");
                    Debug.Log("Test: ArrowShoot State");
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
