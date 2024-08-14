using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger_HMJ : MonoBehaviour
{
 
    // 트리거에 충돌이 발생할 때 실행되는 함수입니다.
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 플레이어일 경우 씬 전환을 수행합니다.
        if(other.name.Contains("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
