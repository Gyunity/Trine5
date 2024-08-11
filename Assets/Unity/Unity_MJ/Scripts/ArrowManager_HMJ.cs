using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager_HMJ : MonoBehaviour
{
    public GameObject arrowPrefab; // 인스펙터에서 설정할 화살 프리팹

    public GameObject player;
    public ChangeCharacter playerChangeState;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        playerChangeState = player.GetComponentInChildren<ChangeCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnArrow()
    {
        if(playerChangeState.GetPlayerCharacterType() == PlayerCharacterType.ArcherType)
            Instantiate(arrowPrefab);
    }
}
