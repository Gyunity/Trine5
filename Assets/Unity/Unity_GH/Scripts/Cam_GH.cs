using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Cam_GH : MonoBehaviour
{
    public GameObject player;
    public GameObject camFIx;
    public float smoothSpeed = 0.125f;
    Vector3 camFocusPos;
    Vector3 camDir;
    public Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.forward = camDir;


       

    }
    private void LateUpdate()
    {
        // 카메라의 목표 위치 계산
       // Vector3 desiredPosition = camFocusPos + offset;
        camFocusPos = (player.transform.position + camFIx.transform.position) / 2;

        camDir = camFocusPos - transform.position;

        Vector3 smoothedPosition = Vector3.Lerp(transform.forward, camDir, smoothSpeed);

        transform.forward = smoothedPosition;
    }
}
