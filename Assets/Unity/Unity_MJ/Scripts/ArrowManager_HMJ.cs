using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArrowMove_HMJ;

public class ArrowManager_HMJ : MonoBehaviour
{
    public GameObject arrowPrefab; // 인스펙터에서 설정할 화살 프리팹

    public GameObject player;
    public ChangeCharacter playerChangeState;

    public GameObject arrowObject;

    EffectManager_HMJ effectManager;

    GameObject SwordObject;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        playerChangeState = player.GetComponentInChildren<ChangeCharacter>();

        effectManager = SwordObject.GetComponentInChildren<EffectManager_HMJ>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnArrow()
    {
        if (playerChangeState.GetPlayerCharacterType() == PlayerCharacterType.ArcherType)
        {
            if (arrowObject && arrowObject.GetComponentInChildren<ArrowMove_HMJ>().m_eCurArrowState == ArrowState.ArrowStateEnd)
                return;
            arrowObject = Instantiate(arrowPrefab);
            effectManager.SpawnAndPlayEffect(arrowObject.transform.position, 5.0f);
        }
    }

    public Vector3 GetArrowDirection()
    {
        if (arrowObject)
            return arrowObject.transform.forward;
        else
            return new Vector3(0.0f, 0.0f, 0.0f);
    }
}
