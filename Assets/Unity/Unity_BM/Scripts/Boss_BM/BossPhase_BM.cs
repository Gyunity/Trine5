using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class BossPhase_BM : MonoBehaviour
{
    public enum EBossPhase
    {
        Phase_1,
        Phase_2,
        Phase_3,
        Phase_4

    }
    public EBossPhase currPhase;

    HpSystem_BM bossHp;
    EnemyMove bossFSM;

    // Start is called before the first frame update
    void Start()
    {
        bossHp=GetComponent<HpSystem_BM>();
        currPhase = EBossPhase.Phase_1;
        bossFSM=GetComponent<EnemyMove>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //switch (currPhase)
        //{
        //    case EBossPhase.Phase_1:
        //        if (bossHp.currHp < bossHp.maxHp * 0.8f)
        //        {
        //            bossHp.currHp = bossHp.maxHp * 0.8f;
        //        }
        //        break;
        //    case EBossPhase.Phase_2:
        //        if (bossHp.currHp < bossHp.maxHp * 0.6f)
        //        {
        //            bossHp.currHp = bossHp.maxHp * 0.6f;
        //        }
        //        break;
        //    case EBossPhase.Phase_3:
        //        if (bossHp.currHp < bossHp.maxHp * 0.4f)
        //        {
        //            bossHp.currHp = bossHp.maxHp * 0.4f;
        //        }
        //        break;
        //    case EBossPhase.Phase_4:
        //        if (bossHp.currHp < 0)
        //        {
        //            bossHp.currHp = 0;
        //        }
        //        break;
        //}
        
    }
    public void ChangeState(EBossPhase phase)
    {
        print(currPhase + "---->" + phase);

        currPhase = phase;

        switch (currPhase)
        {
            case EBossPhase.Phase_1:
                
                break;
            case EBossPhase.Phase_2:

                break;
            case EBossPhase.Phase_3:

                break;
            case EBossPhase.Phase_4:

                break;
        }
    }
}
