using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;
using static ChangeGrabObject_HMJ;

public class ChangeGrabObject_HMJ : MonoBehaviour
{
    public enum MeshType
    {
        Cube,
        Brick,
        Sphere,
        NoneMesh,
        MeshType_End
    };

    MeshType curMeshType;

    List<GameObject> objectList = new List<GameObject>();
    // Start is called before the first frame update

    private void Awake()
    {
        LoadPrefab();
        SetMeshAllActive(false); // 모든 엑티브 끄기
        curMeshType = MeshType.MeshType_End;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangeMeshPrefab();
    }

    void LoadPrefab()
    {
        // 프리펩 정보 리스트에 미리 로드
        string[] prefabNames = { "CubePrefab_HMJ.prefab", "BrickPrefab_HMJ.prefab", "SpherePrefab_HMJ.prefab" }; // .prefab
        foreach (string prefabName in prefabNames)
        {
            GameObject parentObject = GameObject.Find("Player");
            // 프리팹 로드(특정 경로에 있는 프리팹 3가지 로드)
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Unity/Unity_MJ/Prefabs/" + prefabName);
            objectList.Add(Instantiate(obj, Vector3.zero, Quaternion.identity));
            parentObject.transform.SetParent(parentObject.transform, false);
        }
    }
    
    // 메쉬 데이터 설정(특정 타입의 메쉬만 키기)
    public void SetMeshData(ChangeGrabObject_HMJ.MeshType meshtype)
    {
        if(curMeshType != meshtype)
        {
            
            SetMeshAllActive(false); // 모든 엑티브 끄기
            curMeshType = meshtype;

            if (curMeshType == MeshType.NoneMesh)
                return;

            objectList[(int)curMeshType].SetActive(true); // 해당 오브젝트만 엑티브 키기
            objectList[(int)curMeshType].GetComponentInChildren<Rigidbody>().useGravity = false; // 해당 오브젝트만 중력 끄기
            objectList[(int)curMeshType].GetComponentInChildren<Rigidbody>().isKinematic = true;

            //objectList[(int)curMeshType].transform.
            // 자식 조종 오브젝트를 플레이어 머리 위로
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            playerPos.y += 5.0f;
            objectList[(int)curMeshType].transform.position = playerPos;

        }
    }

    void SetMeshActive(ChangeGrabObject_HMJ.MeshType meshtype, bool bActive)
    {
        // 해당 스크립트가 없는 오브젝트는 회전X
        RaycastObjectData_HMJ RaycastObjectData = objectList[(int)meshtype].GetComponentInChildren<RaycastObjectData_HMJ>();
        if (RaycastObjectData)
        {
            RaycastObjectData.SetRotateValue(0.0f);
        }
        objectList[(int)meshtype].SetActive(bActive);
    }

    // 모든 메쉬 엑티브 설정
    void SetMeshAllActive(bool bActive)
    {
        foreach (GameObject obj in objectList)
        {
            // 해당 스크립트가 없는 오브젝트는 회전X
            RaycastObjectData_HMJ RaycastObjectData = obj.GetComponentInChildren<RaycastObjectData_HMJ>();
            if (RaycastObjectData)
            {
                RaycastObjectData.SetRotateValue(0.0f);
            }

            obj.SetActive(bActive);
        }


    }
    
    void ChangeMeshPrefab()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SetMeshData(MeshType.Cube);
        else if (Input.GetKeyDown(KeyCode.F))
            SetMeshData(MeshType.Brick);
        else if (Input.GetKeyDown(KeyCode.Q))
            SetMeshData(MeshType.Sphere);
        else if (Input.GetKeyDown(KeyCode.R))
            SetMeshData(MeshType.NoneMesh);

    }
}
