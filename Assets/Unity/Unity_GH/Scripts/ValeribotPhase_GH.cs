using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.InspectorCurveEditor;
using static ValeribotFSM_GH;

public class ValeribotPhase_GH : MonoBehaviour
{
    public enum EValeribotPhase
    {
        PHASE_1,
        PHASE_2,
        PHASE_3
    }

    public EValeribotPhase currPhase;

    //HPSystem
    HPSystem_GH valeriHP;

    ValeribotFSM_GH valeribotFSM;

    //페이즈 2
    public Rigidbody[] phase2_Shell;

    //페이즈 3
    public GameObject phase3_cannonShield;

    //페이즈 실드
    public Image[] ShieldHPs;

    void Start()
    {

        valeriHP = GetComponent<HPSystem_GH>();

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
                if (!valeribotFSM.onShield)
                {
                    ShieldHPs[0].gameObject.SetActive(false);
                }
                if (valeriHP.currHP < valeriHP.maxHP * 0.75f)
                {
                    valeriHP.currHP = valeriHP.maxHP * 0.75f;
                }
                break;
            case EValeribotPhase.PHASE_2:
                if (!valeribotFSM.onShield)
                {
                    ShieldHPs[1].gameObject.SetActive(false);
                }
                if (valeriHP.currHP < valeriHP.maxHP * 0.5f)
                {
                    valeriHP.currHP = valeriHP.maxHP * 0.5f;
                }
                break;
            case EValeribotPhase.PHASE_3:
                if (!valeribotFSM.onShield)
                {
                    ShieldHPs[2].gameObject.SetActive(false);
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

                valeribotFSM.bossPhase = 3;
                break;

        }
    }

}
