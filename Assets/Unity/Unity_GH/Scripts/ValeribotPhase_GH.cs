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

    //페이즈 2
    public Rigidbody[] phase2_Shell;

    //페이즈 3
    public GameObject phase3_cannonShield;

    void Start()
    {
        phase3_cannonShield.SetActive(false);

        valeriHP = GetComponent<HPSystem>();

        currPhase = EValeribotPhase.PHASE_1;

        for (int i = 0; i < phase2_Shell.Length; i++)
        {
            phase2_Shell[i].useGravity = false;

        }

        valeribotFSM = GetComponent<ValeribotFSM_GH>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (currPhase)
        {
            case EValeribotPhase.PHASE_1:
                if (valeriHP.currHP < valeriHP.maxHP * 0.8f)
                {
                    valeriHP.currHP = valeriHP.maxHP * 0.8f;
                }
                break;
            case EValeribotPhase.PHASE_2:
                if (valeriHP.currHP < valeriHP.maxHP * 0.6f)
                {
                    valeriHP.currHP = valeriHP.maxHP * 0.6f;
                }
                break;
            case EValeribotPhase.PHASE_3:
                if (valeriHP.currHP < valeriHP.maxHP * 0.4f)
                {
                    valeriHP.currHP = valeriHP.maxHP * 0.4f;
                }
                break;
            case EValeribotPhase.PHASE_4:
                if (valeriHP.currHP < 0)
                {
                    valeriHP.currHP = 0;
                }
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
                valeribotFSM.onShield = true;
                for (int i = 0; i < phase2_Shell.Length; i++)
                {
                    phase2_Shell[i].useGravity = true;
                    phase2_Shell[i].constraints = RigidbodyConstraints.None;
                }
                valeribotFSM.bossPhase = 2;
                break;
            case EValeribotPhase.PHASE_3:
                valeribotFSM.onShield = true;
                phase3_cannonShield.SetActive(true);

                valeribotFSM.bossPhase = 3;
                break;
            case EValeribotPhase.PHASE_4:
                valeribotFSM.bossPhase = 4;
                break;

        }
    }

}
