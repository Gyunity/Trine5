using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using static PlayerState_HMJ;

public class PlayerWarriorAttackLeft : StateMachineBehaviour
{
    PlayerState_HMJ playerState;
    bool nextAttack = false;
    EffectManager_HMJ effectManager;

    GameObject swordPosition;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player;
        player = GameObject.Find("Player");
        playerState = GameObject.Find("Player").GetComponentInChildren<PlayerState_HMJ>();
        //swordPosition = FindBoneManager_HMJ.Instance.FindBone(player.transform, "SwordPosition").transform.gameObject;

        //effectManager = swordPosition.GetComponentInChildren<EffectManager_HMJ>();

        //effectManager.SpawnAndPlayEffect(swordPosition.transform.position, 200.0f, false, new Vector3(0.0f, 0.0f, 0.0f));

        nextAttack = false;

        

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetMouseButtonDown(0))
        {
            nextAttack = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(nextAttack)
            playerState.SetState(PlayerState.Attack01);
        else
            playerState.SetState(PlayerState.Idle);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
