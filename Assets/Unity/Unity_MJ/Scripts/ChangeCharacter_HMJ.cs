using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static ChangeGrabObject_HMJ;

public enum PlayerCharacterType
{
    WizardType,
    ArcherType,
    PlayerCharacterTypeEnd
}

public class ChangeCharacter : MonoBehaviour
{
    List<GameObject> objectList = new List<GameObject>();
    List<Avatar> avatarList = new List<Avatar>();
    PlayerCharacterType curPlayerCharacterType = PlayerCharacterType.PlayerCharacterTypeEnd;
    Animator animator;

    private void Awake()
    {
        LoadPrefab();
        LoadAnimData();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetMeshData(PlayerCharacterType.WizardType);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMeshPrefab();
    }

    void LoadPrefab()
    {
        GameObject player = GameObject.Find("Player");

        for(int i = 0; i < (int)PlayerCharacterType.PlayerCharacterTypeEnd; i++)
        {
            objectList.Add(player.transform.GetChild(i).gameObject);
        }

        //// 프리펩 정보 리스트에 미리 로드
        //string[] prefabNames = { "Wizard.prefab", "Archer.prefab"};
        //foreach (string prefabName in prefabNames)
        //{
        //    // 프리팹 로드(특정 경로에 있는 프리팹 3가지 로드)
        //    GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Unity/Unity_MJ/Prefabs/Player/" + prefabName);
        //    GameObject Data = Instantiate(obj, Vector3.zero, Quaternion.identity);
        //    objectList.Add(Data);
        //    Data.transform.SetParent(player.transform, false);
        //}
    }

    public void SetMeshData(PlayerCharacterType PlayerCharacterMeshType)
    {
        if(curPlayerCharacterType != PlayerCharacterMeshType)
        {
            SetMeshAllActive(false);
            curPlayerCharacterType = PlayerCharacterMeshType;
            SetMeshActive(curPlayerCharacterType, true);
            animator.avatar = avatarList[(int)PlayerCharacterMeshType];

        }
    }

    void ChangeMeshPrefab()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetMeshData(PlayerCharacterType.WizardType);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetMeshData(PlayerCharacterType.ArcherType);
        }
    }

    void SetMeshActive(PlayerCharacterType playerCharacterType, bool bActive)
    {
        objectList[(int)playerCharacterType].SetActive(bActive);
    }

    // 모든 메쉬 엑티브 설정
    void SetMeshAllActive(bool bActive)
    {
        foreach (GameObject obj in objectList)
        {
            obj.SetActive(bActive);
        }
    }

    void LoadAnimData()
    {
        // 

        animator = GetComponentInChildren<Animator>();
        avatarList.Add(AssetDatabase.LoadAssetAtPath<Avatar>("Assets/Unity/Unity_MJ/Assets/Mesh/Download/Knight D Pelegrini.fbx"));
        avatarList.Add(AssetDatabase.LoadAssetAtPath<Avatar>("Assets/Unity/Unity_MJ/Assets/Character/Erika Archer With Bow Arrow.fbx"));
    }
}
