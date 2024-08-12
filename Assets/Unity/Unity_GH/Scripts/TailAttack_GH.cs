using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailAttack_GH : MonoBehaviour
{
    public HPSystem_HMJ playerHPSC;
    ValeribotFSM_GH bossFSM;

    void Start()
    {
        bossFSM = GetComponentInParent<ValeribotFSM_GH>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && bossFSM.currState != ValeribotFSM_GH.EValeribotState.JUMP)
        {
            playerHPSC.UpdateHP(-bossFSM.tailAttackValue);
        }
    }
}
