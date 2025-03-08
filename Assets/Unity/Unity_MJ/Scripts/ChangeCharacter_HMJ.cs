using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static ChangeGrabObject_HMJ;

public enum PlayerCharacterType
{
    WizardType,
    ArcherType,
    WarriorType,
    PlayerCharacterTypeEnd
}

public class ChangeCharacter : MonoBehaviour
{
    List<GameObject> objectList = new List<GameObject>();

    List<Avatar> avatarList = new List<Avatar>();
    List<RuntimeAnimatorController> animControllerList = new List<RuntimeAnimatorController>();

    PlayerCharacterType curPlayerCharacterType = PlayerCharacterType.PlayerCharacterTypeEnd;
    Animator animator;

    EffectManager_HMJ effectManager;
    GameObject changeEffect;

    GameObject player;

    private void Awake()
    {
        LoadPrefab();
        LoadPlayerData();

        effectManager = GetComponentInChildren<EffectManager_HMJ>();

        player = GameObject.Find("Player");
    }
    void Start()
    {
        SetMeshData(PlayerCharacterType.WizardType);
    }
    void Update()
    {
        ChangeMeshPrefab();
    }

    void LoadPrefab()
    {
        GameObject player = GameObject.Find("Player");

        for(int i = 0; i < player.transform.childCount; i++)
        {
            GameObject gameObject = player.transform.GetChild(i).gameObject;
            if(gameObject.layer == LayerMask.NameToLayer("Player"))
                objectList.Add(gameObject);
        }
    }

    public void SetMeshData(PlayerCharacterType PlayerCharacterMeshType)
    {
        if(curPlayerCharacterType != PlayerCharacterMeshType)
        {
            changeEffect = effectManager.SpawnAndPlayEffect2(player.transform.position, 1.0f, false, new Vector3());
            SetMeshAllActive(false);
            curPlayerCharacterType = PlayerCharacterMeshType;
            SetMeshActive(curPlayerCharacterType, true);
            animator.avatar = avatarList[(int)PlayerCharacterMeshType];
            animator.runtimeAnimatorController = animControllerList[(int)PlayerCharacterMeshType];
        }
    }

    void ChangeMeshPrefab()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetMeshData(PlayerCharacterType.WizardType);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SetMeshData(PlayerCharacterType.ArcherType);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SetMeshData(PlayerCharacterType.WarriorType);
    }

    void SetMeshActive(PlayerCharacterType playerCharacterType, bool bActive)
    {
        objectList[(int)playerCharacterType].SetActive(bActive);

        // HP 시스템 업데이트(캐릭터 변경)
        GameObject.Find("Player").GetComponentInChildren<HPSystem_HMJ>().UpdateHP(0.0f, playerCharacterType);
    }

    // 모든 메쉬 엑티브 설정
    void SetMeshAllActive(bool bActive)
    {
        foreach (GameObject obj in objectList)
        {
            obj.SetActive(bActive);
        }
    }

    void LoadPlayerData()
    {
        animator = GetComponentInChildren<Animator>();
        avatarList.Add(Resources.Load<Avatar>("Knight D Pelegrini"));
        avatarList.Add(Resources.Load<Avatar>("Erika Archer With Bow Arrow"));
        avatarList.Add(Resources.Load<Avatar>("Paladin WProp J Nordstrom"));

        animControllerList.Add(Resources.Load<RuntimeAnimatorController>("Player_Wizard"));
        animControllerList.Add(Resources.Load<RuntimeAnimatorController>("Player_Archer"));
        animControllerList.Add(Resources.Load<RuntimeAnimatorController>("Player_Warrior"));
    }

    public PlayerCharacterType GetPlayerCharacterType()
    {
        return curPlayerCharacterType;
    }
}
 