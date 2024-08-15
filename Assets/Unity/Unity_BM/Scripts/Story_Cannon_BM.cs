using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_Cannon_BM : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject == player)
    //    {
    //        //들어오면 계속 데미지
    //        print("뎀지");
    //    }
    //}

    /*public int damage = 10; // 매번 줄 데미지 양
    public float damageInterval = 1f; // 데미지를 줄 간격 (초)

    private Coroutine damageCoroutine;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player ) // 플레이어 태그를 가진 오브젝트와 충돌 시
        {
            damageCoroutine = StartCoroutine(ApplyDamageOverTime(other));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player ) // 플레이어가 범위를 벗어날 때
        {
            StopCoroutine(damageCoroutine);
        }

    }

    IEnumerator ApplyDamageOverTime(Collider player)
    {
        while (true)
        {
            // 여기서 데미지를 적용하는 로직을 구현합니다.
            //player.GetComponent<PlayerHealth>().TakeDamage(damage);
            print("데미지");
            yield return new WaitForSeconds(damageInterval);
        }
    }*/
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            print("데미지");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            print("데미지");
        }
    }
}
