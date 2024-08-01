using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    // 카메라가 따라갈 대상

    public float smoothSpeed = 0.125f;
    // 카메라의 따라가는 속도

    public Vector3 offset;
    // 대상과 카메라 사이의 거리

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        // 카메라의 목표 위치 계산
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
 