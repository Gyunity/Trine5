using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformDrop_HMJ : MonoBehaviour
{
    public float rotationSpeed = 1.0f;  // 회전 속도
    private Quaternion targetRotation;  // 목표 회전
    private bool shouldRotate = false;  // 회전을 할지 여부
    bool playerObjectCollision = false;

    void Start()
    {
        // 초기 목표 회전은 현재 회전
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldRotate)
        {
            // 현재 회전에서 목표 회전으로 부드럽게 회전
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 목표 회전과 매우 가까워지면 회전 중지
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;  // 목표 회전에 도달
                shouldRotate = false;  // 회전 완료
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.name.Contains("Player") && !playerObjectCollision) || other.gameObject.layer == LayerMask.NameToLayer("SummonedObject"))
        {

            // 충돌한 위치가 나무통의 왼쪽인지 오른쪽인지 확인
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 direction = contactPoint - transform.position;

            if (direction.x < 0)  // 나무통의 왼쪽에서 충돌
            {
                // 왼쪽 충돌 시 시계방향으로 45도 회전

                float targetZRotation = Mathf.Clamp(transform.eulerAngles.z + 45, -60.0f, 110.0f);
                targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, targetZRotation);
                shouldRotate = true;
            }
            else
            {
                float targetZRotation = Mathf.Clamp(transform.eulerAngles.z - 45, 70.0f, 110.0f);
                targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, targetZRotation);
                shouldRotate = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SummonedObject")) // 사용자 오브젝트 우선
        {
            playerObjectCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SummonedObject")) // 사용자 오브젝트 우선
        {
            playerObjectCollision = false;
        }
    }
}
                                            