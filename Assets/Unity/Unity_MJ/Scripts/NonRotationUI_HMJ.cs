using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonRotationUI_HMJ : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = rotation;
    }

    private void LateUpdate()
    {
        Vector3 offset = new Vector3(-1.1f, 2.0f, 0.0f);

        // 플레이어의 위치에 따라 UI 위치를 설정
        transform.position = player.transform.position + offset;


        // 플레이어의 회전은 적용하되, Y축 회전은 무시
        Quaternion originalRotation = player.transform.rotation;
        Vector3 eulerAngles = originalRotation.eulerAngles;
        eulerAngles.y = 0;  // Y축 회전을 0으로 설정

        // 최종적으로 UI의 회전을 적용
        transform.rotation = Quaternion.Euler(eulerAngles);
    }
}
