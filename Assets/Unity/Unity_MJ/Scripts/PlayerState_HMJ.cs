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
        NoDash,
        Grap,
        Climb,
        DrawArrow,
        ShootArrow,
        Swinging,
        Damaged,
        Death,
        Attack00,
        Attack01,
        reIdle,
        PlayerStateEnd

    }

    public enum PlayerMoveState
    {
        Player_ZeroZ,
        Player_FixZ,
        PlayerMoveStateEnd
    }

    public PlayerState curPlayerState;
    public PlayerState prePlayerState;


    public PlayerMoveState curPlayerMoveState;
    public PlayerMoveState prePlayerMoveState;

    public float grabyPos;

    Animator anim;

    PlayerMove_HMJ playerMove;

    GameObject arrowManager;

    HPSystem_HMJ hpSystem;

    StaminaSystem_HMJ staminaSystem;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerMove = GetComponentInChildren<PlayerMove_HMJ>();
        arrowManager = GameObject.Find("ArrowManager");
        hpSystem = GameObject.Find("Player").GetComponentInChildren<HPSystem_HMJ>();
        staminaSystem = GameObject.Find("Player").GetComponentInChildren<StaminaSystem_HMJ>();
    }
    // Start is called before the first frame update
    void Start()
    {
        curPlayerState = PlayerState.PlayerStateEnd;
        prePlayerState = PlayerState.PlayerStateEnd;

        curPlayerMoveState = PlayerMoveState.PlayerMoveStateEnd;
        prePlayerMoveState = PlayerMoveState.PlayerMoveStateEnd;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        //if (Input.GetKeyDown(KeyCode.B))
        //    hpSystem.UpdateHP(-100.0f, GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>().GetPlayerCharacterType());
    }

    void UpdateState()
    {
        switch (curPlayerState)
        {
            case PlayerState.Idle:
                //Debug.Log("Idle State");
                break;
            case PlayerState.Walk:
                //Debug.Log("Walk State");
                break;
            case PlayerState.Jump:
                //Debug.Log("Jump State");
                break;
            case PlayerState.Dash:
                //Debug.Log("Dash State");
                playerMove.Dash();
                break;
            case PlayerState.Grap:
                //Debug.Log("Grap State");
                transform.position = new Vector3(transform.position.x, grabyPos, transform.position.z);
                break;
            case PlayerState.Climb:
                //Debug.Log("Climb State");
                transform.position = new Vector3(transform.position.x, grabyPos, transform.position.z);
                break;
            case PlayerState.ShootArrow:
                //Debug.Log("Shoot State");
                break;
            case PlayerState.DrawArrow:
                //Debug.Log("Draw State");
                break;
            case PlayerState.Swinging:

                //playerMove.MoveWithBounce();
                //Debug.Log("Swinging State");
                break;
            case PlayerState.Damaged:
                //anim.SetTrigger("Swinging");
                break;
            case PlayerState.Attack00:
                break;
            case PlayerState.Attack01:
                break;
        }
    }

    public bool SetState(PlayerState playerState)
    {
        if (curPlayerState == PlayerState.Death && playerState == PlayerState.Damaged)
            return false;

        
        if (curPlayerState != playerState)
        {
            playerMove.ResetDashData();

            switch (playerState)
            {
                //case PlayerState.reIdle:
                //    anim.SetTrigger("Idle");
                //    break;
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
                    staminaSystem.DashStart();
                    anim.SetTrigger("Dash");
                    Debug.Log("Test: Dash State");
                    break;
                case PlayerState.Grap:
                    anim.SetTrigger("Grap");
                    Debug.Log("Test: Grap State");
                    grabyPos = 1.8f;
                    break;
                case PlayerState.Climb:
                    anim.SetTrigger("Climb");
                    Debug.Log("Test: Climb State");
                    grabyPos = 1.4f;
                    break;
                case PlayerState.DrawArrow:
                    if (arrowManager) // Test용 방어 코드
                        arrowManager.GetComponentInChildren<ArrowManager_HMJ>().SpawnArrow();
                    Debug.Log("Test: SpawnArrow~~~");
                    anim.SetTrigger("ArrowDraw");
                    Debug.Log("플레이어 상태 변경: " + "ArrowDraw");
                    //Debug.Log("Test: ArrowDraw State");
                    break;
                case PlayerState.ShootArrow:
                    anim.SetTrigger("ArrowShoot");
                    Debug.Log("Test: ArrowShoot State");
                    break;
                case PlayerState.Swinging:
                    anim.SetTrigger("Swinging");
                    playerMove.SelectHangingObject();
                    Debug.Log("Test: Swinging State");
                    break;
                case PlayerState.Damaged:
                    anim.SetTrigger("Hit");
                    break;
                case PlayerState.Death:
                    anim.SetTrigger("Death");
                    break;
                case PlayerState.Attack00:
                    anim.SetTrigger("Attack00");
                    break;
                case PlayerState.Attack01:
                    anim.SetTrigger("Attack01");
                    break;
            }

            prePlayerState = curPlayerState;
            curPlayerState = playerState;

            return true;
        }
        return false;
    }


    public bool SetplayerMoveState(PlayerMoveState playerMoveState)
    {
        if (curPlayerMoveState != playerMoveState)
        {
            //playerMove.ResetDashData();

            switch (playerMoveState)
            {
                case PlayerMoveState.Player_ZeroZ:

                    break;
                case PlayerMoveState.Player_FixZ:
                    break;
            }

            prePlayerMoveState = curPlayerMoveState;
            curPlayerMoveState = playerMoveState;

            return true;
        }
        return false;
    }

    // 

    public PlayerState GetState()
    {
        return curPlayerState;
    }

    public PlayerMoveState GetMoveState()
    {
        return curPlayerMoveState;
    }
}
