using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindBoneManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static FindBoneManager _instance = null;

    public void Awake()
    {
        if (_instance == null)
        {
            // 전역변수 instance에 인스턴스가 없다면 자신을 넣어준다.
            _instance = this;

            // 씬 전환이 되더라도 파괴되지 않게 한다.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 만약 씬 이동이 되었을 경우에도 씬에 Hierarchy에 FindBoneManager이 존재할 수 있다.
            // 이전 씬에서 사용하는 인스턴스를 계속 사용한다면(이미 인스턴스가 존재한다면)
            // 자신을 삭제한다.
            Destroy(gameObject);
        }
    }

    public static FindBoneManager Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new FindBoneManager();
            }
            return _instance;
        }
    }


    public Transform FindBone(Transform cur, string boneName)
    {
        // 현재 Transform이 찾고자 하는 뼈 이름과 일치하면 반환
        if (cur.name == boneName) // 뼈 이름이 같으면
            return cur;

        foreach (Transform child in cur) // 재귀적으로 뼈 찾기
        {
            Transform find = FindBone(child, boneName);
            if (null != find) // 찾았다면
                return find;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
