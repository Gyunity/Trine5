using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArrowMove_HMJ;
using static SoundManager;

public class ArrowManager_HMJ : MonoBehaviour
{
    public GameObject arrowPrefab; // 인스펙터에서 설정할 화살 프리팹

    public GameObject player;
    public ChangeCharacter playerChangeState;

    public GameObject arrowObject;
    public GameObject swingObject;

    EffectManager_HMJ effectManager;
    GameObject fireEffect;

    public float angle = 150.0f;
    private void Start()
    {
        player = GameObject.Find("Player");
        playerChangeState = player.GetComponentInChildren<ChangeCharacter>();
        effectManager = swingObject.GetComponentInChildren<EffectManager_HMJ>();
    }

    private void Update()
    {
        if(fireEffect && arrowObject)
        {
            Quaternion effectRotation = Quaternion.LookRotation(arrowObject.transform.forward);

            effectRotation *= Quaternion.Euler(0, 0.0f, 90.0f);

            fireEffect.transform.rotation = effectRotation;

            Vector3 position = swingObject.transform.position;
            position.y += 0.3f;
            fireEffect.transform.position = position;
        }
    }

    public void SpawnArrow()
    {
        if (playerChangeState.GetPlayerCharacterType() == PlayerCharacterType.ArcherType)
        {
            if (arrowObject && arrowObject.GetComponentInChildren<ArrowMove_HMJ>().m_eCurArrowState == ArrowState.ArrowStateEnd)
                return;
            arrowObject = Instantiate(arrowPrefab);

            Vector3 position = swingObject.transform.position;
            position.y += 0.5f;
            fireEffect = effectManager.SpawnAndPlayEffect(position, 0.5f, true, arrowObject.transform.forward);

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
