using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UIElements;
using static ArrowMove_HMJ;
using static PlayerState_HMJ;
using static SoundManager;
using static UnityEngine.Rendering.DebugUI;

public abstract class BasePlayer : MonoBehaviour
{
    // Character Controller
    private CharacterController cc;
    private Animator anim;

    // Player System
    protected PlayerState_HMJ playerState;
    private ChangeCharacter changeCharacter;
    private StaminaSystem_HMJ staminaSystem;

    // Player Manager
    private ArrowManager_HMJ arrowManager;
    private EffectManager_HMJ effectManager;

    // Move Data
    private float moveSpeed = 4.0f;
    private float playDashTime = 0.0f;
    private float dashMaxSpeed = 20.0f;
    private float dashTime = 0.3f;
    private float horizontal = 0.0f;

    // Jump Data
    private int maxJumpN = 2;
    private int JumpCurN = 0;
    private float jumpPower = 3.0f;
    private float gravity = -9.81f;
    private float yVelocity;

    // Dir Data
    private Vector3 dashDir;
    private Vector3 movement;
    private Vector3 incDir;

    // Attack Data
    private bool AttackCheck = false;

    private Collider targetCollider;
    public abstract void Attack();

    protected virtual void Start()
    {
        SettingComponent();
    }

    private void SettingComponent()
    {
        GameObject player = GameObject.Find("Player");

        cc = GetComponentInParent<CharacterController>();
        anim = GetComponentInParent<Animator>();

        playerState = player.GetComponentInParent<PlayerState_HMJ>();
        changeCharacter = player.GetComponentInParent<ChangeCharacter>();

        staminaSystem = player.GetComponentInParent<StaminaSystem_HMJ>();

        arrowManager = GameObject.Find("ArrowManager").GetComponent<ArrowManager_HMJ>();
        effectManager = player.GetComponentInParent<EffectManager_HMJ>();
    }
    protected virtual void Jump()
    {
        if (cc.isGrounded && (playerState.GetState() != PlayerState.DrawArrow) && (playerState.GetState() != PlayerState.Attack00) && (playerState.GetState() != PlayerState.Attack01) && (playerState.GetState() != PlayerState.DrawArrow))
        {
            JumpCurN = 0;
            yVelocity = 0.0f;

            playerState.SetState(PlayerState.Idle);
        }

        if (Input.GetButtonDown("Jump"))
        {
            playerState.SetState(PlayerState.Jump);
            if (JumpCurN < maxJumpN)
            {
                yVelocity = jumpPower;
                JumpCurN++;
            }
        }

        yVelocity += gravity * Time.deltaTime;
        movement.y += yVelocity;
    }
    protected virtual void Move()
    {
        incDir = GetIncVector();
        if (playerState.GetState() != PlayerState.DrawArrow)
            cc.Move((movement * moveSpeed + new Vector3(0.0f, yVelocity, 0.0f) + incDir) * Time.deltaTime);
    }

    protected virtual void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && staminaSystem.EnableDash())
            playerState.SetState(PlayerState.Dash);
    }

    protected virtual void Rotation()
    {
        Quaternion newRotation = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10.0f);
    }

    protected virtual void Movement()
    {
        if (movement.magnitude > 0)
        {
            if (playerState.GetState() == PlayerState.DrawArrow)
                movement = new Vector3(arrowManager.GetArrowDirection().x, 0.0f, 0.0f);
            anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
        }
    }

    protected virtual void Update()
    {
        PlayerUpdate();
    }

    private void PlayerUpdate()
    {
        if (playerState.GetState() != PlayerState.DrawArrow)
            PlayerZFixZeroMove();

        Movement();
        Rotation();
        Jump();
        Move();
        Dash();
        Attack();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    public void PlayerZFixZeroMove()
    {
        horizontal = Input.GetAxis("Horizontal");
        movement = new Vector3(horizontal, 0.0f, 0.0f);
    }

    public void DashMove()
    {
        playDashTime += Time.deltaTime;
        moveSpeed = Mathf.Lerp(moveSpeed, dashMaxSpeed, playDashTime / dashTime);
        cc.Move(dashDir * moveSpeed * Time.deltaTime);
        if (playDashTime >= dashTime)
        {
            playerState.SetState(PlayerState.Idle);
            moveSpeed = 5.0f;
        }
    }

    Vector3 GetIncVector()
    {
        Vector3 result = Vector3.zero;

        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        RaycastHit hitinfo;

        if (Physics.Raycast(ray, out hitinfo, 5, 1 << LayerMask.NameToLayer("SummonedObject")))
        {
            float dot = -Vector3.Dot(Vector3.right, hitinfo.normal);
            result = Vector3.Cross(Vector3.forward, hitinfo.normal) * dot;
        }
        return result;
    }

    public void ResetDashData()
    {
        playDashTime = 0.0f;
        moveSpeed = 4.0f;
    }
    public void SetCollisionCollider(Collider collider)
    {
        targetCollider = collider;
    }
    public void PlayerSwordAttackStart()
    {
        AttackCheck = true;
    }

    public void PlayerSwordAttackEnd()
    {
        AttackCheck = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            // "Boss" 레이어와 충돌했을 때 실행할 코드
            SoundManager.instance.PlayBossEftSound(SoundManager.EBossEftType.BOSS_HIT1);
            Debug.Log("Boss와 충돌: " + other.gameObject.name);
            HPSystem_GH hpSystem = other.gameObject.GetComponentInChildren<HPSystem_GH>();
            ValeribotFSM_GH bossFSM = other.gameObject.GetComponentInChildren<ValeribotFSM_GH>();
            if (!bossFSM.onShield)
                hpSystem.UpdateHP(-50.0f);
        }
    }

}
