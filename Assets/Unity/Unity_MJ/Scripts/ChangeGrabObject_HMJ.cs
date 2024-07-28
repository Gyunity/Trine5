using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class ChangeGrabObject_HMJ : MonoBehaviour
{
    public enum MeshType
    {
        Cube,
        Brick,
        Sphere,
        MeshType_End
    };

    MeshType curMeshType;

    List<GameObject> objectList = new List<GameObject>();
    GameObject grabObject;
    // Start is called before the first frame update

    private void Awake()
    {
        LoadPrefab();
        curMeshType = MeshType.MeshType_End;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadPrefab()
    {
        // 프리펩 정보 리스트에 미리 로드
        string[] prefabNames = { "CubePrefab_HMJ.prefab", "BrickPrefab_HMJ.prefab", "SpherePrefab_HMJ.prefab" };
        foreach (string prefabName in prefabNames)
        {
            GameObject obj = Resources.Load<GameObject>("Assets/Unity/Unity_MJ/Prefabs/" + prefabName);
            objectList.Add(Instantiate(obj, Vector3.zero, Quaternion.identity));
        }
    }
    public void SetMeshData(ChangeGrabObject_HMJ.MeshType meshtype)
    {
        if(curMeshType != meshtype)
        {
            switch(meshtype)
            {
                case MeshType.Cube:
                    //grabObject = objectList[];
                    break;
                case MeshType.Brick:
                    break;
                case MeshType.Sphere:
                    break;
            }
        }
    }
}
