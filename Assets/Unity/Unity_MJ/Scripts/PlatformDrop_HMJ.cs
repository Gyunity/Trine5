using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDrop_HMJ : MonoBehaviour
{
    List<Collider> colliders;

    // Start is called before the first frame update
    void Start()
    {
        colliders = new List<Collider>(GetComponentsInChildren<Collider>()); // 모든 콜라이더 가져오기

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 콜라이더 체크
        foreach (ContactPoint contact in collision.contacts)
        {
            Collider collider = contact.thisCollider;

            if (colliders[0] == collider) // 0번 - 왼쪽 충돌
            {
                int a = 0;
                a++;
                // 충돌한 콜라이더 실행
            }
            else if (colliders[1] == collider) // 1번
            {
                int b = 0;
                b++;
                // 충돌한 콜라이더 실행
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 충돌한 콜라이더 체크
        foreach (ContactPoint contact in collision.contacts)
        {
            Collider collider = contact.thisCollider;

            if (colliders[0] == collider) // 0번
            {
                int a = 0;
                a++;
                // 충돌한 콜라이더 실행
            }
            else if (colliders[1] == collider) // 1번
            {
                int b = 0;
                b++;
                // 충돌한 콜라이더 실행
            }
        }
    }
}
