using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;
using static ValeribotFSM_GH;

public class ValeribotPhase_GH : MonoBehaviour
{
    public enum EValeribotPhase
    {
        PHASE_1,
        PHASE_2,
        PHASE_3,
        PHASE_4
    }

    public EValeribotPhase currPhase;

    //HPSystem
    HPSystem valeriHP;

    ValeribotFSM_GH valeribotFSM;

    public GameObject phase2_Shell;
    Rigidbody rd_phase2_Shell;

    void Start()
    {
        valeriHP = GetComponent<HPSystem>();

        currPhase = EValeribotPhase.PHASE_1;

        rd_phase2_Shell = phase2_Shell.GetComponent<Rigidbody>();

        valeribotFSM = GetComponent<ValeribotFSM_GH>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currPhase)
        {
            case EValeribotPhase.PHASE_1:
                break;
            case EValeribotPhase.PHASE_2:
                break;
            case EValeribotPhase.PHASE_3:
                break;
            case EValeribotPhase.PHASE_4:
                break;
        }


    }

    public void ChangeState(EValeribotPhase phase)
    {
        print(currPhase + "---->" + phase);

        currPhase = phase;

        switch (currPhase)
        {
            case EValeribotPhase.PHASE_1:
                valeribotFSM.bossPhase = 1;
                break;
            case EValeribotPhase.PHASE_2:
                ValeribotManager_GH.instance.onShield = true;
                rd_phase2_Shell.useGravity = true;
                valeribotFSM.bossPhase = 2;
                break;
            case EValeribotPhase.PHASE_3:
                ValeribotManager_GH.instance.onShield = true;

                valeribotFSM.bossPhase = 3;
                break;
            case EValeribotPhase.PHASE_4:
                ValeribotManager_GH.instance.onShield = true;

                valeribotFSM.bossPhase = 4;

                break;

        }
    }

    void HPPhase()
    {
        if(valeriHP.currHP <= valeriHP.maxHP * 0.8 && valeribotFSM.bossPhase == 1)
        {
            ChangeState(EValeribotPhase.PHASE_2);
        }
    }


}
