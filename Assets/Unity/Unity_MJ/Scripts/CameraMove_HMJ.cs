using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    // ī�޶� ���� ���

    public float smoothSpeed = 0.125f;
    // ī�޶��� ���󰡴� �ӵ�

    public Vector3 offset;
    // ���� ī�޶� ������ �Ÿ�

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
        // ī�޶��� ��ǥ ��ġ ���
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
