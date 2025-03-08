using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class PlayerState_HMJ : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Jump,
        Dash,
        NoDash,
        Climb,
        DrawArrow,
        ShootArrow,
        Damaged,
        Death,
        Attack00,
        Attack01,
        reIdle,
        Magic,
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

    BasePlayer playerMove;

    GameObject arrowManager;

    HPSystem_HMJ hpSystem;

    StaminaSystem_HMJ staminaSystem;

    ChangeCharacter changeCharacter;

    public GameObject HpCanvas;

    public float maxDamageTime = 5.0f;
    public float damageTime = 5.0f;
    private void Awake()
    {
        GameObject player = GameObject.Find("Player");

        anim = GetComponentInChildren<Animator>();
        playerMove = GetComponentInChildren<BasePlayer>();
        arrowManager = GameObject.Find("ArrowManager");
        hpSystem = player.GetComponentInChildren<HPSystem_HMJ>();
        staminaSystem = player.GetComponentInChildren<StaminaSystem_HMJ>();
        changeCharacter = player.GetComponentInChildren<ChangeCharacter>();

        maxDamageTime = 5.0f;
        damageTime = 5.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        curPlayerState = PlayerState.PlayerStateEnd;
        prePlayerState = PlayerState.PlayerStateEnd;

        curPlayerMoveState = PlayerMoveState.PlayerMoveStateEnd;
        prePlayerMoveState = PlayerMoveState.PlayerMoveStateEnd;
    }
    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        damageTime += Time.deltaTime;
        if (damageTime < maxDamageTime)
        {
            HpCanvas.SetActive(true);
        }
        else
        {
            HpCanvas.SetActive(false);
        }
        switch (curPlayerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Walk:
                break;
            case PlayerState.Jump:
                break;
            case PlayerState.Dash:
                playerMove.DashMove();
                break;
            case PlayerState.Climb:
                transform.position = new Vector3(transform.position.x, grabyPos, transform.position.z);
                break;
        }
    }

    public bool SetState(PlayerState playerState)
    {
        if (curPlayerState == PlayerState.Death && playerState == PlayerState.Damaged)
            return false;

        if (playerState == PlayerState.Dash && (changeCharacter.GetPlayerCharacterType() != PlayerCharacterType.WarriorType))
            return false;
        if (curPlayerState != playerState)
        {
            playerMove.ResetDashData();

            switch (playerState)
            {
                case PlayerState.reIdle:
                    anim.SetTrigger("Idle");
                    break;
                case PlayerState.Idle:
                    anim.SetTrigger("Idle");
                    break;
                case PlayerState.Walk:
                    break;
                case PlayerState.Jump:
                    SoundManager.instance.PlayPlayerEftSound(EPlayerEftType.PLAYER_JUMP);
                    anim.SetTrigger("Jump");
                    break;
                case PlayerState.Dash:
                    SoundManager.instance.PlayPlayerEftSound(EPlayerEftType.PLAYER_WARRIOR_DASH);
                    staminaSystem.DashStart();
                    anim.SetTrigger("Dash");
                    break;
                case PlayerState.Climb:
                    anim.SetTrigger("Climb");
                    grabyPos = 1.4f;
                    break;
                case PlayerState.DrawArrow:
                    if (arrowManager) // Test용 방어 코드
                        arrowManager.GetComponentInChildren<ArrowManager_HMJ>().SpawnArrow();
                    anim.SetTrigger("ArrowDraw");
                    break;
                case PlayerState.ShootArrow:
                    anim.SetTrigger("ArrowShoot");
                    SoundManager.instance.PlayPlayerEftSound(EPlayerEftType.PLAYER_ARCHER_ARROWLONGSHOOT);
                    break;
                case PlayerState.Damaged:
                    if( changeCharacter.GetPlayerCharacterType() == PlayerCharacterType.WarriorType)
                        SoundManager.instance.PlayPlayerEftSound(EPlayerEftType.PLAYER_WARRIOR_DAMAGE);
                    else if (changeCharacter.GetPlayerCharacterType() == PlayerCharacterType.ArcherType)
                        SoundManager.instance.PlayPlayerEftSound(EPlayerEftType.PLAYER_ARCHER_DAMAGE);
                    else if(changeCharacter.GetPlayerCharacterType() == PlayerCharacterType.WizardType)
                        SoundManager.instance.PlayPlayerEftSound(EPlayerEftType.PLAYER_WIZARD_DAMAGE);
                    damageTime = 0.0f;
                    anim.SetTrigger("Hit");
                    break;
                case PlayerState.Death:
                    anim.SetTrigger("Death");
                    if (changeCharacter.GetPlayerCharacterType() == PlayerCharacterType.WarriorType)
                        SoundManager.instance.PlayPlayerEftSound(EPlayerEftType.PLAYER_WIZARD_WARRIOR_DEAD);
                    else if (changeCharacter.GetPlayerCharacterType() == PlayerCharacterType.ArcherType)
                        SoundManager.instance.PlayPlayerEftSound(EPlayerEftType.PLAYER_ARCHER_DEAD);
                    else if (changeCharacter.GetPlayerCharacterType() == PlayerCharacterType.WizardType)
                        SoundManager.instance.PlayPlayerEftSound(EPlayerEftType.PLAYER_WIZARD_WARRIOR_DEAD);
                    break;
                case PlayerState.Attack00:
                    anim.SetTrigger("Attack00");
                    break;
                case PlayerState.Attack01:
                    anim.SetTrigger("Attack01");
                    SoundManager.instance.PlayPlayerEftSound(EPlayerEftType.PLAYER_WARRIOR_THROWSWORD);
                    break;
                case PlayerState.Magic:
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

    public PlayerState GetState()
    {
        return curPlayerState;
    }

    public PlayerMoveState GetMoveState()
    {
        return curPlayerMoveState;
    }
}
